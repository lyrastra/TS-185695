namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Formula
{
    public class FormulaElementDto
    {
        public string Text { get; set; }

        public FormulaElementType Type { get; set; }

        public string[] Hint { get; set; } = { };
    }
}