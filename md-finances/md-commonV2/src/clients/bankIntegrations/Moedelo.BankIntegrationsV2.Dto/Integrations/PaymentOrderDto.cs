using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BankIntegrationsV2.Dto.Integrations
{
    public class PaymentOrderDto
    {
        public OrderDetailsDto Payer { get; set; }

        public OrderDetailsDto Recipient { get; set; }

        public string PaymentNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Sum { get; set; }

        public NdsType NdsType { get; set; }

        public decimal NdsSum { get; set; }

        public PaymentDirection Direction { get; set; }

        public string Purpose { get; set; } 

        public string PurposeCode { get; set; }

        public PaymentPriority PaymentPriority { get; set; }

        public BudgetaryPayerStatus BudgetaryPayerStatus { get; set; }

        public string Kbk { get; set; }

        public string BudgetaryOkato { get; set; }

        public BudgetaryPaymentBase BudgetaryPaymentBase { get; set; }

        public BudgetaryPeriod BudgetaryPeriod { get; set; }

        public string BudgetaryDocNumber { get; set; }

        public DateTime? BudgetaryDocDate { get; set; }

        public BudgetaryPaymentType BudgetaryPaymentType { get; set; }

        public string KindOfPay { get; set; }

        public string NumberTop { get; set; }

        public BankDocType BankDocType { get; set; }

        public string CodeUin { get; set; }

        public OrderType OrderType { get; set; }

        /// <summary> Поле для индентификации платежа внутри между микросервисами</summary>
        public Guid Guid { get; set; }

        public PaymentOrderDto()
        {
            Payer = new OrderDetailsDto();
            Recipient = new OrderDetailsDto();
            SetDefaultData();
        }

        private void SetDefaultData()
        {
            KindOfPay = string.Empty;
            NumberTop = "0401060";
            BudgetaryDocNumber = string.Empty;
            BudgetaryPeriod = new BudgetaryPeriod { Type = BudgetaryPeriodType.None };
            Kbk = string.Empty;
            BudgetaryPayerStatus = BudgetaryPayerStatus.None;
            PaymentNumber = string.Empty;
            BudgetaryPaymentType = BudgetaryPaymentType.None;
            PaymentPriority = PaymentPriority.Fifth;
            Purpose = string.Empty;
            BudgetaryPaymentBase = BudgetaryPaymentBase.None;
        }
    }
}
