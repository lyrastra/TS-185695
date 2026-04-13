using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Dto;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System;
using System.Linq;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class PaymentToNaturalPersonsMapper
    {
        public static PaymentToNaturalPersonsDto Map(PaymentOrderResponse model)
        {
            var response = new PaymentToNaturalPersonsDto
            {
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                Date = model.PaymentOrder.Date.Date,
                Number = model.PaymentOrder.Number,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                Description = model.PaymentOrder.Description,
                PaymentType = model.PaymentOrder.UnderContract ?? PaymentToNaturalPersonsType.WorkContract,

                SourceFileId = model.PaymentOrder.SourceFileId,
                OperationState = model.PaymentOrder.OperationState,
                DuplicateId = model.PaymentOrder.DuplicateId,
                OutsourceState = model.PaymentOrder.OutsourceState,

                PostingsAndTaxMode = model.PaymentOrder.PostingsAndTaxMode,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                IsPaid = model.PaymentOrder.PaidStatus == PaymentStatus.Payed
            };
            response.Worker = MapEmployeeToResponse(model);
            response.WorkerPayments = model.ChargePayments.Select(MapWorkerPayment).ToArray();
            return response;
        }

        public static PaymentOrderSaveRequest Map(PaymentToNaturalPersonsDto dto)
        {
            return new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.WorkerPayments.Sum(x => x.Sum),
                    SettlementAccountId = dto.SettlementAccountId,
                    Description = dto.Description,
                    UnderContract = dto.PaymentType,
                    SalaryWorkerId = GetWorkerId(dto),
                    KontragentName = GetWorkerName(dto),
                    ProvideInAccounting = dto.ProvideInAccounting,
                    PaidStatus = dto.IsPaid ? PaymentStatus.Payed : PaymentStatus.NotPayed,
                    PaymentPriority = GetPaymentPriority(dto.PaymentType),

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingPaymentToNaturalPersons,
                    OrderType = BankDocType.PaymentOrder,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,
                },
                ChargePayments = dto.OperationState != OperationState.MissingWorker
                    ? MapChargePayments(dto)
                    : Array.Empty<WorkerPayment>(),
            };
        }

        public static PaymentOrderSaveRequest Map(PaymentToNaturalPersonsWithMissingEmployeeDto dto)
        {
            return new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.PaymentSum,
                    SettlementAccountId = dto.SettlementAccountId,
                    Description = dto.Description,

                    KontragentName = dto.PayeeName,
                    ProvideInAccounting = dto.ProvideInAccounting,
                    PaidStatus = PaymentStatus.Payed,
                    PaymentPriority = PaymentPriority.Third,

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingPaymentToNaturalPersons,
                    OrderType = BankDocType.PaymentOrder,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = OperationState.MissingWorker,
                    OutsourceState = dto.OutsourceState,
                },
                KontragentRequisites = new KontragentRequisites
                {
                    Inn = dto.PayeeInn,
                    Name = dto.PayeeName,
                    SettlementAccount = dto.PayeeAccount,
                },
                ChargePayments = Array.Empty<WorkerPayment>(),
            };
        }

        private static WorkerDto MapEmployeeToResponse(PaymentOrderResponse model)
        {
            if (model.PaymentOrder.UnderContract == PaymentToNaturalPersonsType.WorkContract &&
                model.ChargePayments.Count > 1)
            {
                return null;
            }
            if (model.PaymentOrder.SalaryWorkerId == null)
            {
                return null;
            }
            return new WorkerDto
            {
                Id = model.PaymentOrder.SalaryWorkerId.Value,
                Name = model.PaymentOrder.KontragentName
            };
        }

        private static int? GetWorkerId(PaymentToNaturalPersonsDto dto)
        {
            if (dto.OperationState == OperationState.MissingWorker)
            {
                return null;
            }
            return dto.PaymentType.IsSalaryProject()
                ? null
                : dto.Worker?.Id;
        }

        private static string GetWorkerName(PaymentToNaturalPersonsDto dto)
        {
            if (dto.OperationState == OperationState.MissingWorker)
            {
                return null;
            }
            return dto.PaymentType.IsSalaryProject()
                ? null
                : dto.Worker?.Name;
        }

        private static WorkerChargePaymentDto MapWorkerPayment(WorkerPayment payment)
        {
            return new WorkerChargePaymentDto
            {
                Id = payment.Id,
                WorkerId = payment.WorkerId,
                Sum = payment.Sum
            };
        }

        private static WorkerPayment[] MapChargePayments(PaymentToNaturalPersonsDto dto)
        {
            if (dto.WorkerPayments == null || dto.WorkerPayments.Count == 0)
            {
                return Array.Empty<WorkerPayment>();
            }

            if (dto.Worker != null)
            {
                return dto.WorkerPayments
                    .Where(x => x.WorkerId == dto.Worker.Id)
                    .Select(MapWorkerPayment)
                    .ToArray();
            }

            return dto.WorkerPayments.Select(MapWorkerPayment).ToArray();
        }

        private static WorkerPayment MapWorkerPayment(WorkerChargePaymentDto payment)
        {
            return new WorkerPayment
            {
                Id = payment.Id,
                WorkerId = payment.WorkerId,
                Sum = payment.Sum
            };
        }

        private static PaymentPriority GetPaymentPriority(this PaymentToNaturalPersonsType type)
        {
            if (type == PaymentToNaturalPersonsType.Gpd ||
                type == PaymentToNaturalPersonsType.Dividends ||
                type == PaymentToNaturalPersonsType.GpdBySalaryProject ||
                type == PaymentToNaturalPersonsType.DividendsBySalaryProject)
            {
                return PaymentPriority.Fifth;
            }

            return PaymentPriority.Third;
        }
    }
}
