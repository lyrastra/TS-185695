using System;

namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class WorkerPfrDto
    {
        public class RowDto<T>
        {
            public T Month1Value { get; set; }

            public T Month2Value { get; set; }

            public T Month3Value { get; set; }
        }

        public string TariffCode { get; set; }
        
        public RowDto<bool> IsInsuredInMonthWithoutCategoryCode { get; set; } = new RowDto<bool>();
        public RowDto<string> RowDto200 { get; set; } = new RowDto<string>();
        public RowDto<decimal> RowDto210 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto220 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto230 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto240 { get; set; } = new RowDto<decimal>();
        public RowDto<string> RowDto130 { get; set; } = new RowDto<string>();
        public RowDto<decimal> RowDto140 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto150 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto160 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto170 { get; set; } = new RowDto<decimal>();
    }
}