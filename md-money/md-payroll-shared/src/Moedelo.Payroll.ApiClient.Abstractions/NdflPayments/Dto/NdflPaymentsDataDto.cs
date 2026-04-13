using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.NdflPayments.Dto;

public class NdflPaymentsDataDto
{
    /// <summary>
    /// Есть непривязанные ПП
    /// </summary>
    public bool HasUnboundPayments { get; set; }
    /// <summary>
    /// Есть неоплаченные начисления
    /// </summary>
    public bool HasUnpaidCharges { get; set; }
    /// <summary>
    /// Есть ПП с признаком "не оплачено"
    /// </summary>
    public bool HasUnpaidPayments { get; set; }
    public IReadOnlyCollection<NdflPaymentsNdflSettingDto> NdflSettings { get; set; } =
        Array.Empty<NdflPaymentsNdflSettingDto>();
    public IReadOnlyCollection<NdflPaymentsWorkerDto> Workers { get; set; } = Array.Empty<NdflPaymentsWorkerDto>();
    public IReadOnlyCollection<NdflPaymentsIncomeDto> Incomes { get; set; } = Array.Empty<NdflPaymentsIncomeDto>();
}