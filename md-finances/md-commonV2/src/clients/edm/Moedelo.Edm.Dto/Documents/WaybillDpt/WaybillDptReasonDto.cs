using System;

namespace Moedelo.Edm.Dto.Documents.WaybillDpt
{
    public class WaybillDptReasonDto
    {
        public DateTime? DocumentDate { get; set; }
        public string DocumentNumber { get; set; }
        public bool IsBill { get; set; }
        public bool IsContract { get; set; }
    }
}