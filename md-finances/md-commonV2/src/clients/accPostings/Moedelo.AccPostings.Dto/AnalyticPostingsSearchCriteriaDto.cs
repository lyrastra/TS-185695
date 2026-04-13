using System;

namespace Moedelo.AccPostings.Dto
{
    /// <summary>
    /// Критерии поиска
    /// Все поля являются необязательными
    /// Поля с заданными значениями определяют критерии поиска (используется логическое умножение)
    /// </summary>
    public class AnalyticPostingsSearchCriteriaDto
    {
        public DateTime? BeforeDate { get; set; } = null;
        public DateTime? AfterDate { get; set; } = null;
    }
}