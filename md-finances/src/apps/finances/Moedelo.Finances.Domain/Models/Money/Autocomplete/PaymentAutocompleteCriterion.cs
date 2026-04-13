using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.Finances.Domain.Models.Money.Autocomplete
{
    public class PaymentAutocompleteCriterion
    {
        public int? KontragentId { get; set; }

        public IReadOnlyCollection<OperationType> OperationTypes { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public string Query { get; set; }

        public long? RetailRefundBaseId { get; set; }
        
        /// <summary>
        /// Исключаемые счета платежей. (Например исключаем 76.06, когда тип контрагента - Прочий, для возвратов покупателю)
        /// </summary>
        public List<SyntheticAccountCode> ExcludeAccountCodes { get; set; }
    }
}
