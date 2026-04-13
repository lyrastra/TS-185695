using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class PaymentFromCustomerMapper
    {
        public static PaymentFromCustomerImportRequest Map(ImportPaymentFromCustomer commandData)
        {
            return new PaymentFromCustomerImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Description = commandData.Description,
                IsMediation = commandData.IsMediation,
                MediationCommissionSum = commandData.MediationCommissionSum,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                MediationNdsType = commandData.MediationNds?.NdsType,
                MediationNdsSum = commandData.MediationNds?.NdsSum,
                BillLinks = LinkedDocumentsMapper.MapBillLinks(commandData.BillLinks),
                DocumentLinks = LinkedDocumentsMapper.MapDocumentLinks(commandData.DocumentLinks),
                TaxationSystemType = commandData.TaxationSystemType,
                PatentId = commandData.PatentId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static PaymentFromCustomerImportRequest Map(ImportDuplicatePaymentFromCustomer commandData)
        {
            return new PaymentFromCustomerImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Description = commandData.Description,
                IsMediation = commandData.IsMediation,
                MediationCommissionSum = commandData.MediationCommissionSum,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                MediationNdsType = commandData.MediationNds?.NdsType,
                MediationNdsSum = commandData.MediationNds?.NdsSum,
                BillLinks = LinkedDocumentsMapper.MapBillLinks(commandData.BillLinks),
                DocumentLinks = LinkedDocumentsMapper.MapDocumentLinks(commandData.DocumentLinks),
                TaxationSystemType = commandData.TaxationSystemType,
                PatentId = commandData.PatentId,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId,
            };
        }

        public static PaymentFromCustomerImportRequest Map(ImportWithMissingContractorPaymentFromCustomer commandData)
        {
            return new PaymentFromCustomerImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = commandData.Description,
                IsMediation = commandData.IsMediation,
                MediationCommissionSum = commandData.MediationCommissionSum,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                MediationNdsType = commandData.MediationNds?.NdsType,
                MediationNdsSum = commandData.MediationNds?.NdsSum,
                BillLinks = Array.Empty<BillLinkSaveRequest>(),
                DocumentLinks = Array.Empty<DocumentLinkSaveRequest>(),
                TaxationSystemType = commandData.TaxationSystemType,
                PatentId = commandData.PatentId,
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
