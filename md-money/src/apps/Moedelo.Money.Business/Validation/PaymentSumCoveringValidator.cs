using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Domain.LinkedDocuments;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Business.Validation
{
    internal static class PaymentSumCoveringValidator
    {
        public static void Validate(decimal paymentSum, IReadOnlyCollection<DocumentLinkSaveRequest> documentLinks, decimal? reserveSum = 0m)
        {
            if (paymentSum < reserveSum)
            {
                throw new BusinessValidationException("ReserveSum", "Сумма резерва не может быть больше суммы документа");

            }
            var linksSum = documentLinks.Sum(x => x.LinkSum);
            if (paymentSum - reserveSum < linksSum)
            {
                throw new BusinessValidationException("Documents", GetValidationMessage(reserveSum > 0));
            }
        }

        private static string GetValidationMessage(bool hasReserveSum)
        {
            return hasReserveSum
                ? "Сумма связанныех документов не может быть больше суммы платежа с учетом суммы резерва"
                : "Сумма связанныех документов не может быть больше суммы платежа";
        }
    }
}
