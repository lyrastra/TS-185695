using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Events;
using Moedelo.Money.PaymentOrders.Dto;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargePayments;
using System.Collections.Generic;
using System.Linq;
using WorkerDto = Moedelo.Money.PaymentOrders.Dto.WorkerDto;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    internal static class PaymentToNaturalPersonsMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(PaymentToNaturalPersonsSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.EmployeePayments.SelectMany(x => x.ChargePayments).Sum(x => x.Sum)
            };
        }

        internal static PaymentToNaturalPersonsResponse MapToResponse(PaymentToNaturalPersonsDto dto)
        {
            return new PaymentToNaturalPersonsResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                EmployeePayments = dto.WorkerPayments.Select(MapToEmployeePayment).ToArray(),
                PaymentType = dto.PaymentType,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsPaid = dto.IsPaid,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static PaymentToNaturalPersonsSaveRequest MapToSaveRequest(PaymentToNaturalPersonsResponse dto)
        {
            return new PaymentToNaturalPersonsSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                EmployeePayments = MapToEmployeePaymentsSaveModel(dto.EmployeePayments),
                PaymentType = dto.PaymentType,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsPaid = dto.IsPaid,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState
            };
        }

        internal static PaymentToNaturalPersonsSaveRequest MapToSaveRequest(PaymentToNaturalPersonsImportRequest request, List<ChargePayment> chargePayments)
        {
            return new PaymentToNaturalPersonsSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                EmployeePayments = MapEmployeePayments(request, chargePayments),
                PaymentType = request.PaymentType,
                ProvideInAccounting = true,
                IsPaid = true,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                ImportRuleIds = request.ImportRuleId.HasValue ? [request.ImportRuleId.Value] : [],
                ImportLogId = request.ImportLogId,
            };
        }

        private static EmployeePaymentsSaveModel[] MapToEmployeePaymentsSaveModel(IReadOnlyCollection<EmployeePayments> employeePayments)
        {
            return employeePayments.Select(x =>
                new EmployeePaymentsSaveModel
                {
                    Id = x.Id,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.EmployeeName,
                    ChargePayments = x.ChargePayments
                }).ToArray();
        }

        private static EmployeePaymentsSaveModel[] MapEmployeePayments(PaymentToNaturalPersonsImportRequest request, List<ChargePayment> chargePayments)
        {
            var saveModel = new EmployeePaymentsSaveModel
            {
                EmployeeId = request.EmployeeId ?? 0,
                EmployeeName = request.EmployeeName,
                ChargePayments = chargePayments,
            };

            if (request.OperationState == OperationState.MissingWorker)
            {
                saveModel.EmployeeId = 0;
                saveModel.PayeeInn = request.PayeeInn;
                saveModel.PayeeAccount = request.PayeeAccount;
                saveModel.PayeeName = request.PayeeName;
            }
            
            return new[] { saveModel };
        }

        internal static PaymentToNaturalPersonsDto MapToDto(PaymentToNaturalPersonsSaveRequest request)
        {
            var dto = new PaymentToNaturalPersonsDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                WorkerPayments = request.EmployeePayments.Select(MapToDto).ToArray(),
                PaymentType = request.PaymentType,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId
            };
            // fixme: костыль для старого кода
            if (dto.WorkerPayments.Count == 1)
            {
                var payment = dto.WorkerPayments.First();
                dto.Worker = new WorkerDto
                {
                    Id = payment.WorkerId,
                    Name = payment.WorkerName
                };
            }
            return dto;
        }

        internal static PaymentToNaturalPersonsWithMissingEmployeeDto MapToMissingEmployeeDto(PaymentToNaturalPersonsSaveRequest request)
        {
            var missingWorker = request.EmployeePayments.First();
            
            return new PaymentToNaturalPersonsWithMissingEmployeeDto()
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                PaymentSum = missingWorker.ChargePayments.First().Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                PayeeInn = missingWorker.PayeeInn,
                PayeeAccount = missingWorker.PayeeAccount,
                PayeeName = missingWorker.PayeeName,
                OutsourceState = request.OutsourceState
            };
        }

        internal static PaymentToNaturalPersonsCreated MapToCreatedMessage(PaymentToNaturalPersonsSaveRequest request)
        {
            return new PaymentToNaturalPersonsCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                EmployeePayments = request.EmployeePayments.Select(MapToKafka).ToArray(),
                PaymentType = request.PaymentType,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static PaymentToNaturalPersonsUpdated MapToUpdatedMessage(PaymentToNaturalPersonsSaveRequest request)
        {
            return new PaymentToNaturalPersonsUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                EmployeePayments = request.EmployeePayments.Select(MapToKafka).ToArray(),
                PaymentType = request.PaymentType,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                IsPaid = request.IsPaid,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsPaidStatusChanged = request.IsPaidStatusChanged!.Value
            };
        }

        internal static PaymentToNaturalPersonsProvideRequired MapToProvideRequired(PaymentToNaturalPersonsResponse response)
        {
            return new PaymentToNaturalPersonsProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                EmployeePayments = response.EmployeePayments.Select(MapToKafka).ToArray(),
                PaymentType = response.PaymentType,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid
            };
        }

        internal static PaymentToNaturalPersonsDeleted MapToDeleted(PaymentToNaturalPersonsResponse response, long? newDocumentBaseId)
        {
            return new PaymentToNaturalPersonsDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        private static EmployeePayments MapToEmployeePayment(WorkerChargePaymentDto x)
        {
            return new EmployeePayments
            {
                Id = x.Id,
                Sum = x.Sum,
                EmployeeId = x.WorkerId
            };
        }

        private static WorkerChargePaymentDto MapToDto(EmployeePaymentsSaveModel payment)
        {
            return new WorkerChargePaymentDto
            {
                Id = payment.Id,
                WorkerId = payment.EmployeeId,
                WorkerName = payment.EmployeeName,
                Sum = payment.ChargePayments.Sum(s => s.Sum)
            };
        }

        private static Kafka.Abstractions.Models.EmployeePayments MapToKafka(EmployeePaymentsSaveModel employeePayments)
        {
            return new Kafka.Abstractions.Models.EmployeePayments
            {
                EmployeeId = employeePayments.EmployeeId,
                EmployeeName = employeePayments.EmployeeName,
                ChargePayments = employeePayments.ChargePayments.Select(MapToKafka).ToArray()
            };
        }

        private static Kafka.Abstractions.Models.EmployeePayments MapToKafka(EmployeePayments employeePayments)
        {
            return new Kafka.Abstractions.Models.EmployeePayments
            {
                EmployeeId = employeePayments.EmployeeId,
                EmployeeName = employeePayments.EmployeeName,
                ChargePayments = employeePayments.ChargePayments.Select(MapToKafka).ToArray()
            };
        }

        private static Kafka.Abstractions.Models.ChargePayment MapToKafka(ChargePayment chargePayment)
        {
            return new Kafka.Abstractions.Models.ChargePayment
            {
                ChargeId = chargePayment.ChargeId,
                ChargePaymentId = chargePayment.ChargePaymentId,
                Sum = chargePayment.Sum,
                Description = chargePayment.Description
            };
        }

        public static WorkerChargePaymentsListDto MapToPayrollChargePayments(PaymentToNaturalPersonsSaveRequest request)
        {
            var chargePayments = MapWorkerPaymentsToPayrollDto(request.EmployeePayments);
            return new WorkerChargePaymentsListDto
            {
                DocumentBaseId = request.DocumentBaseId,
                DocumentDate = request.Date,
                WorkerChargePayments = chargePayments,
                IsPaid = request.IsPaid
            };
        }

        private static WorkerChargePaymentsDto[] MapWorkerPaymentsToPayrollDto(IReadOnlyCollection<EmployeePaymentsSaveModel> employeePayments)
        {
            return employeePayments.Select(x => new WorkerChargePaymentsDto
            {
                WorkerId = x.EmployeeId,
                ChargePayments = MapChargePaymentsToPayrollDto(x)
            }).Where(c => c.ChargePayments.Any(cp => cp.ChargeId > 0)).ToArray();
        }

        private static ChargePaymentDto[] MapChargePaymentsToPayrollDto(EmployeePaymentsSaveModel payments)
        {
            return payments.ChargePayments
                .Where(c => c.ChargeId > 0)
                .Select(MapChargePaymentToPayrollDto)
                .ToArray();
        }

        private static ChargePaymentDto MapChargePaymentToPayrollDto(ChargePayment chargePayment)
        {
            return new ChargePaymentDto
            {
                ChargeId = chargePayment.ChargeId,
                ChargePaymentId = chargePayment.ChargePaymentId > 0
                    ? chargePayment.ChargePaymentId
                    : null,
                Sum = chargePayment.Sum,
                Description = chargePayment.Description
            };
        }
    }
}
