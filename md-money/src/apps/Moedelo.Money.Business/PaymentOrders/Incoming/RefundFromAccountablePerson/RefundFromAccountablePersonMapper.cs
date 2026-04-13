using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Domain.Payroll;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson.Events;
using Moedelo.Money.PaymentOrders.Dto;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    internal static class RefundFromAccountablePersonMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(RefundFromAccountablePersonSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static RefundFromAccountablePersonDto MapToDto(RefundFromAccountablePersonSaveRequest request)
        {
            return new RefundFromAccountablePersonSaveDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Worker = new WorkerDto
                {
                    Id = request.Employee.Id,
                    Name = request.Employee.Name
                },
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                MissingWorkerRequisites = Map(request.MissingWorkerRequisites)
            };
        }

        private static MissingWorkerRequisitesDto Map(MissingWorkerRequisitesSaveRequest request)
        {
            return request == null
                ? null
                : new MissingWorkerRequisitesDto
                {
                    Name = request.Name,
                    Inn = request.Inn,
                    SettlementNumber = request.SettlementNumber
                };
        }

        internal static RefundFromAccountablePersonSaveRequest MapToSaveRequest(RefundFromAccountablePersonResponse response)
        {
            return new RefundFromAccountablePersonSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Employee = new Employee
                {
                    Id = response.Employee?.Id ?? 0,
                    Name = response.Employee?.Name
                },
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                AdvanceStatementBaseId = response.AdvanceStatement.GetOrThrow()?.DocumentBaseId,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
            };
        }

        internal static RefundFromAccountablePersonResponse MapToResponse(RefundFromAccountablePersonDto dto)
        {
            return new RefundFromAccountablePersonResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Employee = new Employee
                {
                    Id = dto.Worker.Id,
                    Name = dto.Worker.Name
                },
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static RefundFromAccountablePersonCreated MapToCreatedMessage(RefundFromAccountablePersonSaveRequest request)
        {
            return new RefundFromAccountablePersonCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = new Kafka.Abstractions.Models.Contractor
                {
                    Id = request.Employee.Id,
                    Name = request.Employee.Name,
                    Type = Enums.ContractorType.Worker
                },
                Description = request.Description,
                Sum = request.Sum,
                AdvanceStatementBaseId = request.AdvanceStatementBaseId,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static RefundFromAccountablePersonUpdated MapToUpdatedMessage(RefundFromAccountablePersonSaveRequest request)
        {
            return new RefundFromAccountablePersonUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = new Kafka.Abstractions.Models.Contractor
                {
                    Id = request.Employee.Id,
                    Name = request.Employee.Name,
                    Type = Enums.ContractorType.Worker
                },
                Description = request.Description,
                Sum = request.Sum,
                AdvanceStatementBaseId = request.AdvanceStatementBaseId,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static RefundFromAccountablePersonProvideRequired MapToProvideRequiredEvent(RefundFromAccountablePersonResponse response)
        {
            return new RefundFromAccountablePersonProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = new Kafka.Abstractions.Models.Contractor
                {
                    Id = response.Employee.Id,
                    Name = response.Employee.Name,
                    Type = Enums.ContractorType.Worker
                },
                Description = response.Description,
                Sum = response.Sum,
                AdvanceStatementBaseId = response.AdvanceStatement.GetOrThrow()?.DocumentBaseId,
                ProvideInAccounting = response.ProvideInAccounting
            };
        }

        internal static RefundFromAccountablePersonDeleted MapToDeletedMessage(RefundFromAccountablePersonResponse response, long? newDocumentBaseId)
        {
            return new RefundFromAccountablePersonDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        public static RefundFromAccountablePersonSaveRequest MapToSaveRequest(RefundFromAccountablePersonImportRequest request)
        {
            return new RefundFromAccountablePersonSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Employee = MapEmployee(request),
                Description = request.Description,
                Sum = request.Sum,
                ProvideInAccounting = true,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                MissingWorkerRequisites = request.MissingWorkerRequisites,

                DocumentBaseId = 0,
                AdvanceStatementBaseId = null,

                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }
        
        private static Employee MapEmployee(RefundFromAccountablePersonImportRequest request)
        {
            if (request.OperationState == OperationState.MissingWorker)
            {
                return new Employee
                {
                    Id = 0,
                    Name = request.EmployeeName
                };
            }
            
            return new Employee
            {
                Id = request.EmployeeId!.Value,
                Name = request.EmployeeName
            };
        }
    }
}
