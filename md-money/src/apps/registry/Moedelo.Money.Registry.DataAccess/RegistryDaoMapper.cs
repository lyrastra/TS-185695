using Moedelo.Money.Enums;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.DataAccess
{
    static class RegistryDaoMapper
    {
        public static MoneyOperation MapFromDbModel(OperationDbModel data)
        {
            return new MoneyOperation
            {
                DocumentBaseId = data.DocumentBaseId,
                Number = data.Number,
                Date = data.Date,
                Sum = data.Sum,
                Contractor = MapContractor(data),
                Direction = data.Direction,
                Source = MapSource(data),
                KontragentSettlementAccountId = data.KontragentSettlementAccountId,
                OperationType = data.OperationType,
                Description = data.Description,
                ModifyDate = data.ModifyDate,
                IsPaid = data.IsPaid,
                PatentId = data.PatentId,
                TaxationSystemType = data.TaxationSystemType,
                NdsSum = data.NdsSum,
                NdsType = data.NdsType,
                IncludeNds = data.IncludeNds,
                KontragentAccountCode = data.KontragentAccountCode,
            };
        }

        private static Contractor MapContractor(OperationDbModel data)
        {
            if (data.OperationType == OperationType.PaymentOrderIncomingTransferFromAccount ||
                data.OperationType == OperationType.PaymentOrderOutgoingTransferToAccount)
            {
                return null;
            }
            if ((data.OperationType == OperationType.PaymentOrderOutgoingPaymentToNaturalPersons ||
                data.OperationType == OperationType.CashOrderOutgoingPaymentForWorking) &&
                data.ContractorId == null)
            {
                return null;
            }
            if (data.ContractorId == null)
            {
                return new Contractor
                {
                    Id = null,
                    Type = ContractorType.Other,
                    Name = data.ContractorName
                };
            }
            return new Contractor
            {
                Id = data.ContractorId.Value,
                Type = data.ContractorType == (int)ContractorType.Kontragent
                        ? ContractorType.Kontragent
                        : ContractorType.Worker,
                Name = data.ContractorName
            };
        }

        private static Source MapSource(OperationDbModel data)
        {
            if (data == null)
            {
                return new Source();
            }

            return new Source
            {
                Id = data.SourceId,
                Name = data.SourceName,
                Type = data.SourceType
            };
        }
    }
}
