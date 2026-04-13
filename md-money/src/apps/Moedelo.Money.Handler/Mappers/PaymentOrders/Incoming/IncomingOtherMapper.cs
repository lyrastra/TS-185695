using System.Linq;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.Other.Commands;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.Other.Commands.Models;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class IncomingOtherMapper
    {
        public static ImportOtherIncomingRequest Map(ImportOtherIncoming commandData)
        {
            return new ImportOtherIncomingRequest()
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                Contractor = commandData.OperationState == OperationState.MissingKontragent
                    ? null
                    : KontragentMapper.MapToContractor(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                SourceFileId = commandData.SourceFileId,
                ImportId = commandData.ImportId,
                ImportLogId = commandData.ImportLogId,
                ImportRuleIds = commandData.ImportRuleIds,
                AccPosting = Map(commandData.AccPosting, commandData.Date, commandData.Sum),
                TaxPosting = Map(commandData.TaxPosting),
                OperationState = commandData.OperationState,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static ImportOtherIncomingRequest Map(ImportOtherIncomingDuplicate commandData)
        {
            return new ImportOtherIncomingRequest()
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                Contractor = KontragentMapper.MapToContractor(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                AccPosting = Map(commandData.AccPosting, commandData.Date, commandData.Sum),
                TaxPosting = Map(commandData.TaxPosting),
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                DuplicateId = commandData.DuplicateId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportLogId = commandData.ImportLogId,
                ImportRuleIds = commandData.ImportRuleIds,
            };
        }

        public static ImportOtherIncomingRequest Map(ImportOtherIncomingWithMissingContragent commandData)
        {
            return new ImportOtherIncomingRequest()
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                AccPosting = Map(commandData.AccPosting, commandData.Date, commandData.Sum),
                TaxPosting = Map(commandData.TaxPosting),
                OperationState = OperationState.MissingKontragent,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportLogId = commandData.ImportLogId,
                ImportRuleIds = commandData.ImportRuleIds,
            };
        }

        private static OtherIncomingCustomAccPosting Map(
            IncomingOtherAccPosting commandAccPosting,
            DateTime date,
            decimal sum)
        {
            if (commandAccPosting == null)
            {
                return null;
            }
            
            return new OtherIncomingCustomAccPosting
            {
                Date = date,
                Sum = sum,
                CreditCode = commandAccPosting.CreditCode,
                CreditSubconto = commandAccPosting.CreditSubcontos?.Select(Map).ToArray() ?? Array.Empty<Money.Domain.AccPostings.Subconto>(),
                Description = commandAccPosting.Description,
            };
        }

        private static Domain.AccPostings.Subconto Map(Subconto commandSubconto)
        {
            return new Domain.AccPostings.Subconto()
            {
                Id = commandSubconto.Id,
                Name = commandSubconto.Name
            };
        }

        private static ImportCustomTaxPosting Map(CustomTaxPosting commandData)
        {
            return commandData == null
                ? null
                : new ImportCustomTaxPosting
                {
                    Direction = TaxPostingDirection.Incoming,
                    Sum = commandData.Sum,
                    Description = commandData.Description,
                    Type = commandData.Type,
                    Kind = commandData.Kind,
                    NormalizedCostType = commandData.NormalizedCostType
                };
        }
    }
}
