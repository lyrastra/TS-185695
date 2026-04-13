using System;

namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class PatentDto
    {
        public string RowDto020 { get; set; }
        public string RowDto030 { get; set; }
        public DateTime RowDto040 { get; set; }
        public DateTime RowDto050 { get; set; }
        public RowDto<decimal> RowDto060 { get; set; } = new RowDto<decimal>();
        public long PatentId { get; set; }
    }
}