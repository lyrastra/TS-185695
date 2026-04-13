using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing;

namespace Moedelo.Money.PaymentOrders.Api.Mappers.PaymentOrders.Outgoing
{
    internal static class DeductionMapper
    {
        public static DeductionDto Map(PaymentOrderResponse model)
        {
            var result = new DeductionDto
            {
                Date = model.PaymentOrder.Date,
                Description = model.PaymentOrder.Description,
                Kbk = model.PaymentOrderSnapshot.Kbk,
                Number = model.PaymentOrder.Number,
                Oktmo = model.PaymentOrderSnapshot.Recipient.Oktmo,
                Sum = model.PaymentOrder.Sum,
                Uin = model.PaymentOrderSnapshot.CodeUin,
                DeductionWorkerDocumentNumber = model.PaymentOrderSnapshot.BudgetaryDocNumber,
                DuplicateId = model.PaymentOrder.DuplicateId,
                DeductionWorkerId = model.PaymentOrder.SalaryWorkerId,
                DeductionWorkerInn = model.PaymentOrder.SalaryWorkerId.HasValue ? model.PaymentOrderSnapshot.Payer.Inn : null,
                IsPaid = model.PaymentOrder.PaidStatus == PaymentStatus.Payed,
                OperationState = model.PaymentOrder.OperationState,
                PaymentPriority = model.PaymentOrder.PaymentPriority,
                DocumentBaseId = model.PaymentOrder.DocumentBaseId,
                IsBudgetaryDebt = model.PaymentOrder.SalaryWorkerId.HasValue,
                ProvideInAccounting = model.PaymentOrder.ProvideInAccounting,
                SettlementAccountId = model.PaymentOrder.SettlementAccountId,
                SourceFileId = model.PaymentOrder.SourceFileId,
                OutsourceState = model.PaymentOrder.OutsourceState,
                PayerStatus = model.PaymentOrderSnapshot.BudgetaryPayerStatus
            };

            result.Contractor = KontragentRequisitesMapper.MapToKontragent(
                model.PaymentOrder.KontragentId,
                model.PaymentOrderSnapshot.Recipient);

            return result;
        }

        public static PaymentOrderSaveRequest Map(DeductionDto dto)
        {
            var result = new PaymentOrderSaveRequest
            {
                DocumentBaseId = dto.DocumentBaseId,
                BudgetaryFields = new BudgetaryFields
                {
                    CodeUin = dto.Uin,
                    Kbk = dto.Kbk,
                    PayerStatus = dto.PayerStatus,
                    DocNumber = dto.DeductionWorkerDocumentNumber
                },
                DeductionFields = new DeductionFields
                {
                    PayerInn = dto.DeductionWorkerInn,
                },
                PaymentOrder = new PaymentOrder
                {
                    Date = dto.Date.Date,
                    Number = dto.Number,
                    Sum = dto.Sum,
                    SalaryWorkerId = dto.DeductionWorkerId,
                    SettlementAccountId = dto.SettlementAccountId,
                    Description = dto.Description,
                    ProvideInAccounting = dto.ProvideInAccounting,

                    Direction = MoneyDirection.Outgoing,
                    OperationType = OperationType.PaymentOrderOutgoingDeduction,
                    OrderType = BankDocType.PaymentOrder,
                    PaidStatus = dto.IsPaid ? PaymentStatus.Payed : PaymentStatus.NotPayed,
                    PaymentPriority = dto.PaymentPriority,

                    PostingsAndTaxMode = ProvidePostingType.ByHand,
                    TaxPostingType = ProvidePostingType.Auto,

                    SourceFileId = dto.SourceFileId,
                    DuplicateId = dto.DuplicateId,
                    OperationState = dto.OperationState,
                    OutsourceState = dto.OutsourceState,
                },
            };
            
            if (dto.OperationState != OperationState.MissingKontragent)
            {
                result.PaymentOrder.KontragentId = dto.Contractor.Id;
                result.PaymentOrder.KontragentName = dto.Contractor.Name;

                result.KontragentRequisites = new KontragentRequisites
                {
                    Inn = dto.Contractor.Inn,
                    Kpp = dto.Contractor.Kpp,
                    Name = dto.Contractor.Name,
                    BankBik = dto.Contractor.BankBik,
                    BankName = dto.Contractor.BankName,
                    SettlementAccount = dto.Contractor.SettlementAccount,
                    BankCorrespondentAccount = dto.Contractor.BankCorrespondentAccount,
                    Oktmo = dto.Oktmo
                };
            }

            return result;
        }
    }
}