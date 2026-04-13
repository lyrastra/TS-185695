using System;
using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BillingV2.Dto.AccountingAct
{
    public class PaymentAccountingActFor1CDto
    {
        public int Index { get; set; }
        public int FirmId { get; set; }
        public int PaymentId { get; set; }
        public DateTime ActDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ClientLogin { get; set; }
        public string ClientFullName { get; set; }
        public string ClientOpf { get; set; }
        public string PartnerName { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string SettlementAccount { get; set; }
        public string CorrAccount { get; set; }
        public string BankName { get; set; }
        public string BankBik { get; set; }
        /// <summary>
        /// Тип услуги в 1С
        /// </summary>
        public ActServiceType Service { get; set; }
        /// <summary>
        /// название услуги, по которой выставлен акт
        /// </summary>
        public string ServiceName { get; set; }
        public string Tariff { get; set; }
        public decimal Summa { get; set; }
        public string ProductGroup { get; set; }
        public string PaymentMethod { get; set; }
        public PaymentPositionType PaymentType { get; set; }
        public bool HasNds { get; set; }
        public string Description { get; set; }
        public string IsClosingDocumentsNeeded { get; set; }
        public string IsClosingDocumentsSentViaEdm { get; set; }
        public long ClosingDocumentsPostAddressId { get; set; }
        [Obsolete("На самом деле это Id адреса, а не сам адрес")]
        public string ClosingDocumentsPostAddress => ClosingDocumentsPostAddressId.ToString();
    }
}