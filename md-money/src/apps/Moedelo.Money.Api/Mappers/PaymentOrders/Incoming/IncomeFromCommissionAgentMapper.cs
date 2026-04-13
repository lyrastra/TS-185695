using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class IncomeFromCommissionAgentMapper
    {
        public static IncomeFromCommissionAgentResponseDto Map(IncomeFromCommissionAgentResponse response)
        {
            return new IncomeFromCommissionAgentResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = KontragentMapper.MapToDto(response.Kontragent),
                Contract = LinkedDocumentsMapper.MapContractResponse(response.Contract),
                Description = response.Description,
                Nds = MapNds(response),
                ProvideInAccounting = response.ProvideInAccounting,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        private static NdsResponseDto MapNds(IncomeFromCommissionAgentResponse operation)
        {
            return operation.IncludeNds
                ? new NdsResponseDto
                {
                    IncludeNds = operation.IncludeNds,
                    Type = operation.NdsType,
                    Sum = operation.NdsSum ?? 0
                }
                : null;
        }

        public static IncomeFromCommissionAgentSaveRequest Map(IncomeFromCommissionAgentSaveDto dto)
        {
            return new IncomeFromCommissionAgentSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = dto.Contract.DocumentBaseId ?? 0L,
                Description = dto.Description,
                IncludeNds = dto.Nds?.IncludeNds ?? false,
                NdsType = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Type
                    : null,
                NdsSum = dto.Nds?.IncludeNds ?? false
                    ? dto.Nds?.Sum
                    : default,
                ProvideInAccounting = dto.ProvideInAccounting ?? true
            };
        }

        public static IncomeFromCommissionAgentSaveRequest ToSaveRequest(this ConfirmIncomeFromCommissionAgentDto dto)
        {
            return new IncomeFromCommissionAgentSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(dto.Contractor),
                ContractBaseId = 0,
                Description = dto.Description,
                IncludeNds = dto.Nds?.IncludeNds == true,
                NdsType = dto.Nds?.IncludeNds == true ? dto.Nds?.Type : null,
                NdsSum = dto.Nds?.IncludeNds == true ? dto.Nds?.Sum : null,
                ProvideInAccounting = true,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null
            };
        }

        public static CommissionAgentAutocompleteResponseDto Map(CommissionAgentWithRequisites model)
        {
            return new CommissionAgentAutocompleteResponseDto
            {
                Id = model.Id,
                Inn = model.Inn,
                Kpp = model.Kpp,
                Name = model.Name,
                KontragentId = model.KontragentId,
                SettlementAccount = model.SettlementAccount,
                BankBik = model.BankBik,
                BankName = model.BankName
            };
        }
    }
}
