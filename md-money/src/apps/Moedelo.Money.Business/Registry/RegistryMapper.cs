using Moedelo.Money.Domain.Registry;
using Moedelo.Money.Domain.SelfCostPayments;
using Moedelo.Money.Registry.Dto;

namespace Moedelo.Money.Business.Registry
{
    class RegistryMapper
    {
        public static RegistryOperation MapOperation(RegistryOperationDto operation)
        {
            return new RegistryOperation
            {
                DocumentBaseId = operation.DocumentBaseId,
                Number = operation.Number,
                Date = operation.Date,
                Sum = operation.Sum,
                Contractor = MapContractor(operation.Contractor),
                Direction = operation.Direction,
                Source = MapSource(operation.Source),
                KontragentSettlementAccountId = operation.KontragentSettlementAccountId,
                OperationType = operation.OperationType,
                Description = operation.Description,
                ModifyDate = operation.ModifyDate,
                IsPaid = operation.IsPaid,
                PatentId = operation.PatentId,
                TaxationSystemType = operation.TaxationSystemType,
                IncludeNds = operation.IncludeNds,
                NdsSum = operation.NdsSum,
                NdsType = operation.NdsType,
                IsMainContractor = operation.IsMainContractor,
            };
        }

        private static Contractor MapContractor(RegistryOperationContractorDto contractor)
        {
            if (contractor == null)
            {
                return null;
            }
            return new Contractor
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Type = contractor.Type
            };
        }

        private static Source MapSource(RegistryOperationSourceDto source)
        {
            return new Source
            {
                Id = source.Id,
                Name = source.Name,
                Type = source.Type
            };
        }

        public static SelfCostPayment MapPayments(SelfCostPaymentResponseDto payment)
        {
            return new SelfCostPayment
            {
                DocumentBaseId = payment.DocumentBaseId,
                Number = payment.Number,
                Date = payment.Date,
                Sum = payment.Sum,
                RubSum = payment.RubSum,
                Type = payment.Type
            };
        }
    }
}
