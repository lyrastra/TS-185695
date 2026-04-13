using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyOther.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyOther
{
    internal static class CurrencyOtherIncomingMapper
    {
        public static CurrencyOtherIncomingDto MapToDto(CurrencyOtherIncomingSaveRequest request)
        {
            return new CurrencyOtherIncomingDto
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

        public static AccPosting[] MapToPostings(CurrencyOtherIncomingCustomAccPosting posting, int debitCode)
        {
            return new[]
            {
                new AccPosting
                {
                    Date = posting.Date,
                    Sum = posting.Sum,
                    DebitCode = debitCode,
                    DebitSubconto = new[] { new Subconto { Id = posting.DebitSubconto } },
                    CreditCode = posting.CreditCode,
                    CreditSubconto = posting.CreditSubconto,
                    Description = posting.Description
                }
            };
        }

        public static CurrencyOtherIncomingResponse MapToResponse(CurrencyOtherIncomingDto dto)
        {
            return new CurrencyOtherIncomingResponse
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

        public static CurrencyOtherIncomingCreatedMessage MapToCreatedMessage(CurrencyOtherIncomingSaveRequest request)
        {
            return new CurrencyOtherIncomingCreatedMessage
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
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }
        public static CurrencyOtherIncomingUpdatedMessage MapToUpdatedMessage(CurrencyOtherIncomingSaveRequest request)
        {
            return new CurrencyOtherIncomingUpdatedMessage
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
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        public static CurrencyOtherIncomingDeletedMessage MapToDeletedMessage(CurrencyOtherIncomingResponse response, long? newDocumentBaseId)
        {
            return new CurrencyOtherIncomingDeletedMessage
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
