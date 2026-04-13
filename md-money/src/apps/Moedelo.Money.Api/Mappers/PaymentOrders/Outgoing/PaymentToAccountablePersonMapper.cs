using System;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Domain.Payroll;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Api.Models.TaxPostings;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentDetail.Dto;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Outgoing;
using Moedelo.Money.Resources;
using Moedelo.TaxPostings.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing
{
    public static class PaymentToAccountablePersonMapper
    {
        public static PaymentToAccountablePersonResponseDto Map(PaymentToAccountablePersonResponse response)
        {
            return new PaymentToAccountablePersonResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Employee = response.OperationState != OperationState.MissingWorker
                    ? new EmployeeResponseDto
                    {
                        Id = response.Employee.Id,
                        Name = response.Employee.Name
                    }
                    : null,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                AdvanceStatements = MapAdvanceStatement(response.AdvanceStatements),
                IsPaid = response.IsPaid,
                IsReadOnly = response.IsReadOnly,
                TaxPostingsInManualMode = response.TaxPostingsInManualMode,
                IsFromImport = response.IsFromImport
            };
        }

        public static PaymentToAccountablePersonSaveRequest Map(PaymentToAccountablePersonCreateDto dto)
        {
            return new PaymentToAccountablePersonSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Employee = new Employee()
                {
                    Id = dto.Employee.Id,
                    Name = dto.Employee.Name
                },
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.PaymentToAccountablePerson
                    : dto.Description,
                ProvideInAccounting = dto.IsPaid && (dto.ProvideInAccounting ?? true),
                AdvanceStatementBaseIds = dto.AdvanceStatements.Select(x => x.DocumentBaseId).ToArray(),
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
                IsPaid = dto.IsPaid,
                IsSaveNumeration = dto.IsSaveNumeration
            };
        }

        public static PaymentToAccountablePersonSaveRequest Map(PaymentToAccountablePersonUpdateDto dto)
        {
            return new PaymentToAccountablePersonSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Employee = new Employee()
                {
                    Id = dto.Employee.Id,
                    Name = dto.Employee.Name
                },
                Description = string.IsNullOrWhiteSpace(dto.Description)
                    ? PaymentOrdersDescriptions.PaymentToAccountablePerson
                    : dto.Description,
                ProvideInAccounting = dto.IsPaid && (dto.ProvideInAccounting ?? true),
                TaxPostings = TaxPostingsMapper.MapTaxPostings(dto.TaxPostings, TaxPostingDirection.Outgoing),
                IsPaid = dto.IsPaid
            };
        }
        
        public static PaymentDetailDto MapToPaymentDetail(PaymentToAccountablePersonResponse response)
        {
            return new PaymentDetailDto
            {
                EmployeeId = response.Employee.Id,
                Amount = response.Sum,
                Number = response.Number
            };
        }

        private static Models.RemoteServiceResponseDto<AdvanceStatementResponseDto[]> MapAdvanceStatement(RemoteServiceResponse<IReadOnlyCollection<AdvanceStatementLink>> response)
        {
            return new Models.RemoteServiceResponseDto<AdvanceStatementResponseDto[]>
            {
                Data = response?.Data?.Select(MapAdvanceStatement).ToArray(),
                Status = response.Status
            };
        }

        private static AdvanceStatementResponseDto MapAdvanceStatement(AdvanceStatementLink advanceStatement)
        {
            if (advanceStatement == null)
            {
                return null;
            }

            return new AdvanceStatementResponseDto
            {
                DocumentBaseId = advanceStatement.DocumentBaseId,
                Number = advanceStatement.Number,
                Date = advanceStatement.Date,
                Sum = advanceStatement.Sum
            };
        }

        public static PaymentToAccountablePersonSaveRequest ToSaveRequest(this ConfirmPaymentToAccountablePersonDto dto)
        {
            return new PaymentToAccountablePersonSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Employee = new Employee
                {
                    Id = dto.Contractor.Id,
                    Name = dto.Contractor.Name,
                },
                Sum = dto.Sum,
                Description = dto.Description,
                ProvideInAccounting = true,
                IsPaid = true,
                OperationState = OperationState.OutsourceApproved,
                TaxPostings = TaxPostingsMapper.MapTaxPostings((TaxPostingsSaveDto)null, TaxPostingDirection.Outgoing),
                IsIgnoreNumber = true,
                AdvanceStatementBaseIds = Array.Empty<long>(),
                OutsourceState = null,
            };
        }
    }
}
