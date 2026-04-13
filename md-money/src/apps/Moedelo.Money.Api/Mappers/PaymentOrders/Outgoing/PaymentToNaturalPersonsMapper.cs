using System.Collections.Generic;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Enums;
using System.Linq;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class PaymentToNaturalPersonsMapper
    {
        public static PaymentToNaturalPersonsResponseDto Map(PaymentToNaturalPersonsResponse response)
        {
            return new PaymentToNaturalPersonsResponseDto
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Sum = response.EmployeePayments.SelectMany(x => x.ChargePayments).Sum(x => x.Sum),
                Description = response.Description,
                EmployeePayments = response.EmployeePayments.Select(MapToDto).ToArray(),
                PaymentType = response.PaymentType,
                ProvideInAccounting = response.ProvideInAccounting,
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly
            };
        }

        public static PaymentToNaturalPersonsSaveRequest Map(PaymentToNaturalPersonsSaveDto dto)
        {
            return new PaymentToNaturalPersonsSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.PaymentToNaturalPersons
                    : dto.Description,
                EmployeePayments = dto.EmployeePayments.Select(MapToDomain).ToArray(),
                PaymentType = dto.PaymentType,
                ProvideInAccounting = dto.IsPaid && (dto.ProvideInAccounting ?? true),
                IsPaid = dto.IsPaid,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        private static EmployeePaymentsResponseDto MapToDto(EmployeePayments payments)
        {
            return new EmployeePaymentsResponseDto
            {
                Employee = new EmployeeResponseDto
                {
                    Id = payments.EmployeeId,
                    Name = payments.EmployeeName
                },
                TaxationSystem = payments.TaxationSystem,
                ChargePayments = payments.ChargePayments.Select(MapToDto).ToArray()
            };
        }

        private static ChargePaymentResponseDto MapToDto(ChargePayment payment)
        {
            return new ChargePaymentResponseDto
            {
                ChargeId = payment.ChargeId,
                ChargePaymentId = payment.ChargePaymentId,
                Sum = payment.Sum,
                Description = payment.Description
            };
        }

        private static EmployeePaymentsSaveModel MapToDomain(EmployeePaymentsSaveDto payments)
        {
            return new EmployeePaymentsSaveModel
            {
                EmployeeId = payments.Employee.Id,
                EmployeeName = payments.Employee.Name,
                ChargePayments = payments.ChargePayments.Select(MapToDomain).ToArray()
            };
        }

        private static ChargePayment MapToDomain(ChargePaymentSaveDto payment)
        {
            return new ChargePayment
            {
                ChargeId = payment.ChargeId ?? 0,
                ChargePaymentId = payment.ChargePaymentId,
                Sum = payment.Sum,
                Description = payment.Description
            };
        }
        
        public static PaymentToNaturalPersonsSaveRequest ToSaveRequest(this ConfirmPaymentToNaturalPersonsDto dto)
        {
            return new PaymentToNaturalPersonsSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Description = dto.Description,
                PaymentType = 0, // импорт ставит это значение в случае "MissingWorker"
                OperationState = dto.Employee.Id > 0
                    ? OperationState.OutsourceApproved
                    : OperationState.MissingWorker,
                EmployeePayments = dto.ToEmployeePayments(),
                OutsourceState = null,
                ProvideInAccounting = true,
                IsPaid = true
            };
        }
        
        private static EmployeePaymentsSaveModel[] ToEmployeePayments(this ConfirmPaymentToNaturalPersonsDto request)
        {
            var saveModel = new EmployeePaymentsSaveModel
            {
                EmployeeId = request.Employee.Id,
                EmployeeName = request.Employee.Name,
                ChargePayments = new List<ChargePayment>
                {
                    new ChargePayment
                    {
                        ChargeId = default,
                        ChargePaymentId = default,
                        Description = request.Description,
                        Sum = request.Sum
                    }
                },
            };

            if (request.Employee.Id == 0)
            {
                saveModel.EmployeeId = 0;
                saveModel.PayeeInn = request.Employee.Inn;
                saveModel.PayeeAccount = request.Employee.SettlementAccount;
                saveModel.PayeeName = request.Employee.Name;
            }
            
            return new[] { saveModel };
        }
    }
}
