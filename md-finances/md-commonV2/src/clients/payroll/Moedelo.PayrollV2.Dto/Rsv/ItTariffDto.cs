using System;

namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class ItTariffDto
    {
        public class RowDto<T>
        {
            public T BeforePeriodValue { get; set; }

            public T CurrentPeriodValue { get; set; }
        }

        public class ReestrRowDto
        {
            public DateTime DateOfRecord{ get; set; }

            public string RecordNumber { get; set; }
        }

        public RowDto<int> RowDto010 { get; set; } = new RowDto<int>();
        public RowDto<decimal> RowDto020 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto030 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto040 { get; set; } = new RowDto<decimal>();
        public ReestrRowDto RowDto050 { get; set; } = new ReestrRowDto();
        public ReestrRowDto RowDto060 { get; set; } = new ReestrRowDto();
    }
}