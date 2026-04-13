using System.Collections.Generic;

namespace Moedelo.Manufacturing.Dto.ManufacturingReports
{
    public class ManufacturedProductReportItemDto
    {
        public int Id { get; set; }

        public long ManufacturingReportId { get; set; }

        /// <summary>
        /// Идентификатор готового продукта
        /// </summary>
        public long ManufacturedProductId { get; set; }
        
        public decimal Count { get; set; }
        
        public List<ManufacturingReportItemComponentDto> Components { get; set; }
    }
}