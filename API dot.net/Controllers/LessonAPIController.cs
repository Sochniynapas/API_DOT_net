using API_dot.net.Data;
using API_dot.net.Logging;
using API_dot.net.Models;
using API_dot.net.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API_dot.net.Controllers
{
    [Route("api/LessonAPI")]
    [ApiController]

    public class LessonAPIController : ControllerBase
    {
        private readonly ILogging logger;

        public LessonAPIController(ILogging logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {

            logger.Log("Getting all villas","");
            return Ok(VillaStore.villaList);

        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                logger.Log("the id was " + id, "error");
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                logger.Log("No villas of id: " + id, "error");
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


            if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villa.Name.ToLower()) != null){
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

            villa.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            
            VillaStore.villaList.Add(villa);

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
            var villa = VillaStore.villaList.FirstOrDefault(u=>u.Id == id);

            if(villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);

            return NoContent();

        }


        

        
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO) { 
            
            if(villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id); 

           
            villa.Name = villaDTO.Name;
            villa.Сapacity = villaDTO.Сapacity;
            villa.Squares = villaDTO.Squares;
            
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
            var villa = VillaStore.villaList.FirstOrDefault(u=>u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }

            patch.ApplyTo(villa);
            
            if(!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            return NoContent();

        }


    }
}
