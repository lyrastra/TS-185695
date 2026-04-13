using Moedelo.Money.Api.Models.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount.Commands;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class CurrencyTransferFromAccountMapper
    {
        public static CurrencyTransferFromAccountResponseDto Map(CurrencyTransferFromAccountResponse response)
        {
            return new CurrencyTransferFromAccountResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.Sum,
                Description = response.Description,
                FromSettlementAccountId = response.FromSettlementAccountId,
                IsReadOnly = response.IsReadOnly,
                ProvideInAccounting = response.ProvideInAccounting,
                IsFromImport = response.IsFromImport
            };
        }

        public static CurrencyTransferFromAccountImportRequest Map(ImportCurrencyTransferFromAccount commandData)
        {
            return new CurrencyTransferFromAccountImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                FromSettlementAccountId = commandData.FromSettlementAccountId,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.Imported,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static CurrencyTransferFromAccountImportRequest Map(ImportDuplicateCurrencyTransferFromAccount commandData)
        {
            return new CurrencyTransferFromAccountImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                FromSettlementAccountId = commandData.FromSettlementAccountId,
                SourceFileId = commandData.SourceFileId,
                DuplicateId = commandData.DuplicateId,
                OperationState = OperationState.Duplicate,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static CurrencyTransferFromAccountImportRequest Map(ImportWithMissingCurrencySettlementAccountCurrencyTransferFromAccount commandData)
        {
            return new CurrencyTransferFromAccountImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                SourceFileId = commandData.SourceFileId,
                OperationState = OperationState.MissingCurrencySettlementAccount,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        internal static CurrencyTransferFromAccountSaveRequest ToSaveRequest(this ConfirmCurrencyTransferFromAccountDto dto)
        {
            return new CurrencyTransferFromAccountSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Sum = dto.Sum,
                Description = dto.Description,
                FromSettlementAccountId = dto.FromSettlementAccountId,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
                ProvideInAccounting = true,
            };
        }
    }
}