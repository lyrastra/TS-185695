using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Dto.Money.Operations;

namespace Moedelo.Finances.Api.Mappers.Money
{
    public static class MoneyOperationMapper
    {
        public static MoneyOperationDto MapOperationToDto(MoneyOperation operation)
        {
            return new MoneyOperationDto
            {
                Date = operation.Date,
                Description = operation.Description,
                DocumentBaseId = operation.DocumentBaseId,
                Id = operation.Id,
                KontragentId = operation.KontragentId,
                KontragentName = operation.KontragentName,
                Number = operation.Number,
                OperationState = operation.OperationState,
                OperationType = operation.OperationType,
                PaidStatus = operation.PaidStatus,
                Sum = operation.Sum,
                SettlementAccountId = operation.SettlementAccountId,
                TaxationSystemType = operation.TaxationSystemType
            };
        }
    }
}