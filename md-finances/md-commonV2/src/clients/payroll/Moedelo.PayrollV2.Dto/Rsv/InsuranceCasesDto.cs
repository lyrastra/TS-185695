namespace Moedelo.PayrollV2.Dto.Rsv
{
    public class InsuranceCasesDto
    {
        public class RowDto
        {
            public int CasesCount { get; set; }
            public int PaysCount { get; set; }
            public decimal ExpenseSum { get; set; }
            public decimal FederalBudgetSum { get; set; }
        }

        public RowDto RowDto010 { get; set; } = new RowDto();
        public RowDto RowDto011 { get; set; } = new RowDto();
        public RowDto RowDto020 { get; set; } = new RowDto();
        public RowDto RowDto021 { get; set; } = new RowDto();
        public RowDto RowDto030 { get; set; } = new RowDto();
        public RowDto RowDto031 { get; set; } = new RowDto();
        public RowDto RowDto040 { get; set; } = new RowDto();
        public RowDto RowDto050 { get; set; } = new RowDto();
        public RowDto RowDto060 { get; set; } = new RowDto();
        public RowDto RowDto061 { get; set; } = new RowDto();
        public RowDto RowDto062 { get; set; } = new RowDto();
        public RowDto RowDto070 { get; set; } = new RowDto();
        public RowDto RowDto080 { get; set; } = new RowDto();
        public RowDto RowDto090 { get; set; } = new RowDto();
        public RowDto RowDto100 { get; set; } = new RowDto();
        public RowDto RowDto110 { get; set; } = new RowDto();
    }
}
