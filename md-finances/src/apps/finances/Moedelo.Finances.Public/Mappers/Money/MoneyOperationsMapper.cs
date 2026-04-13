using Moedelo.Finances.Domain.Models.Money.Operations;
using Moedelo.Finances.Public.ClientData.Money.Operations;

namespace Moedelo.Finances.Public.Mappers.Money
{
    public static class MoneyOperationsMapper
    {
        public static OperationBaseClientData MapToBase(this MoneyOperation clientData)
        {
            return new OperationBaseClientData
            {
                DocumentBaseId = clientData.DocumentBaseId,
                Direction = clientData.Direction,
                OperationType = clientData.OperationType,
                OperationKind = clientData.OperationKind
            };
        }
    }
}
