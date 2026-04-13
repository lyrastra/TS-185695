using Moedelo.Money.Domain.PaymentOrders.Snapshot;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Snapshot;

namespace Moedelo.Money.Business.PaymentOrders.Snapshot
{
    internal static class PaymentOrderSnapshotMapper
    {
        public static PaymentOrderSnapshotResponse Map(this PaymentOrderSnapshotDto model)
        {
            return new PaymentOrderSnapshotResponse
            {
                Payer = model.Payer.Map(),
                Recipient = model.Recipient.Map(),
                PaymentNumber = model.PaymentNumber,
                OrderDate = model.OrderDate,
                Sum = model.Sum,
                Direction = model.Direction,
                Purpose = model.Purpose,
                PaymentPriority = model.PaymentPriority,
                BudgetaryPayerStatus = model.BudgetaryPayerStatus,
                Kbk = model.Kbk,
                BudgetaryOkato = model.BudgetaryOkato,
                BudgetaryPaymentBase = model.BudgetaryPaymentBase,
                BudgetaryPeriod = model.BudgetaryPeriod.Map(),
                BudgetaryDocNumber = model.BudgetaryDocNumber,
                BudgetaryDocDate = model.BudgetaryDocDate,
                BudgetaryPaymentType = model.BudgetaryPaymentType,
                KindOfPay = model.KindOfPay,
                NumberTop = model.NumberTop,
                BankDocType = model.BankDocType,
                OrderType = model.OrderType,
                CodeUin = model.CodeUin,
            };
        }

        private static OrderDetailsResponse Map(this OrderDetailsDto model)
        {
            return new OrderDetailsResponse
            {
                Name = model.Name,
                Inn = model.Inn,
                Kpp = model.Kpp,
                SettlementNumber = model.SettlementNumber,
                BankName = model.BankName,
                BankBik = model.BankBik,
                BankCorrespondentAccount = model.BankCorrespondentAccount,
                BankCity = model.BankCity,
                IsOoo = model.IsOoo,
                KontragentType = model.KontragentType,
                Address = model.Address,
                Okato = model.Okato,
                Oktmo = model.Oktmo,
            };
        }

        private static BudgetaryPeriodResponse Map(this BudgetaryPeriodDto model)
        {
            if (model == null)
            {
                return null;
            }

            return new BudgetaryPeriodResponse
            {
                Date = model.Date,
                Type = model.Type,
                Number = model.Number,
                Year = model.Year,
            };
        }
    }
}
