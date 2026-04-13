namespace Moedelo.Manufacturing.Dto.ManufacturingReports
{
    public class ManufacturingReportItemComponentDto
    {
        public int Id { get; set; }
        
        public int ManufacturingReportItemId { get; set; }
        
        public long ProductId { get; set; }
        
        public decimal Count { get; set; }
    }
}