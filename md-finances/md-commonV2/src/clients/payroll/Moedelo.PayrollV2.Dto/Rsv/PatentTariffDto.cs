using System.Collections.Generic;

namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class PatentTariffDto
    {
        public RowDto<decimal> RowDto010 { get; set; } = new RowDto<decimal>();
        public IList<PatentDto> PatentTable { get; set; } = new List<PatentDto>();
    }
}