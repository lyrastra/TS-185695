using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Kontragents
{
    /// <summary>
    /// Классификация факторов
    /// </summary>
    public enum KontragentRatingClass
    {
        /// <summary>
        /// Правовые факторы
        /// </summary>
        [Description("Правовые факторы")]
        Right = 10,

        /// <summary>
        /// Репутационные факторы
        /// </summary>
        [Description("Репутационные факторы")]
        Reputation = 20,

        /// <summary>
        /// Финансовые факторы
        /// </summary>
        [Description("Финансовые факторы")]
        Finance = 30,

        /// <summary>
        /// Налоговые факторы
        /// </summary>
        [Description("Налоговые факторы")]
        Tax = 40,

        /// <summary>
        /// Другие факторы
        /// </summary>
        [Description("Другие факторы")]
        Unknown = 999999
    }
}
