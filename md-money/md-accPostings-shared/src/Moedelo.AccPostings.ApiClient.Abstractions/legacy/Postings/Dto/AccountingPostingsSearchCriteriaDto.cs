using System;
using System.Collections.Generic;
using Moedelo.AccPostings.Enums;

namespace Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings.Dto
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
        public DateTime? BeforeDate { get; set; } = null;
        public DateTime? AfterDate { get; set; } = null;
        public IReadOnlyCollection<long> DebitSubcontoIds { get; set; } = null;
        public IReadOnlyCollection<long> CreditSubcontoIds { get; set; } = null;

        /// <summary>
        /// Переключить запрос на базу для чтения
        /// </summary>
        public bool IsFromReadOnlyDb { get; set; }
    }
}