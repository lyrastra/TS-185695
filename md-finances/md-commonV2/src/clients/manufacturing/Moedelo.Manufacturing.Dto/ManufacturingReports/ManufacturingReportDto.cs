using System;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Manufacturing.Dto.ManufacturingReports
{
    public class ManufacturingReportDto
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }
        
        public long StockId { get; set; }
        
        public int? DivisionId { get; set; }
        
        public DateTime CreateDate { get; set; }
        
        public DateTime ModifyDate { get; set; }
        
        public AccountingDocumentType DocumentType { get; set; }
    }
}