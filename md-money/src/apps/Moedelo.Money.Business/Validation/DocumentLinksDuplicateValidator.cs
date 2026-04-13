using Moedelo.Money.Business.Abstractions.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Moedelo.Money.Business.Validation
{
    public static class DocumentLinksDuplicateValidator
    {
        public static Task Validate(IReadOnlyCollection<long> documentLinkIds)
        {
            if (documentLinkIds == null || documentLinkIds.Count == 0)
            {
                return Task.CompletedTask;
            }

            var idsGroup = documentLinkIds.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            var duplicate = idsGroup.FirstOrDefault(x => x.Value > 1);
            if (!duplicate.Equals(default(KeyValuePair<long, int>)))
            {
                throw new BusinessValidationException($"Documents", $"Документ с ид {duplicate.Key} указан 2 или более раз");
            }

            return Task.CompletedTask;
        }
    }
}
