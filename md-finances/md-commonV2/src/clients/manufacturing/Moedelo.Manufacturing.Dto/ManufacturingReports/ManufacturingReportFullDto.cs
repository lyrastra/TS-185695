using System.Collections.Generic;

namespace Moedelo.Manufacturing.Dto.ManufacturingReports
{
    public class ManufacturingReportFullDto
    {
        public ManufacturingReportDto Report { get; set; }
        
        public List<ManufacturedProductReportItemDto> ReportItems { get; set; }
    }
}