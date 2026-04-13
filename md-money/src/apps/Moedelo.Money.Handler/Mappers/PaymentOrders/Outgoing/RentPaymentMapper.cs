using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing;

internal static class RentPaymentMapper
{
    internal static RentPaymentImportRequest Map(ImportRentPayment eventData)
    {
        return new RentPaymentImportRequest
        {
            Number = eventData.Number,
            Date = eventData.Date,
            Description = eventData.Description,
            Kontragent = KontragentMapper.MapToKontragent(eventData.Contractor),
            Sum = eventData.Sum,
            ContractBaseId = eventData.ContractBaseId,
            SettlementAccountId = eventData.SettlementAccountId,
            SourceFileId = eventData.SourceFileId,
            ImportId = eventData.ImportId,
            OperationState = OperationState.Imported,
            ImportRuleIds = eventData.ImportRuleIds,
            ImportLogId = eventData.ImportLogId,
            OutsourceState = eventData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
        };
    }

    internal static RentPaymentImportRequest Map(ImportDuplicateRentPayment eventData)
    {
        return new RentPaymentImportRequest
        {
            Number = eventData.Number,
            Date = eventData.Date,
            Description = eventData.Description,
            Kontragent = KontragentMapper.MapToKontragent(eventData.Contractor),
            Sum = eventData.Sum,
            ContractBaseId = eventData.ContractBaseId,
            SettlementAccountId = eventData.SettlementAccountId,
            SourceFileId = eventData.SourceFileId,
            ImportId = eventData.ImportId,
            OperationState = OperationState.Duplicate,
            OutsourceState = eventData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            ImportRuleIds = eventData.ImportRuleIds,
            ImportLogId = eventData.ImportLogId,
            DuplicateId = eventData.DuplicateId,
        };
    }

    internal static RentPaymentImportRequest Map(ImportWithMissingContractRentPayment eventData)
    {
        return new RentPaymentImportRequest
        {
            Number = eventData.Number,
            Date = eventData.Date,
            Description = eventData.Description,
            Kontragent = KontragentMapper.MapToKontragent(eventData.Contractor),
            Sum = eventData.Sum,
            SettlementAccountId = eventData.SettlementAccountId,
            SourceFileId = eventData.SourceFileId,
            ImportId = eventData.ImportId,
            OperationState = OperationState.MissingContract,
            ContractBaseId = null,
            ImportRuleIds = eventData.ImportRuleId > 0
                ? new []{ eventData.ImportRuleId.Value }
                : Array.Empty<int>(),
            ImportLogId = eventData.ImportLogId,
            OutsourceState = eventData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
        };
    }

    internal static RentPaymentImportRequest Map(ImportWithMissingContractorRentPayment eventData)
    {
        return new RentPaymentImportRequest
        {
            Number = eventData.Number,
            Date = eventData.Date,
            Description = eventData.Description,
            Sum = eventData.Sum,
            SettlementAccountId = eventData.SettlementAccountId,
            SourceFileId = eventData.SourceFileId,
            ImportId = eventData.ImportId,
            OperationState = OperationState.MissingKontragent,
            ImportRuleIds = eventData.ImportRuleId > 0
                ? new []{ eventData.ImportRuleId.Value }
                : Array.Empty<int>(),
            ImportLogId = eventData.ImportLogId,
            OutsourceState = eventData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
        };
    }
}