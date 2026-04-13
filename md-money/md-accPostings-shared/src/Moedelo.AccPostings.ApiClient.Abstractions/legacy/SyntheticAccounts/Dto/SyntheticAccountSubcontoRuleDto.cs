using Moedelo.AccPostings.Enums;

namespace Moedelo.AccPostings.ApiClient.Abstractions.legacy.SyntheticAccounts.Dto
{
    /// <summary>
    /// Правило расположения субконто в рамках БУ-счета
    /// </summary>
    public class SyntheticAccountSubcontoRuleDto
    {
        /// <summary>
        /// Идентификаторп счета
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        /// Тип субконто
        /// </summary>
        public SubcontoType SubcontoType { get; set; }

        /// <summary>
        /// Порядок среди всех субконто по счету
        /// </summary>
        public int Level { get; set; }
    }
}
