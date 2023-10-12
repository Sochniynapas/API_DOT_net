using API_dot.net.Data;
using API_dot.net.Logging;
using API_dot.net.Models;
using API_dot.net.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_dot.net.Controllers
{
    [Route("api/LessonAPI")]
    [ApiController]

    public class LessonAPIController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public LessonAPIController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {

            return Ok(db.Villas.ToList());

        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
               
                return BadRequest();
            }
            var villa = db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villa)
        {


            if (db.Villas.FirstOrDefault(u => u.Name.ToLower() == villa.Name.ToLower()) != null){
                ModelState.AddModelError("Custom error", "Villa already exists!");
                return BadRequest(ModelState);

            }
            if(villa == null)
            {
                return BadRequest(villa);
            }
            if(villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError); 
            }

            Villa model = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Squares = villa.Squares,
                Сapacity = villa.Сapacity,
                ImageUrl = villa.ImageUrl
                
            };

            db.Villas.Add(model);
            db.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);

        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult DeleteVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var villa = db.Villas.FirstOrDefault(u=>u.Id == id);

            if(villa == null)
            {
                return NotFound();
            }
            db.Villas.Remove(villa);
            db.SaveChanges();
            return NoContent();

        }


        

        
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO) { 
            
            if(villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }

            //var villa = db.Villas.FirstOrDefault(u => u.Id == id;
            //villa.Name = villaDTO.Name;
            //villa.Сapacity = villaDTO.Сapacity;
            //villa.Squares = villaDTO.Squares;
            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Squares = villaDTO.Squares,
                Сapacity = villaDTO.Сapacity,
                ImageUrl = villaDTO.ImageUrl

            };
            db.Villas.Update(model);
            db.SaveChanges(); 

            return NoContent();
        

        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patch)
        {
            if(patch == null || id == 0)
            {
                return BadRequest();
            }
            var villa = db.Villas.AsNoTracking().FirstOrDefault(u=>u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            VillaDTO villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Squares = villa.Squares,
                Сapacity = villa.Сapacity,
                ImageUrl = villa.ImageUrl

            };
            patch.ApplyTo(villaDto, ModelState);
            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Squares = villaDto.Squares,
                Сapacity = villaDto.Сapacity,
                ImageUrl = villaDto.ImageUrl

            };

            db.Villas.Update(model);
            db.SaveChanges();

            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            return NoContent();

        }


    }
}
