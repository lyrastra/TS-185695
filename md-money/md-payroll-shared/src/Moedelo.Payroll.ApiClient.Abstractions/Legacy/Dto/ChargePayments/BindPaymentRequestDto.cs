using System;
using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargePayments;

public class BindPaymentRequestDto
{
    public IReadOnlyList<BindPaymentDto> Payments { get; set; } = Array.Empty<BindPaymentDto>();
    public IReadOnlyList<BindSalaryProjectDto> SalaryProjectData { get; set; } = Array.Empty<BindSalaryProjectDto>();
    public IReadOnlyCollection<BindOverPaymentDto> OverPayments { get; set; } = Array.Empty<BindOverPaymentDto>();
}