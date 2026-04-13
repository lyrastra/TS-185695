using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class MediationFeeMapper
    {
        public static MediationFeeImportRequest Map(ImportMediationFee commandData)
        {
            return new MediationFeeImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Description = commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                BillLinks = LinkedDocumentsMapper.MapBillLinks(commandData.BillLinks),
                DocumentLinks = LinkedDocumentsMapper.MapDocumentLinks(commandData.DocumentLinks),
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static MediationFeeImportRequest Map(ImportDuplicateMediationFee commandData)
        {
            return new MediationFeeImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Description = commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                BillLinks = LinkedDocumentsMapper.MapBillLinks(commandData.BillLinks),
                DocumentLinks = LinkedDocumentsMapper.MapDocumentLinks(commandData.DocumentLinks),
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static MediationFeeImportRequest Map(ImportWithMissingContractMediationFee commandData)
        {
            return new MediationFeeImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                Description = commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                BillLinks = Array.Empty<BillLinkSaveRequest>(),
                DocumentLinks = Array.Empty<DocumentLinkSaveRequest>(),
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingContract,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static MediationFeeImportRequest Map(ImportWithMissingContractorMediationFee commandData)
        {
            return new MediationFeeImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                BillLinks = Array.Empty<BillLinkSaveRequest>(),
                DocumentLinks = Array.Empty<DocumentLinkSaveRequest>(),
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingKontragent,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }
    }
}
