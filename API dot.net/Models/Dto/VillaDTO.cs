using System.ComponentModel.DataAnnotations;

namespace API_dot.net.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int Squares { get; set; }
        public int Сapacity { get; set; }

    }
}
