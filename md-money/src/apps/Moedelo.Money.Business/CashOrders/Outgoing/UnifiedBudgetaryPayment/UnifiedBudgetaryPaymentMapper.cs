using Moedelo.Money.CashOrders.Dto.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment.Events;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    internal static class UnifiedBudgetaryPaymentMapper
    {
        internal static BaseDocumentCreateRequest MapToBaseDocumentCreateRequest(UnifiedBudgetaryPaymentSaveRequest request)
        {
            return new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
        }

        internal static UnifiedBudgetaryPaymentDto MapToDto(UnifiedBudgetaryPaymentSaveRequest request)
        {
            return new UnifiedBudgetaryPaymentDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                CashId = request.CashId,
                Sum = request.Sum,
                Recipient = request.Recipient,
                Destination = request.Destination,
                SubPayments = MapSubPaymentsToDto(request.SubPayments),
                ProvideInAccounting = request.ProvideInAccounting,
                PostingsAndTaxMode = request.PostingsAndTaxMode,
            };
        }

        internal static UnifiedBudgetaryPaymentResponse MapToResponse(UnifiedBudgetaryPaymentDto dto)
        {
            return new UnifiedBudgetaryPaymentResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                CashId = dto.CashId,
                Sum = dto.Sum,
                Recipient = dto.Recipient,
                Destination = dto.Destination,
                SubPayments = MapSubPaymentsToResponseModel(dto.SubPayments),
                ProvideInAccounting = dto.ProvideInAccounting,
                PostingsAndTaxMode = dto.PostingsAndTaxMode
            };
        }

        internal static UnifiedBudgetaryPaymentSaveResponse MapToResponse(UnifiedBudgetaryPaymentUpdateResponseDto dto)
        {
            return new UnifiedBudgetaryPaymentSaveResponse
            {
                DeletedSubPaymentDocumentIds = dto.DeletedSubPaymentDocumentIds
            };
        }

        internal static UnifiedBudgetaryPaymentDeleteResponse MapToResponse(UnifiedBudgetaryPaymentDeleteResponseDto dto)
        {
            return new UnifiedBudgetaryPaymentDeleteResponse
            {
                DeletedSubPaymentDocumentIds = dto.DeletedSubPaymentDocumentIds
            };
        }

        private static UnifiedBudgetarySubPaymentResponseModel[] MapSubPaymentsToResponseModel(
            IReadOnlyCollection<UnifiedBudgetarySubPaymentDto> subPayments)
        {
            return subPayments.Select(x =>
                new UnifiedBudgetarySubPaymentResponseModel
                {
                    DocumentBaseId = x.DocumentBaseId,
                    Kbk = new UnifiedBudgetaryKbkResponseModel
                    {
                        Id = x.KbkId
                    },
                    PatentId = x.PatentId,
                    Period = new BudgetaryPeriod
                    {
                        Type = x.Period.Type,
                        Year = x.Period.Year,
                        Number = x.Period.Number,
                        Date = x.Period.Date
                    },
                    Sum = x.Sum,
                    TaxPostingsInManualMode = x.TaxPostingType == Enums.ProvidePostingType.ByHand
                }).ToArray();
        }

        private static UnifiedBudgetarySubPaymentDto[] MapSubPaymentsToDto(
            IReadOnlyCollection<UnifiedBudgetarySubPaymentSaveModel> subPayments)
        {
            return subPayments.Select(x =>
                new UnifiedBudgetarySubPaymentDto
                {
                    DocumentBaseId = x.DocumentBaseId,
                    KbkId = x.KbkId,
                    PatentId = x.PatentId,
                    Period = new UnifiedBudgetaryPeriodDto
                    {
                        Type = x.Period.Type,
                        Year = x.Period.Year,
                        Number = x.Period.Number,
                        Date = x.Period.Date
                    },
                    Sum = x.Sum,
                    TaxPostingType = x.TaxPostings.ProvidePostingType
                }).ToArray();
        }

        internal static UnifiedBudgetaryPaymentCreated MapToCreatedMessage(UnifiedBudgetaryPaymentSaveRequest request)
        {
            return new UnifiedBudgetaryPaymentCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                CashId = request.CashId,
                Sum = request.Sum,
                Recipient = request.Recipient,
                Destination = request.Destination,
                SubPayments = MapSubPaymentsToEventModel(request.SubPayments)
            };
        }

        internal static UnifiedBudgetaryPaymentUpdated MapToUpdatedMessage(
            UnifiedBudgetaryPaymentSaveRequest request,
            OperationType oldOperationType,
            IReadOnlyCollection<long> deletedSubPaymentDocumentIds)
        {
            return new UnifiedBudgetaryPaymentUpdated
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                CashId = request.CashId,
                Sum = request.Sum,
                Recipient = request.Recipient,
                Destination = request.Destination,
                SubPayments = MapSubPaymentsToEventModel(request.SubPayments),
                OldOperationType = oldOperationType,
                DeletedSubPaymentDocumentIds = deletedSubPaymentDocumentIds
            };
        }

        internal static UnifiedBudgetaryPaymentDeleted MapToDeleted(
            UnifiedBudgetaryPaymentResponse response,
            IReadOnlyCollection<long> deletedSubPaymentDocumentIds)
        {
            return new UnifiedBudgetaryPaymentDeleted
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                DeletedSubPaymentDocumentIds = deletedSubPaymentDocumentIds
            };
        }

        internal static CustomTaxPostingsOverwriteRequest MapToCustomTaxPostingsOverwriteRequest(
            UnifiedBudgetaryPaymentSaveRequest request,
            UnifiedBudgetarySubPaymentSaveModel subPayment)
        {
            return new CustomTaxPostingsOverwriteRequest
            {
                //Description = request.Description,
                Postings = subPayment.TaxPostings,
                DocumentDate = request.Date,
                DocumentNumber = request.Number,
                DocumentBaseId = subPayment.DocumentBaseId
            };
        }

        private static Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment.Events.Models.UnifiedBudgetarySubPayment[]
            MapSubPaymentsToEventModel(IReadOnlyCollection<UnifiedBudgetarySubPaymentSaveModel> subPayments)
        {
            return subPayments.Select(x =>
                new Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment.Events.Models.UnifiedBudgetarySubPayment
                {
                    DocumentBaseId = x.DocumentBaseId,
                    KbkId = x.KbkId,
                    Period = new Kafka.Abstractions.Models.BudgetaryPeriod
                    {
                        Type = x.Period.Type,
                        Year = x.Period.Year,
                        Number = x.Period.Number,
                        Date = x.Period.Date
                    },
                    Sum = x.Sum,
                    PatentId = x.PatentId,
                    IsManualTaxPostings = x.TaxPostings.ProvidePostingType == ProvidePostingType.ByHand
                }).ToArray();
        }
    }
}
