using Moedelo.Common.Enums.Enums.Finances.Money;

namespace Moedelo.Finances.Public.ClientData.Money
{
    public class MoneySourceDto
    {
        /// <summary>
        /// ИД Счета/Кассы/Кошелька
        /// </summary> 
        public long Id { get; set; }

        /// <summary>
        /// Счет = 1
        /// Касса = 2
        /// Кошелек = 3
        /// </summary> 
        public MoneySourceType Type { get; set; }


    }
}