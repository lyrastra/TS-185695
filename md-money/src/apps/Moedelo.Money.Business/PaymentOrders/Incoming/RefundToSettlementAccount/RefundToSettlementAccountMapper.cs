using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount.Events;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Incoming;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount
{
    internal static class RefundToSettlementAccountMapper
    {
        internal static RefundToSettlementAccountDto MapToDto(RefundToSettlementAccountSaveRequest request)
        {
            return new RefundToSettlementAccountDto
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapContractorRequisitesToDto(request.Contractor)
                    : null,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                // Поля из прочего поступления для Возврата на расчетный счет пока сокрыты
                //IncludeNds = request.IncludeNds,
                //NdsType = request.NdsType,
                //NdsSum = request.NdsSum,
                DuplicateId = request.DuplicateId,
                TaxationSystemType = request.TaxationSystemType,
                PatentId = request.PatentId,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId
            };
        }

        internal static AccPosting[] MapToPostings(RefundToSettlementAccountCustomAccPosting posting)
        {
            return new[]
            {
                new AccPosting
                {
                    Date = posting.Date,
                    Sum = posting.Sum,
                    DebitCode = posting.DebitCode,
                    DebitSubconto = new[] { new Subconto { Id = posting.DebitSubconto } },
                    CreditCode = posting.CreditCode,
                    CreditSubconto = posting.CreditSubconto,
                    Description = posting.Description
                }
            };
        }

        internal static RefundToSettlementAccountResponse MapToResponse(RefundToSettlementAccountDto dto)
        {
            return new RefundToSettlementAccountResponse
            {
                DocumentBaseId = dto.DocumentBaseId,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                SettlementAccountId = dto.SettlementAccountId,
                Contractor = KontragentMapper.MapContractorRequisites(dto.Contractor),
                Description = dto.Description,
                ProvideInAccounting = dto.ProvideInAccounting,
                // Поля из прочего поступления для Возврата на расчетный счет пока сокрыты
                //IncludeNds = dto.IncludeNds,
                //NdsType = dto.NdsType,
                //NdsSum = dto.NdsSum,
                DuplicateId = dto.DuplicateId,
                IsFromImport = !string.IsNullOrEmpty(dto.SourceFileId),
                TaxationSystemType = dto.TaxationSystemType,
                PatentId = dto.PatentId,
                OperationState = dto.OperationState,
                OutsourceState = dto.OutsourceState,
            };
        }

        internal static RefundToSettlementAccountSaveRequest MapToSaveRequest(RefundToSettlementAccountResponse response)
        {
            return new RefundToSettlementAccountSaveRequest
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                Sum = response.Sum,
                SettlementAccountId = response.SettlementAccountId,
                Contractor = response.Contractor,
                Description = response.Description,
                ProvideInAccounting = response.ProvideInAccounting,
                DuplicateId = response.DuplicateId,
                TaxationSystemType = response.TaxationSystemType,
                PatentId = response.PatentId,
                OperationState = response.OperationState,
                OutsourceState = response.OutsourceState,
                // слудующие поля нужно загружать из разных частей сервиса
                TaxPostings = new TaxPostingsData(),
                AccPosting = null,
                ContractBaseId = null,
            };
        }

        internal static RefundToSettlementAccountCreatedMessage MapToCreatedMessage(RefundToSettlementAccountSaveRequest request)
        {
            return new RefundToSettlementAccountCreatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.OperationState != OperationState.MissingKontragent
                    ? KontragentMapper.MapContractorWithRequisitesToKafka(request.Contractor)
                    : null,
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                // Поля из прочего поступления для Возврата на расчетный счет пока сокрыты
                //BillLinks = GetBillLinks(request),
                //Nds = request.IncludeNds
                //    ? new Kafka.Abstractions.Models.Nds
                //    {
                //        NdsSum = request.NdsSum,
                //        NdsType = request.NdsType
                //    }
                //    : null,
                TaxationSystemType = request.TaxationSystemType,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        internal static RefundToSettlementAccountUpdatedMessage MapToUpdatedMessage(RefundToSettlementAccountSaveRequest request)
        {
            return new RefundToSettlementAccountUpdatedMessage
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = KontragentMapper.MapContractorWithRequisitesToKafka(request.Contractor),
                ContractBaseId = request.ContractBaseId,
                Sum = request.Sum,
                Description = request.Description,
                ProvideInAccounting = request.ProvideInAccounting,
                // Поля из прочего поступления для Возврата на расчетный счет пока сокрыты
                //BillLinks = GetBillLinks(request),
                //Nds = request.IncludeNds
                //    ? new Kafka.Abstractions.Models.Nds
                //    {
                //        NdsSum = request.NdsSum,
                //        NdsType = request.NdsType
                //    }
                //    : null,
                TaxationSystemType = request.TaxationSystemType,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState
            };
        }

        internal static RefundToSettlementAccountDeletedMessage MapToDeletedMessage(RefundToSettlementAccountResponse response, long? newDocumentBaseId)
        {
            return new RefundToSettlementAccountDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Contractor?.Id ?? default,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }
        // Поля из прочего поступления для Возврата на расчетный счет пока сокрыты
        //private static Kafka.Abstractions.Models.BillLink[] GetBillLinks(RefundToSettlementAccountSaveRequest payment)
        //{
        //    return payment.BillLinks
        //        .Select(billLink => new Kafka.Abstractions.Models.BillLink
        //        {
        //            BillBaseId = billLink.BillBaseId,
        //            LinkSum = billLink.LinkSum
        //        }).ToArray() ?? Array.Empty<Kafka.Abstractions.Models.BillLink>();
        //}

        internal static RefundToSettlementAccountSaveRequest MapToSaveRequest(ImportRefundToSettlementAccountRequest request)
        {
            return new RefundToSettlementAccountSaveRequest()
            {
                DocumentBaseId = request.DocumentBaseId,
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum,
                Description = request.Description,
                SettlementAccountId = request.SettlementAccountId,
                Contractor = request.Contractor,
                ContractBaseId = request.ContractBaseId,
                ProvideInAccounting = true,
                TaxPostings = new TaxPostingsData() { ProvidePostingType = ProvidePostingType.ByHand },
                // Поля из прочего поступления для Возврата на расчетный счет пока сокрыты
                //BillLinks = Array.Empty<BillLinkSaveRequest>(),
                //IncludeNds = false, // У депозита нет НДС
                //NdsType = Enums.NdsType.None,
                //NdsSum = 0.0m,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                SourceFileId = request.SourceFileId,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }
    }
}
