using System;

namespace Moedelo.Finances.Business.Services.Money.Operations.Exceptions
{
    public class MoneyOperationNotFoundException : Exception
    {
        public MoneyOperationNotFoundException(long baseId, int firmId)
            : base($"Money operation with BaseId = {baseId} doesn not found for firm with Id = {firmId}")
        {
        }
    }
}