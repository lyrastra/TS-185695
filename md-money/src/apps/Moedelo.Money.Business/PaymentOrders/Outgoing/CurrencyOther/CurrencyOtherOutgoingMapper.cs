using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyOther.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther
{
    public static class CurrencyOtherOutgoingMapper
    {
        public static CurrencyOtherOutgoingDto MapToDto(CurrencyOtherOutgoingSaveRequest request)
        {
            return new CurrencyOtherOutgoingDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                TotalSum = request.TotalSum,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapContractorRequisitesToDto(request.Contractor),
                Description = request.Description,
                IncludeNds = request.IncludeNds,
                NdsType = request.NdsType,
                NdsSum = request.NdsSum,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        public static AccPosting[] MapToPostings(CurrencyOtherOutgoingCustomAccPosting posting, int creditCode)
        {
            return new[]
            {
                new AccPosting
                {
                    Date = posting.Date,
                    Sum = posting.Sum,
                    DebitCode = posting.DebitCode,
                    DebitSubconto = posting.DebitSubconto,
                    CreditCode = creditCode,
                    CreditSubconto = new[] { new Subconto { Id = posting.CreditSubconto } },
                    Description = posting.Description
                }
            };
        }

        public static CurrencyOtherOutgoingResponse MapToResponse(CurrencyOtherOutgoingDto dto)
        {
            return new CurrencyOtherOutgoingResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                TotalSum = dto.TotalSum,
                SettlementAccountId = dto.SettlementAccountId,
                Contactor = KontragentMapper.MapContractorRequisites(dto.Contractor),
                Description = dto.Description,
                IncludeNds = dto.IncludeNds,
                NdsType = dto.NdsType,
                NdsSum = dto.NdsSum,
                ProvideInAccounting = dto.ProvideInAccounting,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
            };
        }

        public static CurrencyOtherOutgoingCreatedMessage MapToCreatedMessage(CurrencyOtherOutgoingSaveRequest request)
        {
            return new CurrencyOtherOutgoingCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapContractorWithRequisitesToKafka(request.Contractor),
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                TotalSum = request.TotalSum,
                Description = request.Description,
                Nds = request.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.NdsSum,
                        NdsType = request.NdsType
                    }
                    : null,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                IsSaveNumeration = request.IsSaveNumeration
            };
        }

        public static CurrencyOtherOutgoingUpdatedMessage MapToUpdatedMessage(CurrencyOtherOutgoingSaveRequest request)
        {
            return new CurrencyOtherOutgoingUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapContractorWithRequisitesToKafka(request.Contractor),
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                TotalSum = request.TotalSum,
                Description = request.Description,
                Nds = request.IncludeNds
                    ? new Kafka.Abstractions.Models.Nds
                    {
                        NdsSum = request.NdsSum,
                        NdsType = request.NdsType
                    }
                    : null,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        internal static CurrencyOtherOutgoingDeletedMessage MapToDeletedMessage(CurrencyOtherOutgoingResponse response, long? newDocumentBaseId)
        {
            return new CurrencyOtherOutgoingDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Contactor?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
    }
}