using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    public class ContributionToAuthorizedCapitalMapper
    {
        public static ContributionToAuthorizedCapitalImportRequest Map(ImportContributionToAuthorizedCapital commandData)
        {
            return new ContributionToAuthorizedCapitalImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = commandData.Description,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static ContributionToAuthorizedCapitalImportRequest Map(ImportDuplicateContributionToAuthorizedCapital commandData)
        {
            return new ContributionToAuthorizedCapitalImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = commandData.Description,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static ContributionToAuthorizedCapitalImportRequest Map(ImportWithMissingContractorContributionToAuthorizedCapital commandData)
        {
            return new ContributionToAuthorizedCapitalImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = Enums.OperationState.MissingKontragent,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }
    }
}
