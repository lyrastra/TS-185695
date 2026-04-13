using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.Common.Enums.Enums.PostingEngine;

namespace Moedelo.AccPostings.Dto
{
    /// <summary>
    /// Критерии поиска
    /// Все поля являются необязательными
    /// Поля с заданными значениями определяют критерии поиска (используется логическое умножение)
    /// </summary>
    public class AccountingPostingsSearchCriteriaDto
    {
        public IReadOnlyCollection<long> DocumentBaseIds { get; set; } = null;
        public IReadOnlyCollection<SyntheticAccountCode> CreditCodes { get; set; } = null;
        public IReadOnlyCollection<SyntheticAccountCode> DebitCodes { get; set; } = null;
        public IReadOnlyCollection<SyntheticAccountCode> DebitOrCreditCodes { get; set; } = null;
        public DateTime? BeforeDate { get; set; } = null;
        public DateTime? AfterDate { get; set; } = null;
        public IReadOnlyCollection<long> DebitSubcontoIds { get; set; } = null;
        public IReadOnlyCollection<long> CreditSubcontoIds { get; set; } = null;
        public IReadOnlyCollection<OperationType> OperationTypes { get; set; } = null;
        public IReadOnlyCollection<long> CreditOrDebitSubcontoIds { get; set; } = null;

        /// <summary>
        /// Переключить запрос на базу для чтения
        /// </summary>
        public bool IsFromReadOnlyDb { get; set; }
    }
}