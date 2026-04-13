using System;
using Moedelo.Billing.Shared.Enums.AutoBilling;

namespace Moedelo.Billing.Abstractions.AutoBilling.Dto;

public sealed class InitiateDto
{
    public int Id { get; set; }
    public InitiateType InitiateType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public FirmValidationType FirmValidationType { get; set; }
    public BillValidationType BillValidationType { get; set; }
    public InitiateState State { get; set; }

    /// <summary> Автор запуска </summary>
    public int OperatorUserId { get; set; }
    
    public ProductTypeEnum ProductType { get; set; }
    public ExtraDataModeEnum ExtraDataMode { get; set; }
}