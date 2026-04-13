using Moedelo.Money.Business.Abstractions.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Business.Validation.UnifiedBudgetaryPayment
{
    public static class UnifiedBudgetarySubPaymentsDuplicateValidator
    {
        public static void Validate(IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds == null || documentBaseIds.Count == 0)
            {
                return;
            }

            var idsGroup = documentBaseIds
                .Where(x => x > 0)
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
            var duplicate = idsGroup.FirstOrDefault(x => x.Value > 1);
            if (!duplicate.Equals(default(KeyValuePair<long, int>)))
            {
                throw new BusinessValidationException($"SubPayments", $"Дочерний бюджетный платеж с ид {duplicate.Key} указан 2 или более раз");
            }
        }
    }
}
