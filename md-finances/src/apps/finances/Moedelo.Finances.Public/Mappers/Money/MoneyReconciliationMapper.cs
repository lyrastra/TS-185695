using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.Finances.Public.ClientData.Money.Reconciliation;
using System;
using System.Linq;

namespace Moedelo.Finances.Public.Mappers.Money
{
    public static class MoneyReconciliationMapper
    {
        public static ReconciliationResultClientData MapToClient(this ReconciliationBusinessModel result)
        {
            return new ReconciliationResultClientData
            {
                Status = result.ReconciliationResult.Status,
                SessionId = result.ReconciliationResult.SessionId,
                InitialDate = result.ReconciliationResult.CreateDate,
                ExcessOperations = result.ReconciliationResult.ExcessOperations?.Select(Map).ToArray() ?? Array.Empty<ReconciliationOperationClientData>(),
                MissingOperations = result.ReconciliationResult.MissingOperations?.Select(Map).ToArray() ?? Array.Empty<ReconciliationOperationClientData>(),
                Currency = result.SettlementAccount.Currency,
                SettlementAccount = new SettlementAccountClientData
                {
                    Id = result.SettlementAccount.Id,
                    Name = result.SettlementAccount.Name,
                    Number = result.SettlementAccount.Number,
                },
            };
        }

        private static ReconciliationOperationClientData Map(ReconciliationOperation operation)
        {
            return new ReconciliationOperationClientData
            {
                Id = operation.Id,
                IsOutgoing = operation.IsOutgoing,
                Date = operation.Date,
                Sum = operation.Sum,
                KontragentName = operation.KontragentName,
                Description = operation.Description,
                IsSalary = operation.IsSalary
            };
        }
    }
}
