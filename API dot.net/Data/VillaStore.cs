using API_dot.net.Models.Dto;

namespace API_dot.net.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO { Id = 1, Name = "Pool View", Squares = 30, Сapacity = 20 },
            new VillaDTO {Id = 2, Name = "Beach Viev", Squares = 50, Сapacity = 40}
        };
    }
}
