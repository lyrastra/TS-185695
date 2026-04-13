using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class OtherOutgoingMapper
    {
        public static OtherOutgoingDto Map(PaymentOrderResponse model)
        {
            var result = new OtherOutgoingDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,

                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                TaxPostingType = model.PaymentOrder.TaxPostingType,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                IsPaid = model.PaymentOrder.PaidStatus == PaymentStatus.Payed,
                
                IncludeNds = model.PaymentOrder.IncludeNds,
                NdsType = model.PaymentOrder.NdsType,
                NdsSum = model.PaymentOrder.NdsSum
            };

            var contractorId = model.PaymentOrder.SalaryWorkerId ?? model.PaymentOrder.KontragentId;
            var contractorType = GetContractorType(model);

            result.Contractor = KontragentRequisitesMapper.MapToContractor(
                contractorId,
                contractorType,
                model.PaymentOrderSnapshot.Recipient);

            return result;
        }

        private static ContractorType GetContractorType(PaymentOrderResponse model)
        {
            if (model.PaymentOrder.SalaryWorkerId.HasValue)
            {
                return ContractorType.Worker;
            }
            
            if (model.PaymentOrder.KontragentId.HasValue)
            {
                return ContractorType.Kontragent;
            }
                
            return ContractorType.Ip;
        }

        public static PaymentOrderSaveRequest Map(OtherOutgoingDto dto)
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

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingOther,
                    OrderType = BankDocType.PaymentOrder,
                    PaidStatus = dto.IsPaid ? PaymentStatus.Payed : PaymentStatus.NotPayed,
                    KontragentAccountCode = 600200,

                    PostingsAndTaxMode = ProvidePostingType.ByHand,
                    TaxPostingType = ProvidePostingType.ByHand,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,
                },
            };

            if (dto.OperationState != OperationState.MissingKontragent)
            {
                if (dto.Contractor.Type == ContractorType.Worker)
                {
                    result.PaymentOrder.SalaryWorkerId = dto.Contractor.Id;
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
