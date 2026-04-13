using System.Collections.Generic;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData.ChargeSettings;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class CalculationChargeSettingDto
{
    /// <summary>Аванс</summary>
    public IReadOnlyCollection<AdvanceDto> Advances { get; set; }

    /// <summary>Зарплата</summary>
    public IReadOnlyCollection<SalaryTemplateDto> SalaryTemplates { get; set; }

    /// <summary>ГПД</summary>
    public IReadOnlyCollection<WorkerContractSettingsDto> Contracts { get; set; }

    /// <summary>Премии</summary>
    public IReadOnlyCollection<PremiumDto> Premiums { get; set; }

    /// <summary>Больничные и декретные</summary>
    public IReadOnlyCollection<SickListDto> SickLists { get; set; }

    /// <summary>Отпуска по уходу за ребенком</summary>
    public IReadOnlyCollection<ChildCareVacationDto> ChildCares { get; set; }

    /// <summary>Прочие начисления</summary>
    public IReadOnlyCollection<CustomChargeDto> CustomCharges { get; set; }

    /// <summary>Единовременные пособия</summary>
    public IReadOnlyCollection<SameTimeAllowanceDto> SameTimeAllowances { get; set; }

    /// <summary>Увольнения</summary>
    public IReadOnlyCollection<FiredSettingDto> FireSettings { get; set; }

    /// <summary>Список всех отклонений от графика</summary>
    public IReadOnlyCollection<SpecialScheduleDto> AllSpecialSchedules { get; set; }

    /// <summary>Настройка начислений командировочных</summary>
    public IReadOnlyCollection<BusinessTripExpensesSettingDto> BusinessTripExpensesSettings { get; set; }

    /// <summary>Командировочные</summary>
    public IReadOnlyCollection<BusinessTripDto> BusinessTrips { get; set; }

    /// <summary>Надбавки</summary>
    public IReadOnlyCollection<WorkerBonusDto> Bonuses { get; set; }

    /// <summary>Районный коэффициент</summary>
    public IReadOnlyCollection<FirmRegionRateDto> RegionRates { get; set; }

    /// <summary>Вычеты</summary>
    public IReadOnlyCollection<ResidueDto> Residues { get; set; }

    /// <summary>Удержания</summary>
    public IReadOnlyCollection<DeductionDto> Deductions { get; set; }

    /// <summary>История изменения статуса НДфЛ</summary>
    public IReadOnlyCollection<WorkerNdflStatusHistoryDto> WorkerNdflStatusHistories { get; set; }

    /// <summary>История изменения иностранного сотрудника</summary>
    public IReadOnlyCollection<ForeignerHistoryDto> ForeignersHistory { get; set; }

    /// <summary>Периоды типов работы</summary>
    public IReadOnlyCollection<WorkTypePeriodDto> WorkTypePeriods { get; set; }

    /// <summary>Графики работы</summary>
    public IReadOnlyCollection<WorkScheduleHistoryDto> WorkSchedules { get; set; }

    /// <summary>Дни отклонения от графика</summary>
    public IReadOnlyCollection<WorkingDayInfoDto> NonStandardDays { get; set; }


}