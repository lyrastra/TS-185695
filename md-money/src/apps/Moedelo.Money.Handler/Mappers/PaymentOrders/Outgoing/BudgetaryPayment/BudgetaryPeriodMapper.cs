using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing.BudgetaryPayment
{
    internal static class BudgetaryPeriodMapper
    {
        public static BudgetaryPeriod MapToDomain(Kafka.Abstractions.Models.BudgetaryPeriod period)
        {
            if (period.Type == BudgetaryPeriodType.Date)
            {
                return new BudgetaryPeriod
                {
                    Type = period.Type,
                    Date = period.Date.Value.Date,
                    Year = period.Date.Value.Year
                };
            }
            return new BudgetaryPeriod
            {
                Type = period.Type,
                Number = period.Number,
                Year = period.Year
            };
        }
    }
}
