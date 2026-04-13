using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Finances.Domain.Enums.Money;

namespace Moedelo.Finances.Api.Mappers.Money.Operations
{
    public static class PaymentTypeMapper
    {
        public static MoneySourceType? MapOperationKindToMoneySourceType(OperationKind operationKind)
        {
            switch (operationKind)
            {
                case OperationKind.PaymentOrderOperation:
                    return MoneySourceType.SettlementAccount;
                case OperationKind.CashOrderOperation:
                    return MoneySourceType.Cash;
                case OperationKind.PurseOperation:
                    return MoneySourceType.Purse;
                default:
                    return null;
            }
        }
    }
}