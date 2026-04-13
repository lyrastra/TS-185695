using System;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.Enums;
using Moedelo.BankIntegrations.Models.PaymentOrder;
using Moedelo.Finances.Domain.Enums.Money.Operations.PaymentOrders;

namespace Moedelo.Finances.Business.Mappers.Integrations
{
    public static class PaymentOrderMapper
    {
        public static PaymentOrder Map(this PaymentOrderDto po)
        {
            return new PaymentOrder
            {
                Guid = po.Guid == Guid.Empty ? Guid.NewGuid() : po.Guid,
                CodeUin = po.CodeUin,
                BudgetaryPaymentBase = (BudgetaryPaymentBase)po.BudgetaryPaymentBase,
                BudgetaryPaymentType = (BudgetaryPaymentType)po.BudgetaryPaymentType,
                BudgetaryPeriod = new BudgetaryPeriod
                {
                    Date = po.BudgetaryPeriod.Date,
                    Type = (BudgetaryPeriodType) po.BudgetaryPeriod.Type,
                    Number = po.BudgetaryPeriod.Number,
                    Year = po.BudgetaryPeriod.Year
                },
                BudgetaryDocNumber = po.BudgetaryDocNumber,
                Purpose = po.Purpose,
                PurposeCode = po.PurposeCode,
                Kbk = po.Kbk,
                PaymentNumber = po.PaymentNumber,
                BudgetaryDocDate = po.BudgetaryDocDate,
                BankDocType = (BankDocType)po.BankDocType,
                BudgetaryOkato = po.BudgetaryOkato,
                BudgetaryPayerStatus = (BudgetaryPayerStatus)po.BudgetaryPayerStatus,
                Direction = (PaymentDirection)po.Direction,
                KindOfPay = po.KindOfPay,
                NumberTop = po.NumberTop,
                OrderDate = po.OrderDate,
                OrderType = (OrderType)po.OrderType,
                PaymentPriority = (PaymentPriority)po.PaymentPriority,
                Sum = po.Sum,
                NdsType = (NdsType)po.NdsType,
                NdsSum = po.NdsSum,
                Payer = new OrderDetails
                {
                    SettlementNumber = po.Payer.SettlementNumber,
                    Inn = po.Payer.Inn,
                    BankName = po.Payer.BankName,
                    BankBik = po.Payer.BankBik,
                    Kpp = po.Payer.Kpp,
                    Okato = po.Payer.Okato,
                    Name = po.Payer.Name,
                    BankCorrespondentAccount = po.Payer.BankCorrespondentAccount,
                    Address = po.Payer.Address,
                    BankCity = po.Payer.BankCity,
                    IsOoo = po.Payer.IsOoo,
                    Oktmo = po.Payer.Oktmo
                },
                Recipient = new OrderDetails
                {
                    SettlementNumber = po.Recipient.SettlementNumber,
                    Inn = po.Recipient.Inn,
                    BankName = po.Recipient.BankName,
                    BankBik = po.Recipient.BankBik,
                    Kpp = po.Recipient.Kpp,
                    Okato = po.Recipient.Okato,
                    Name = po.Recipient.Name,
                    BankCorrespondentAccount = po.Recipient.BankCorrespondentAccount,
                    Address = po.Recipient.Address,
                    BankCity = po.Recipient.BankCity,
                    IsOoo = po.Recipient.IsOoo,
                    Oktmo = po.Recipient.Oktmo
                }
            };
        }
        
        public static NdsType MapNdsType(PaymentOrderNdsType paymentType)
        {
            if (paymentType == PaymentOrderNdsType.None)
                return NdsType.WithoutNds;

            var name = paymentType.ToString();

            // Убираем хвост вида "To120"
            var baseName = name.Split(new[] { "To" }, StringSplitOptions.None)[0];

            return Enum.TryParse<NdsType>(baseName, out var parsed) 
                ? parsed 
                : NdsType.UnknownNds;
        }
    }
}