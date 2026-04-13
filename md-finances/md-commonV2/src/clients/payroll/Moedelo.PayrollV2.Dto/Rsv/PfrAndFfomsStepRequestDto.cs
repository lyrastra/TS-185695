using System.Collections.Generic;

namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class PfrAndFfomsStepRequestDto
    {
        public int Period { get; set; }

        public int Year { get; set; }

        public IReadOnlyCollection<TariffDto> PrevData { get; set; }
    }
}