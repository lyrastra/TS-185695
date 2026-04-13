namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class TemporaryDisabilityDto
    {
        public string TariffCode { get; set; }
        public RowDto<int> RowDto010 { get; set; } = new RowDto<int>();
        public RowDto<int> RowDto015 { get; set; } = new RowDto<int>();
        public RowDto<decimal> RowDto020 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto030 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto040 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto050 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto051 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto052 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto053 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto054 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto055 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto060 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto070 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto080 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto0901 { get; set; } = new RowDto<decimal>();
        public RowDto<decimal> RowDto0902 { get; set; } = new RowDto<decimal>();
        public decimal? FssDisabilityRate { get; set; }
        public decimal? FssForStayingTemporarilyRate { get; set; }
    }
}