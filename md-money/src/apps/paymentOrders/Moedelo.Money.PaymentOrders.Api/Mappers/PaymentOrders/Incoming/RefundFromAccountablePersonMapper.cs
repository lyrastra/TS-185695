using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using Moedelo.Money.PaymentOrders.Dto;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Incoming
{
    internal static class RefundFromAccountablePersonMapper
    {
        public static RefundFromAccountablePersonDto Map(PaymentOrderResponse model)
        {
            return new RefundFromAccountablePersonDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,
                Worker = new WorkerDto
                {
                    Id = model.PaymentOrder.SalaryWorkerId ?? 0,
                    Name = model.PaymentOrder.KontragentName
                },
                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                PostingsAndTaxMode = model.PaymentOrder.PostingsAndTaxMode,
                TaxPostingType = model.PaymentOrder.TaxPostingType,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
            };
        }

        public static PaymentOrderSaveRequest Map(RefundFromAccountablePersonSaveDto dto)
        {
            return new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.Sum,
                    SettlementAccountId = dto.SettlementAccountId,
                    Description = dto.Description,
                    SalaryWorkerId = IsOmitWorker(dto) ? null : dto.Worker.Id,
                    KontragentName = IsOmitWorker(dto) ? null : dto.Worker.Name,
                    ProvideInAccounting = dto.ProvideInAccounting,

                    Direction = MoneyDirection.Incoming,
                    OperationType = OperationType.PaymentOrderIncomingRefundFromAccountablePerson,
                    OrderType = BankDocType.PaymentOrder,
                    PaidStatus = PaymentStatus.Payed,
                    KontragentAccountCode = 620200,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,
                },
                KontragentRequisites = dto.MissingWorkerRequisites == null 
                    ? null
                    : new KontragentRequisites
                    {
                        Name = dto.MissingWorkerRequisites.Name,
                        Inn = dto.MissingWorkerRequisites.Inn,
                        SettlementAccount = dto.MissingWorkerRequisites.SettlementNumber
                    }
            };
        }

        private static bool IsOmitWorker(RefundFromAccountablePersonDto dto)
        {
            return dto.OperationState.IsBadOperationState() && (dto.Worker == null || dto.Worker.Id == 0);
        }
    }
}
