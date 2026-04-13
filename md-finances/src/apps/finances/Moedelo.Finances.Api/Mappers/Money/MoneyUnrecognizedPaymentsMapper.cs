using System.Linq;
using Moedelo.Common.Enums.Enums.FinancialTransactions;
using Moedelo.Common.Enums.Enums.Money;
using Moedelo.Finances.Domain.Models.Money.Table;
using Moedelo.Finances.Dto.Money.Table;

namespace Moedelo.Finances.Api.Mappers.Money
{
    public static class MoneyUnrecognizedPaymentsMapper
    {
        public static MoneyTableRequest MapTableRequestToDomain(this MoneyTableRequestDto clientData)
        {
            return new MoneyTableRequest
            {
                Count = clientData.Count,
                Offset = clientData.Offset,
                SourceType = clientData.SourceType,
                SourceId = clientData.SourceId,
            };
        }

        public static UnrecognizedMoneyTableResponseDto MapUnrecognizedTableResponseToClient(this UnrecognizedMoneyTableResponse response)
        {
            return new UnrecognizedMoneyTableResponseDto
            {
                TotalCount = response.TotalCount,
                Operations = response.Operations.Select(Map).ToList(),
            };
        }

        private static UnrecognizedMoneyTableOperationDto Map(UnrecognizedMoneyTableOperation operation)
        {
            return new UnrecognizedMoneyTableOperationDto
            {
                DocumentBaseId = operation.DocumentBaseId,
                Date = operation.Date,
                Direction = (MoneyDirection) operation.Direction,
                OperationType = (OperationType) operation.OperationType,
            };
        }
    }
}