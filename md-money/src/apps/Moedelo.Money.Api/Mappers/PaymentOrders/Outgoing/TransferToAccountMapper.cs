using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Commands;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class TransferToAccountMapper
    {
        public static TransferToAccountResponseDto Map(TransferToAccountResponse response)
        {
            return new TransferToAccountResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                ToSettlementAccountId = response.ToSettlementAccountId,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        public static TransferToAccountSaveRequest Map(TransferToAccountSaveDto dto)
        {
            return new TransferToAccountSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                ToSettlementAccountId = dto.ToSettlementAccountId,
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.TransferToAccount
                    : dto.Description,
                ProvideInAccounting = dto.IsPaid ? dto.ProvideInAccounting ?? true : false,
                IsPaid = dto.IsPaid,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        public static TransferToAccountSaveResponseDto Map(TransferToAccountSaveResponse response)
        {
            return new TransferToAccountSaveResponseDto
            {
                DocumentBaseId = response.DocumentBaseId,
                TransferFromAccountBaseId = response.TransferFromAccountBaseId
            };
        }

        public static TransferToAccountImportRequest Map(ImportTransferToAccount commandData)
        {
            return new TransferToAccountImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                ToSettlementAccountId = commandData.ToSettlementAccountId,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.Imported,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static TransferToAccountImportRequest Map(ImportDuplicateTransferToAccount commandData)
        {
            return new TransferToAccountImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                ToSettlementAccountId = commandData.ToSettlementAccountId,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
                DuplicateId = commandData.DuplicateId,
                OperationState = OperationState.Duplicate,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static TransferToAccountApplyIgnoreNumberRequest Map(ApplyIgnoreNumberTransferToAccount commandData)
        {
            return new TransferToAccountApplyIgnoreNumberRequest
            {
                DocumentBaseIds = commandData.DocumentBaseIds,
                ImportRuleId = commandData.ImportRuleId
            };
        }
        
        public static PaymentDetailDto MapToPaymentDetail(TransferToAccountResponse response)
        {
            return new PaymentDetailDto
            {
                PayeeSettlementAccountId = response.ToSettlementAccountId,
                Amount = response.Sum,
                Number = response.Number
            };
        }

        internal static TransferToAccountSaveRequest ToSaveRequest(this ConfirmTransferToAccountDto dto)
        {
            return new TransferToAccountSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                ProvideInAccounting = true,
                IsPaid = true,
                ToSettlementAccountId = dto.ToSettlementAccountId,
                OutsourceState = null,
                OperationState = OperationState.OutsourceApproved,
            };
        }
    }
}
