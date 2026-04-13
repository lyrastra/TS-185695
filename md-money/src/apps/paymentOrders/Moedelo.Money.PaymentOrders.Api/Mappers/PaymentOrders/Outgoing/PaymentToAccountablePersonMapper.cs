using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using Moedelo.Money.PaymentOrders.Dto;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class PaymentToAccountablePersonMapper
    {
        public static PaymentToAccountablePersonDto Map(PaymentOrderResponse model)
        {
            return new PaymentToAccountablePersonDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                Sum = model.PaymentOrder.Sum,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,
                Worker = model.PaymentOrder.OperationState != OperationState.MissingWorker
                    ? new WorkerDto
                    {
                        Id = model.PaymentOrder.SalaryWorkerId ?? 0,
                        Name = model.PaymentOrder.KontragentName
                    }
                    : null,
                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                PostingsAndTaxMode = model.PaymentOrder.PostingsAndTaxMode,
                TaxPostingType = model.PaymentOrder.TaxPostingType,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                IsPaid = model.PaymentOrder.PaidStatus == PaymentStatus.Payed
            };
        }

        public static PaymentOrderSaveRequest Map(PaymentToAccountablePersonDto dto)
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
                    SalaryWorkerId = IsOmitWorker(dto) ? null : dto.Worker.Id,
                    KontragentName = IsOmitWorker(dto) ? null : dto.Worker.Name,
                    Description = dto.Description,
                    ProvideInAccounting = dto.ProvideInAccounting,
                    TaxPostingType = dto.TaxPostingType,
                    PaidStatus = dto.IsPaid ? PaymentStatus.Payed : PaymentStatus.NotPayed,

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingPaymentToAccountablePerson,
                    OrderType = BankDocType.PaymentOrder,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    IsIgnoreNumber = dto.IsIgnoreNumber,
                    OutsourceState = dto.OutsourceState,
                },
            };
        }
        
        private static bool IsOmitWorker(PaymentToAccountablePersonDto dto)
        {
            return dto.OperationState.IsBadOperationState() && (dto.Worker == null || dto.Worker.Id == 0);
        }

        public static PaymentOrderSaveRequest Map(PaymentToAccountablePersonWithMissingEmployeeDto employeeDto)
        {
            return new PaymentOrderSaveRequest
            {
                DocumentBaseId = employeeDto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = employeeDto.Date.Date,
                    Number = employeeDto.Number,
                    Sum = employeeDto.Sum,
                    SettlementAccountId = employeeDto.SettlementAccountId,
                    SalaryWorkerId = null,
                    KontragentName = employeeDto.PayeeName,
                    Description = employeeDto.Description,
                    ProvideInAccounting = true,
                    TaxPostingType = ProvidePostingType.Auto,
                    PaidStatus = PaymentStatus.Payed,

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingPaymentToAccountablePerson,
                    OrderType = BankDocType.PaymentOrder,

                    SourceFileId = employeeDto.SourceFileId,
                    DuplicateId = employeeDto.DuplicateId,
                    OperationState = OperationState.MissingWorker,
                    IsIgnoreNumber = employeeDto.IsIgnoreNumber,
                },
                KontragentRequisites = new KontragentRequisites
                {
                    Inn = employeeDto.PayeeInn,
                    Name = employeeDto.PayeeName,
                    SettlementAccount = employeeDto.PayeeAccount,
                },
            };
        }
    }
}
