namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Reports.Models
{
    internal class XlsReportModel
    {
        public string Date { get; set; }
        public string PaymentNumber { get; set; }
        public string Sum { get; set; }
        public string SumInWords { get; set; }
        public string Description { get; set; }
        public string PaymentPriority { get; set; }

        public string Kbk { get; set; }
        public string CodeUin { get; set; }
        public string Oktmo { get; set; }
        public string BudgetaryPaymentType { get; set; }
        public string BudgetaryPayerStatus { get; set; }
        public string BudgetaryPaymentBase { get; set; }
        public string BudgetaryDocNumber { get; set; }
        public string BudgetaryDocDate { get; set; }
        public string BudgetaryPeriod { get; set; }

        public string Payer { get; set; }
        public string PayerSettlementNumber { get; set; }
        public string PayerInn { get; set; }
        public string PayerKpp { get; set; }
        public string PayerBank{ get; set; }
        public string PayerBankBik { get; set; }
        public string PayerCorrespondentAccount { get; set; }


        public string Recipient { get; set; }
        public string RecipientSettlementNumber { get; set; }
        public string RecipientInn { get; set; }
        public string RecipientKpp { get; set; }
        public string RecipientBank { get; set; }
        public string RecipientBankBik { get; set; }
        public string RecipientCorrespondentAccount { get; set; }
    }
}
