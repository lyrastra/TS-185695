using System.Collections.Generic;
using Moedelo.AccountingV2.Dto.Payments.SalaryProject;

namespace Moedelo.AccountingV2.Dto.Payments
{
    public class SavingPaymentsModelDto
    {
        public SavingPaymentsModelDto()
        {
            PaymentOrders = new List<PaymentOrderDto>();
            CashOrders = new List<CashOrderDto>();
            NdflOrders = new List<PaymentOrderDto>();
            SalaryProjectPaymentOrders = new List<SalaryProjectPaymentOrderDto>();
        }

        public List<PaymentOrderDto> PaymentOrders { get; set; }

        public List<CashOrderDto> CashOrders { get; set; }

        public List<PaymentOrderDto> NdflOrders { get; set; }

        public List<SalaryProjectPaymentOrderDto> SalaryProjectPaymentOrders { get; set; }

        public int? SettlementAccountId { get; set; }

        public decimal DividendsNdflSum { get; set; }
    }
}