using Moedelo.Common.Enums.Enums.Finances.Money;
using System;
using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Money
{
    public class BalanceRequestDto
    {
        /// <summary>
        /// Объект запроса баланса
        /// </summary> 
        public List<MoneySourceDto> Sources { get; set; }

        /// <summary>
        /// Дата баланса
        /// </summary> 
        public DateTime OnDate { get; set; }


        public static BalanceRequestDto GetSwaggerExample()
        {
            return new BalanceRequestDto
            {
                Sources = new List<MoneySourceDto>
                {
                    new MoneySourceDto{
                        Id = 3012,
                        Type = MoneySourceType.SettlementAccount
                    }
                },

                OnDate = new DateTime(2019, 09, 16)
            };
        }
    }
}