using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.LinkedDocuments;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash.Commands;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class TransferFromCashMapper
    {
        public static TransferFromCashResponseDto Map(TransferFromCashResponse response)
        {
            return new TransferFromCashResponseDto
            {
                Number = response.Number,
                Date = response.Date.Date,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                CashOrder = MapLinks(response.CashOrder),
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        private static RemoteServiceResponseDto<CashOrderDto> MapLinks(RemoteServiceResponse<CashOrderLink> response)
        {
            return new RemoteServiceResponseDto<CashOrderDto>
            {
                Data = response.Data != null
                    ? new CashOrderDto
                    {
                        DocumentBaseId = response.Data.DocumentBaseId,
                        Number = response.Data.Number,
                        Date = response.Data.Date,
                        Sum = response.Data.Sum
                    }
                    : null,
                Status = response.Status
            };
        }

        public static TransferFromCashSaveRequest Map(TransferFromCashSaveDto saveDto)
        {
            return new TransferFromCashSaveRequest
            {
                DocumentBaseId = 0,
                Date = saveDto.Date.Date,
                Sum = saveDto.Sum,
                Number = saveDto.Number,
                Description = saveDto.Description,
                SettlementAccountId = saveDto.SettlementAccountId,
                ProvideInAccounting = saveDto.ProvideInAccounting ?? true,
                CashOrderBaseId = saveDto.CashOrder?.DocumentBaseId,
            };
        }

        public static TransferFromCashImportRequest Map(ImportTransferFromCash commandData)
        {
            return new TransferFromCashImportRequest
            {
                Date = commandData.Date.Date,
                Sum = commandData.Sum,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.Imported,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static TransferFromCashImportRequest Map(ImportDuplicateTransferFromCash commandData)
        {
            return new TransferFromCashImportRequest
            {
                Date = commandData.Date.Date,
                Sum = commandData.Sum,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.Duplicate,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        internal static TransferFromCashSaveRequest ToSaveRequest(this ConfirmTransferFromCashDto dto)
        {
            return new TransferFromCashSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Number = dto.Number,
                Description = dto.Description,
                SettlementAccountId = dto.SettlementAccountId,
                Date = dto.Date,
                Sum = dto.Sum,
                OperationState = OperationState.OutsourceApproved,
                ProvideInAccounting = true,
                OutsourceState = null,
            };
        }
    }
}