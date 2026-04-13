using Moedelo.Money.Enums;

public class BudgetaryAccountDto
{
    public BudgetaryAccountCodes Code { get; set; }

    public string FullNumber { get; set; }

    public string Name { get; set; }

    public BudgetaryPeriodType DefaultPeriodType { get; set; }
}