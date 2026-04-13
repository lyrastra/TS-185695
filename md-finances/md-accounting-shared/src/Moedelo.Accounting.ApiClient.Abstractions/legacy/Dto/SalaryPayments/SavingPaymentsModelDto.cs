using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.SalaryPayments
{
    public class SavingPaymentsModelDto
    {
        public IReadOnlyList<PaymentOrderDto> PaymentOrders { get; set; } = new List<PaymentOrderDto>();

        public IReadOnlyList<CashOrderDto> CashOrders { get; set; } = new List<CashOrderDto>();

        public IReadOnlyList<PaymentOrderDto> NdflOrders { get; set; } = new List<PaymentOrderDto>();

        public IReadOnlyList<SalaryProjectPaymentOrderDto> SalaryProjectPaymentOrders { get; set; } = new List<SalaryProjectPaymentOrderDto>();

        public int? SettlementAccountId { get; set; }

        public decimal DividendsNdflSum { get; set; }
    }
}