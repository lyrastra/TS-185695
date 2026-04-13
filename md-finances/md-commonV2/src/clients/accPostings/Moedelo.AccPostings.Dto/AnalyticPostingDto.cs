using System;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.AccPostings.Dto
{
    public class AnalyticPostingDto
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        /// <summary>
        /// Код счета по дебету
        /// </summary>
        public SyntheticAccountCode DebitCode { get; set; }

        /// <summary>
        /// Код счета по кредиту
        /// </summary>
        public SyntheticAccountCode CreditCode { get; set; }
    }
}