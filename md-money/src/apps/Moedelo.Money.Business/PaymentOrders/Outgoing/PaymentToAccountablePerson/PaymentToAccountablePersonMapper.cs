using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Domain.Payroll;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Events;
using Moedelo.Money.PaymentOrders.Dto;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;
using System;
using System.Linq;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    internal static class PaymentToAccountablePersonMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(PaymentToAccountablePersonSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(PaymentToAccountablePersonWithMissingEmployeeSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static PaymentToAccountablePersonDto MapToDto(PaymentToAccountablePersonSaveRequest request)
        {
            return new PaymentToAccountablePersonDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Worker = request.OperationState != OperationState.MissingWorker
                    ? new WorkerDto
                    {
                        Id = request.Employee.Id,
                        Name = request.Employee.Name
                    }
                    : null,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                TaxPostingType = request.TaxPostings.ProvidePostingType,
                IsPaid = request.IsPaid,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                IsIgnoreNumber = request.IsIgnoreNumber,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static PaymentToAccountablePersonWithMissingEmployeeDto MapToDto(PaymentToAccountablePersonWithMissingEmployeeSaveRequest request)
        {
            return new PaymentToAccountablePersonWithMissingEmployeeDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                IsIgnoreNumber = request.IsIgnoreNumber,
                PayeeAccount = request.PayeeAccount,
                PayeeInn = request.PayeeInn,
                PayeeName = request.PayeeName,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static PaymentToAccountablePersonResponse MapToResponse(PaymentToAccountablePersonDto dto)
        {
            return new PaymentToAccountablePersonResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Employee = dto.OperationState != OperationState.MissingWorker
                    ? new Employee
                    {
                        Id = dto.Worker.Id,
                        Name = dto.Worker.Name
                    }
                    : null,
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                IsPaid = dto.IsPaid,
                TaxPostingsInManualMode = dto.TaxPostingType == ProvidePostingType.ByHand,
                DuplicateId = dto.DuplicateId,
                OperationState = dto.OperationState,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static PaymentToAccountablePersonSaveRequest MapToSaveRequest(PaymentToAccountablePersonWithMissingEmployeeSaveRequest request)
        {
            return new PaymentToAccountablePersonSaveRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Employee = new Employee(),
                Description = request.Description,
                ProvideInAccounting = true,
                DuplicateId = request.DuplicateId,
                OperationState = OperationState.MissingWorker,
                IsPaid = true,
                AdvanceStatementBaseIds = Array.Empty<long>(),
                TaxPostings = new TaxPostingsData(),
                OutsourceState = request.OutsourceState,
            };
        }

        internal static PaymentToAccountablePersonSaveRequest MapToSaveRequest(PaymentToAccountablePersonResponse response)
        {
            return new PaymentToAccountablePersonSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Employee = response.Employee,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                DuplicateId = response.DuplicateId,
                OperationState = response.OperationState,
                IsPaid = response.IsPaid,
                AdvanceStatementBaseIds = response.AdvanceStatements.GetOrThrow()?.Select(x => x.DocumentBaseId).ToArray() ?? [],
                TaxPostings = new TaxPostingsData(),
                OutsourceState = response.OutsourceState,
            };
        }

        internal static PaymentToAccountablePersonSaveRequest MapToSaveRequest(PaymentToAccountablePersonImportRequest request)
        {
            return new PaymentToAccountablePersonSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Employee = MapEmployee(request),
                Description = request.Description,
                AdvanceStatementBaseIds = Array.Empty<long>(),
                TaxPostings = new TaxPostingsData(),
                ProvideInAccounting = true,
                IsPaid = true,
                DuplicateId = request.DuplicateId,
                OperationState = request.OperationState,
                SourceFileId = request.SourceFileId,
                IsIgnoreNumber = request.IsIgnoreNumber,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static PaymentToAccountablePersonWithMissingEmployeeSaveRequest MapToMissingSaveRequest(PaymentToAccountablePersonImportRequest request)
        {
            return new PaymentToAccountablePersonWithMissingEmployeeSaveRequest
            {
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Description = request.Description,
                DuplicateId = request.DuplicateId,
                SourceFileId = request.SourceFileId,
                IsIgnoreNumber = request.IsIgnoreNumber,
                PayeeAccount = request.PayeeAccount,
                PayeeInn = request.PayeeInn,
                PayeeName = request.PayeeName,
                OutsourceState = request.OutsourceState,
            };
        }

        private static Employee MapEmployee(PaymentToAccountablePersonImportRequest request)
        {
            return request.OperationState != OperationState.MissingWorker
                ? new Employee
                {
                    Id = request.EmployeeId.Value,
                    Name = request.EmployeeName
                }
                : null;
        }

        internal static PaymentToAccountablePersonCreated MapToCreatedMessage(PaymentToAccountablePersonSaveRequest request)
        {
            return new PaymentToAccountablePersonCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingWorker
                    ? new Kafka.Abstractions.Models.Contractor
                    {
                        Id = request.Employee.Id,
                        Name = request.Employee.Name,
                        Type = ContractorType.Worker
                    }
                    : null,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                AdvanceStatementBaseIds = request.AdvanceStatementBaseIds,
                IsPaid = request.IsPaid,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static PaymentToAccountablePersonUpdated MapToUpdatedMessage(PaymentToAccountablePersonSaveRequest request)
        {
            return new PaymentToAccountablePersonUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = new Kafka.Abstractions.Models.Contractor
                {
                    Id = request.Employee.Id,
                    Name = request.Employee.Name,
                    Type = ContractorType.Worker
                },
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                AdvanceStatementBaseIds = request.AdvanceStatementBaseIds,
                IsPaid = request.IsPaid,
                IsManualTaxPostings = request.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static PaymentToAccountablePersonProvideRequired MapToProvideRequired(PaymentToAccountablePersonResponse response)
        {
            return new PaymentToAccountablePersonProvideRequired
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = new Kafka.Abstractions.Models.Contractor
                {
                    Id = response.Employee.Id,
                    Name = response.Employee.Name,
                    Type = ContractorType.Worker
                },
                Sum = response.Sum,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                AdvanceStatementBaseIds = response.AdvanceStatements.GetOrThrow()?.Select(x => x.DocumentBaseId).ToArray() ?? Array.Empty<long>(),
                IsPaid = response.IsPaid,
                IsManualTaxPostings = response.TaxPostingsInManualMode
            };
        }

        internal static PaymentToAccountablePersonDeleted MapToDeleted(PaymentToAccountablePersonResponse response, long? newDocumentBaseId)
        {
            return new PaymentToAccountablePersonDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            PaymentToAccountablePersonSaveRequest request)
        {
            return new CustomTaxPostingsOverwriteRequest
            {
                Description = request.Description,
                Postings = request.TaxPostings,
                DocumentDate = request.Date,
                DocumentNumber = request.Number,
                DocumentBaseId = request.DocumentBaseId
            };
        }
    }
}
