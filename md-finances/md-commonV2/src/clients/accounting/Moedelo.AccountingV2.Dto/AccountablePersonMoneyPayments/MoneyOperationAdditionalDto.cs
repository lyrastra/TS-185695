using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.AccountablePersonMoneyPayments
{
    public class MoneyOperationAdditionalDto
    {
        public int Id { get; set; }

        public AdvancePaymentDocumentTypes Type { get; set; }
    }
}