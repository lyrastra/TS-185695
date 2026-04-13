using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Common;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class ChargeGenerationInitialDataDto
{
    public FirmContextInfo FirmContextInfo { get; set; }
    public PeriodDto CalculationPeriod { get; set; }
    public SalarySettingsDto SalarySettings { get; set; }
    public IReadOnlyCollection<WorkerDto> Workers { get; set; } = [];
    public BalanceDto Balance { get; set; }
    public IReadOnlyCollection<ChargeTypeDto> ChargeTypes { get; set; } = [];
    public IReadOnlyDictionary<int, CalculationChargeSettingDto> ChargeSettings { get; set; }
}