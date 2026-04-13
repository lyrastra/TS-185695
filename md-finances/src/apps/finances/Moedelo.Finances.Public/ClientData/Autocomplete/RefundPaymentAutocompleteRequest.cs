using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.Finances.Public.ClientData.Autocomplete
{
    public class RefundPaymentAutocompleteRequest
    {
        /// <summary>
        /// Смещение начальной позиции чтения в позициях (НЕ В СТРАНИЦАХ)
        /// </summary>
        public int Offset { get; set; } = 0;

        /// <summary>
        /// Размер страницы в позициях
        /// </summary>
        public int Limit { get; set; } = 50;

        /// <summary>
        /// Только платежи, содержашие в номере указанную подстроку
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Только платежи от/для контрагента с указанными Id
        /// </summary>
        public int? KontragentId { get; set; }

        /// <summary>
        /// В т.ч. платежи, связанные с розн. возвратом с указанными BaseId
        /// </summary>
        public long? RetailRefundBaseId { get; set; }
        
        /// <summary>
        /// Исключаемые счета платежей. (Например исключаем 76.06, когда тип контрагента - Прочий, для возвратов покупателю)
        /// </summary>
        public List<SyntheticAccountCode> ExcludeAccountCodes { get; set; }
    }
}