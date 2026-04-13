using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Incoming
{
    internal static class CurrencyOtherIncomingMapper
    {
        public static CurrencyOtherIncomingDto Map(PaymentOrderResponse model)
        {
            var result = new CurrencyOtherIncomingDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                TotalSum = model.PaymentOrder.TotalSum.GetValueOrDefault(),
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,

                IncludeNds = model.PaymentOrder.IncludeNds,
                NdsType = model.PaymentOrder.NdsType,
                NdsSum = model.PaymentOrder.NdsSum,

                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                OperationState = model.PaymentOrder.OperationState,
                OutsourceState = model.PaymentOrder.OutsourceState,
            };

            var contractorId = model.PaymentOrder.SalaryWorkerId ?? model.PaymentOrder.KontragentId;

            result.Contractor = KontragentRequisitesMapper.MapToContractor(
                contractorId,
                GetContractorType(model),
                model.PaymentOrderSnapshot.Payer);

            return result;
        }

        public static PaymentOrderSaveRequest Map(CurrencyOtherIncomingDto dto)
        {
            var result = new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.Sum,
                    TotalSum = dto.TotalSum,
                    SettlementAccountId = dto.SettlementAccountId,
                    KontragentName = dto.Contractor.Name,
                    KontragentId = dto.Contractor.Type == ContractorType.Kontragent ? dto.Contractor.Id : default(int?),
                    SalaryWorkerId = dto.Contractor.Type == ContractorType.Worker ? dto.Contractor.Id : default(int?),
                    Description = dto.Description,

                    IncludeNds = dto.IncludeNds,
                    NdsType = dto.NdsType,
                    NdsSum = dto.NdsSum,

                    Direction = MoneyDirection.Incoming,
                    OperationType = OperationType.PaymentOrderIncomingCurrencyOther,
                    OrderType = BankDocType.PaymentOrder,
                    PaidStatus = PaymentStatus.Payed,
                    KontragentAccountCode = 620200,

                    ProvideInAccounting = dto.ProvideInAccounting,
                    PostingsAndTaxMode = ProvidePostingType.ByHand,
                    TaxPostingType = ProvidePostingType.ByHand,
                    OutsourceState = dto.OutsourceState,
                    OperationState = dto.OperationState,
                },
            };

            if (dto.Contractor.Type == ContractorType.Worker)
            {
                result.PaymentOrder.SalaryWorkerId = dto.Contractor.Id;
            }
            else if (dto.Contractor.Type == ContractorType.Kontragent)
            {
                result.PaymentOrder.KontragentId = dto.Contractor.Id;
            }

            result.KontragentRequisites = KontragentRequisitesMapper.Map(dto.Contractor);
            return result;
        }

        private static ContractorType GetContractorType(PaymentOrderResponse model)
        {
            if (model.PaymentOrder.SalaryWorkerId.HasValue)
            {
                return ContractorType.Worker;
            }

            return model.PaymentOrder.KontragentId.HasValue ? ContractorType.Kontragent : ContractorType.Ip;
        }
    }
}
