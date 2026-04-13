namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SalaryYears;

public class SalaryYearDto
{
    public int Id { get; set; }

    public int Year { get; set; }

    /// <summary>
    /// База для начисления страховых взносов в ПФР (salary_base)
    /// </summary>
    public decimal SalaryBasePfr { get; set; }

    /// <summary>
    /// База для начисления страховых взносов в ФСС
    /// </summary>
    public decimal SalaryBaseFss { get; set; }

    /// <summary>
    /// Предел дохода - если налогооблагаемый доход сотрудника с начала года по текущий месяц превысил эту сумму, то вычет не применяется
    /// Если Null - то предел дохода не установлен(т.е. вычет применяется не зависимо от дохода)
    /// </summary>
    public decimal MaxResidueChild { get; set; }

    public decimal? AverageWorkingHours { get; set; }
}