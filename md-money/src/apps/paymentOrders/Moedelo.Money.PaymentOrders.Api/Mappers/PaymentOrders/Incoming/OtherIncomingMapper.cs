using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Incoming
{
    internal static class OtherIncomingMapper
    {
        public static OtherIncomingDto Map(PaymentOrderResponse model)
        {
            var result = new OtherIncomingDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,

                IncludeNds = model.PaymentOrder.IncludeNds,
                NdsType = model.PaymentOrder.NdsType,
                NdsSum = model.PaymentOrder.NdsSum,

                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,

                TaxationSystemType = model.PaymentOrder.TaxationSystemType,
                PatentId = model.PaymentOrder.PatentId,

                IsTargetIncome = model.PaymentOrder.IsTargetIncome ?? false
            };

            var contractorId = model.PaymentOrder.SalaryWorkerId ?? model.PaymentOrder.KontragentId;
            var contractorType = model.PaymentOrder.SalaryWorkerId.HasValue
                    ? ContractorType.Worker
                    : model.PaymentOrder.KontragentId.HasValue
                        ? ContractorType.Kontragent
                        : ContractorType.Ip;

            result.Contractor = KontragentRequisitesMapper.MapToContractor(
                contractorId,
                contractorType,
                model.PaymentOrderSnapshot.Payer);

            return result;
        }

        public static PaymentOrderSaveRequest Map(OtherIncomingDto dto)
        {
            var result = new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.Sum,
                    SettlementAccountId = dto.SettlementAccountId,
                    KontragentName = dto.OperationState != OperationState.MissingKontragent
                        ? dto.Contractor.Name
                        : null,
                    Description = dto.Description,
                    ProvideInAccounting = dto.ProvideInAccounting,

                    IncludeNds = dto.IncludeNds,
                    NdsType = dto.NdsType,
                    NdsSum = dto.NdsSum,

                    Direction = MoneyDirection.Incoming,
                    OperationType = OperationType.PaymentOrderIncomingOther,
                    OrderType = BankDocType.PaymentOrder,
                    PaidStatus = PaymentStatus.Payed,
                    KontragentAccountCode = 620200,

                    TaxationSystemType = dto.TaxationSystemType,
                    PatentId = dto.PatentId,

                    PostingsAndTaxMode = ProvidePostingType.ByHand,
                    TaxPostingType = ProvidePostingType.ByHand,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,

                    IsTargetIncome = dto.IsTargetIncome == true
                        ? dto.IsTargetIncome
                        : null
                },
            };

            if (dto.OperationState != OperationState.MissingKontragent)
            {
                if (dto.Contractor.Type == ContractorType.Worker)
                {
                    result.PaymentOrder.SalaryWorkerId =  dto.Contractor.Id;
                }
                else if (dto.Contractor.Type == ContractorType.Kontragent)
                {
                    result.PaymentOrder.KontragentId = dto.Contractor.Id;
                }
                result.KontragentRequisites = KontragentRequisitesMapper.Map(dto.Contractor);
            }
            return result;
        }
    }
}
