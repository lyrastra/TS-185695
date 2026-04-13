using Moedelo.Money.Api.Models;
using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.Api.Models.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models.Incoming;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Domain.Payroll;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Incoming
{
    public static class RefundFromAccountablePersonMapper
    {
        public static RefundFromAccountablePersonResponseDto Map(RefundFromAccountablePersonResponse response)
        {
            return new RefundFromAccountablePersonResponseDto
            {
                Date = response.Date.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Employee = new EmployeeResponseDto
                {
                    Id = response.Employee.Id,
                    Name = response.Employee.Name
                },
                AdvanceStatement = MapAdvanceStatementResponse(response.AdvanceStatement),
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                IsReadOnly = response.IsReadOnly,
                IsFromImport = response.IsFromImport
            };
        }

        public static RefundFromAccountablePersonSaveRequest Map(RefundFromAccountablePersonSaveDto dto)
        {
            return new RefundFromAccountablePersonSaveRequest
            {
                DocumentBaseId = 0,
                Date = dto.Date.Date,
                Number = dto.Number,
                SettlementAccountId = dto.SettlementAccountId,
                Employee = new Employee
                {
                    Id = dto.Employee.Id,
                    Name = dto.Employee.Name
                },
                AdvanceStatementBaseId = dto.AdvanceStatement?.DocumentBaseId,
                Sum = dto.Sum,
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting ?? true,
            };
        }

        private static RemoteServiceResponseDto<AdvanceStatementResponseDto> MapAdvanceStatementResponse(RemoteServiceResponse<AdvanceStatementLink> response)
        {
            return new RemoteServiceResponseDto<AdvanceStatementResponseDto>
            {
                Data = MapAdvanceStatement(response.Data),
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
                Date = advanceStatement.Date
            };
        }

        public static RefundFromAccountablePersonSaveRequest ToSaveRequest(this ConfirmRefundFromAccountablePersonDto dto)
        {
            return new RefundFromAccountablePersonSaveRequest
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
                AdvanceStatementBaseId = null,
                Sum = dto.Sum,
                Description = dto.Description,
                ProvideInAccounting = true,
                OperationState = OperationState.OutsourceApproved,
                OutsourceState = null,
            };
        }
    }
}
