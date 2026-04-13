using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    internal static class PaymentToSupplierMapper
    {
        public static PaymentToSupplierApplyIgnoreNumberRequest Map(ApplyIgnoreNumberPaymentToSupplier commandData)
        {
            return new PaymentToSupplierApplyIgnoreNumberRequest
            {
                DocumentBaseIds = commandData.DocumentBaseIds,
                ImportRuleId = commandData.ImportRuleId
            };
        }

        public static PaymentToSupplierImportRequest Map(ImportPaymentToSupplier commandData)
        {
            return new PaymentToSupplierImportRequest
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
                DocumentLinks = LinkedDocumentsMapper.MapDocumentLinks(commandData.DocumentLinks),
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static PaymentToSupplierImportRequest Map(ImportDuplicatePaymentToSupplier commandData)
        {
            return new PaymentToSupplierImportRequest
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
                DocumentLinks = LinkedDocumentsMapper.MapDocumentLinks(commandData.DocumentLinks),
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static PaymentToSupplierImportRequest Map(ImportWithMissingContractorPaymentToSupplier commandData)
        {
            return new PaymentToSupplierImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                DocumentLinks = Array.Empty<DocumentLinkSaveRequest>(),
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingKontragent,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }
    }
}
