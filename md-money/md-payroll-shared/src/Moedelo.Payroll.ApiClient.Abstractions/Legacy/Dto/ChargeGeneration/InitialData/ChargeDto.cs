using System;
using System.Collections.Generic;
using Moedelo.Payroll.Enums.Ndfl;
using Moedelo.Payroll.Shared.Enums.Charge;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

public class ChargeDto
{
    public int WorkerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ChargeType ChargeType { get; set; }
    public int ChargeTypeId { get; set; }
    public long? WorkerSalarySettingId { get; set; }
    public long? SpecialScheduleId { get; set; }
    public bool IsRegular { get; set; }
    public decimal Sum { get; set; }
    public decimal SumForFundCharge { get; set; }
    public decimal TaxableSumForNdfl { get; set; }
    public decimal TaxableBonusSum { get; set; }
    public decimal TaxBaseSum { get; set; }
    public DateTime ChargeDate { get; set; }
    public DateTime PaymentDeadline { get; set; }
    public DateTime FundChargeDate { get; set; }
    public decimal SourceSum { get; set; }
    public NdflTaxBaseLimitType NdflTaxBaseLimitType { get; set; }
    public NdflRateType NdflRateType { get; set; }
    public List<ChargeResidueDto> Residues { get; set; } = [];
    public List<ChargeBonusDto> Bonuses { get; set; } = [];
    public List<ChargePatentDto> Patents { get; set; } = [];
    public bool IsPayKontragent { get; set; }
    public ChargeDto ParentChargeForNdfl { get; set; }
    public decimal AppliedNotTaxableSum { get; set; }
    public decimal AppliedAdvanceByPatentSum { get; set; }
    public bool IsPaymentCharge { get; set; }
}