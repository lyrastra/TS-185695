using Moedelo.Common.Enums.Enums.Finances.Money;
using System.Collections.Generic;

namespace Moedelo.Finances.Public.ResponseData
{
    public class MoneySourceBalanceDto
    {
        public long Id { get; set; }
        public MoneySourceType Type { get; set; }
        public decimal Balance { get; set; }


        public static object GetSwaggerExample()
        {
            return new
            {
                data = new List<MoneySourceBalanceDto>
                        {
                            new MoneySourceBalanceDto { Id = 3012, Type = MoneySourceType.SettlementAccount, Balance = 2666 }
                        }
            };
        }
    }
}
