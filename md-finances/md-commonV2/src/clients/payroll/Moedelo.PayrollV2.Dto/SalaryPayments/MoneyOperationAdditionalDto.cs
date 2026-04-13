
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.PayrollV2.Dto.SalaryPayments
{
    public class MoneyOperationAdditionalDto
    {
        public int Id { get; set; }

        public AdvancePaymentDocumentTypes Type { get; set; }
    }
}