using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Finances.Domain.Models.Money.Autocomplete;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.Finances.DataAccess.Money.Operations.RefundPaymentAutocomplete
{
    public static class RefundPaymentAutocompleteQueryBuilder
    {
        public static QueryObject GetByCriterion(int firmId, PaymentAutocompleteCriterion criterion)
        {
            var stringBuilder = new StringBuilder(RefundPaymentAutocompleteQueries.GetByCriterion);

            if (!string.IsNullOrEmpty(criterion.Query))
            {
                EnableCondition(stringBuilder, "-- query_condition --");
            }

            if (criterion.KontragentId != null)
            {
                EnableCondition(stringBuilder, "-- kontragent_condition --");
            }

            var excludeAccountCodes = new List<int>();
            if (criterion.ExcludeAccountCodes != null && criterion.ExcludeAccountCodes.Any())
            {
                excludeAccountCodes = criterion.ExcludeAccountCodes.Select(eac => (int) eac).ToList();
                EnableCondition(stringBuilder, "-- excludeAccountCodes --");
            }
            
            return new QueryObject(stringBuilder.ToString(), new
            {
                firmId,
                limit = criterion.Limit,
                kontragentId = criterion.KontragentId,
                direction = PaymentDirection.Outgoing,
                paymentLinkType = LinkType.Reason,
                retailRefundDocType = AccountingDocumentType.RetailRefund,
                paidStatus = DocumentStatus.Payed,
                operationTypes = criterion.OperationTypes,
                retailRefundBaseId = criterion.RetailRefundBaseId,
                query = criterion.Query,
                offset = criterion.Offset,
                excludeAccountCodes = excludeAccountCodes.ToIntListTVP()
            });
        }

        private static void EnableCondition(StringBuilder stringBuilder, string condition)
        {
            stringBuilder.Replace($"/*{condition}", "");
            stringBuilder.Replace($"{condition}*/", "");
        }
    }
}
