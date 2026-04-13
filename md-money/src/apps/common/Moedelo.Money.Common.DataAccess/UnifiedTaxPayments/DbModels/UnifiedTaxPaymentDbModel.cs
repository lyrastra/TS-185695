using Moedelo.Money.Enums;

namespace Moedelo.Money.Common.DataAccess.UnifiedTaxPayments.DbModels
{
    class UnifiedTaxPaymentDbModel
    {
        public long DocumentBaseId { get; set; }
        public long ParentDocumentId { get; set; }
        public long FirmId { get; set; }
        public int KbkNumberId { get; set; }
        public BudgetaryPeriodType PeriodType { get; set; }
        public int PeriodNumber { get; set; }
        public int PeriodYear { get; set; }
        public decimal PaymentSum { get; set; }
        public long? PatentId { get; set; }
        public int? TradingObjectId { get; set; }
        public ProvidePostingType TaxPostingType { get; set; }
    }
}
