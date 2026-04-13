using System;

namespace Moedelo.Parsers.Klto1CParser.Models
{
    public class Balance
    {
        private const int MaxSettlementLength = 20;

        private string settlementAccount;

        public Balance()
        {
            settlementAccount = string.Empty;
        }

        /// <summary> Расчётный счёт </summary>
        public string SettlementAccount
        {
            get { return settlementAccount; }
            set
            {
                settlementAccount = !string.IsNullOrEmpty(value) && value.Length > MaxSettlementLength
                    ? value.Substring(0, MaxSettlementLength)
                    : value;
            }
        }

        /// <summary> Начальный остаток денежных средств </summary>
        public decimal? StartBalance { get; set; }

        /// <summary> Всего поступило </summary>
        public decimal? IncomingBalance { get; set; }

        /// <summary> Всего списано </summary>
        public decimal? OutgoingBalance { get; set; }

        /// <summary> Конечный остаток денежных средств </summary>
        public decimal? EndBalance { get; set; }

        /// <summary> Дата начала </summary>
        public DateTime StartDate { get; set; }

        /// <summary> Дата конца </summary>
        public DateTime? EndDate { get; set; }

        /// <summary> Секция в текстовом виде </summary>
        public string RawSection { get; set; }
    }
}