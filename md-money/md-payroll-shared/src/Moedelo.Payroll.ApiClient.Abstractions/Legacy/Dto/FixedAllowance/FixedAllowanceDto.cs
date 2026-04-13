using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.FixedAllowance;

public class FixedAllowanceDto
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    /// <summary>
    /// Минимальная сумма пособия по уходу за первым ребенком до 1,5 лет
    /// </summary>
    public decimal MinCareFirstChild { get; set; }

    /// <summary>
    /// Минимальная сумма пособия по уходу за в торым и след ребенком ребенком до 1,5 лет
    /// </summary>
    public decimal MinCareSecondChild { get; set; }

    /// <summary>
    /// Пособие при рождении ребенка (единоразовое)
    /// </summary>
    public decimal ChildBirthAllowance { get; set; }

    /// <summary>
    /// Единовременное пособие женщинам, вставшим на учет в ранние сроки ( до 12 недель) беременности
    /// </summary>
    public decimal EarlyPregnancyAllowance { get; set; }

    /// <summary>
    /// Сумма пособия на погребение
    /// </summary>
    public decimal FuneralAllowance { get; set; }

    /// <summary>
    /// Должно ли влиять на ранее созданные отпуска
    /// </summary>
    public bool IsRetrospective { get; set; }
}