namespace Moedelo.CatalogV2.Dto.CourierRegion
{
    public class CourierRegionDto
    {
        public CourierRegionDto()
        {
            
        }

        public CourierRegionDto(int id, string code)
        {
            this.Id = id;
            this.Code = code;
        }

        public int Id { get; set; }

        public string Code { get; set; }
    }
}
