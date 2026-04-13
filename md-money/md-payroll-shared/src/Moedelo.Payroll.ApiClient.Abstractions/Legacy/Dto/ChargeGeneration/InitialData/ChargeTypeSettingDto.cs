using System;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class ChargeTypeSettingDto
{
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool HasFundCharges { get; set; }

    /// <summary>
    /// Минимальная сумма начисления, с которой генерируются начисления в фонды
    /// </summary>
    public decimal MinimalSumForFundCharges { get; set; }
}