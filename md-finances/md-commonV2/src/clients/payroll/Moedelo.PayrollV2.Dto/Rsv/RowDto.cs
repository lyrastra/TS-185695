namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class RowDto<T>
    {
        public T Month1Value { get; set; }

        public T Month2Value { get; set; }

        public T Month3Value { get; set; }

        public T QuarterValue { get; set; }

        public T YearValue { get; set; }
    }
}
