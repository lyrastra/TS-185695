using Moedelo.Finances.Domain.Models.Money.Operations.PaymentOrders;
using Moedelo.Finances.Dto.Money.Operations;

namespace Moedelo.Finances.Api.Mappers.Money
{
    public static class PaymentOrderOperationMapper
    {
        public static MoneyIncomingBalanceOperationDto MapIncomingBalanceOperationToClient(PaymentOrderOperation operation)
        {
            return new MoneyIncomingBalanceOperationDto
            {
                SettlementAccountId = operation.SettlementAccountId ?? 0,
                Date = operation.Date,
                Balance = operation.Sum
            };
        }
    }
}