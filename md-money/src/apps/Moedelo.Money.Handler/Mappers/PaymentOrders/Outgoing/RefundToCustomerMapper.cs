using Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Commands;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    internal static class RefundToCustomerMapper
    {
        public static RefundToCustomerImportRequest Map(ImportRefundToCustomer commandData)
        {
            return new RefundToCustomerImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Description = string.IsNullOrWhiteSpace(commandData.Description)
                    ? PaymentOrdersDescriptions.RefundToCustomer
                    : commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                TaxationSystemType = commandData.TaxationSystemType,
                PatentId = commandData.PatentId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static RefundToCustomerImportRequest Map(ImportDuplicateRefundToCustomer commandData)
        {
            return new RefundToCustomerImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                Description = string.IsNullOrWhiteSpace(commandData.Description)
                    ? PaymentOrdersDescriptions.RefundToCustomer
                    : commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                TaxationSystemType = commandData.TaxationSystemType,
                PatentId = commandData.PatentId,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static RefundToCustomerImportRequest Map(ImportWithMissingContractorRefundToCustomer commandData)
        {
            return new RefundToCustomerImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = string.IsNullOrWhiteSpace(commandData.Description)
                    ? PaymentOrdersDescriptions.RefundToCustomer
                    : commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                TaxationSystemType = commandData.TaxationSystemType,
                PatentId = commandData.PatentId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingKontragent,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }
    }
}
