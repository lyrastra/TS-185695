using Moedelo.Common.Enums.Enums.FinancialOperations;
using Moedelo.Finances.Domain.Models.Money.Operations.MoneyTransfers;
using Moedelo.Finances.Dto.Money.Operations;

namespace Moedelo.Finances.Api.Mappers.Money
{
    public static class MoneyTransferOperationMapper
    {
        public static MoneyTransferOperation MapIncomingBalanceOperationToDomain(MoneyTransferOperation operation, MoneyIncomingBalanceOperationDto dto)
        {
            operation = operation ?? new MoneyTransferOperation
            {
                MoneyBayType = MoneyBayType.Settlement,
                OperationType = "CurrencyBalanceOperation"
            };
            operation.SettlementAccountId = dto.SettlementAccountId;
            operation.Number = "";
            operation.Date = dto.Date;
            operation.Sum = dto.Balance;
            return operation;
        }

        public static MoneyIncomingBalanceOperationDto MapIncomingBalanceOperationToClient(MoneyTransferOperation operation)
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