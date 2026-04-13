using Moedelo.Common.Utils.Numbers;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Attributes;
using Moedelo.Money.PaymentOrders.Business.PaymentOrders.Reports.Models;
using Moedelo.Money.PaymentOrders.Domain.Enums;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Reports.Formats
{
    internal static class XlsReportModelGenerator
    {
        public static XlsReportModel Generate(PaymentOrder paymentOrder, PaymentOrderSnapshot snapshot)
        {
            return new XlsReportModel
            {
                Date = paymentOrder.Date.ToString("dd.MM.yyyy"),
                PaymentNumber = $"ПЛАТЕЖНОЕ ПОРУЧЕНИЕ № {paymentOrder.Number}",
                Sum = paymentOrder.Sum.ToString("#0.00").Replace(',', '-'),
                SumInWords = Summa.ToWords(paymentOrder.Sum, Moedelo.Common.Utils.Numbers.Currency.Rub, Moedelo.Common.Utils.Numbers.CapitalLetter.First, true),
                Description = paymentOrder.Description,
                PaymentPriority = GetPaymentPriority(paymentOrder.PaymentPriority),

                Payer = GetPayerName(paymentOrder, snapshot),
                PayerInn = snapshot.Payer.Inn,
                PayerKpp = snapshot.Payer.Kpp,
                PayerSettlementNumber = snapshot.Payer.SettlementNumber,
                PayerBank = snapshot.Payer.BankName,
                PayerBankBik = snapshot.Payer.BankBik,
                PayerCorrespondentAccount = snapshot.Payer.BankCorrespondentAccount,

                Recipient = snapshot.Recipient.Name,
                RecipientInn = snapshot.Recipient.Inn,
                RecipientKpp = snapshot.Recipient.Kpp,
                RecipientSettlementNumber = snapshot.Recipient.SettlementNumber,
                RecipientBank = snapshot.Recipient.BankName,
                RecipientBankBik = snapshot.Recipient.BankBik,
                RecipientCorrespondentAccount = snapshot.Recipient.BankCorrespondentAccount,

                BudgetaryPaymentType = GetBudgetaryPaymentType(paymentOrder.Date, snapshot.BudgetaryPaymentType),
                BudgetaryPayerStatus = GetBudgetaryPaymentStatus(snapshot.BudgetaryPayerStatus),
                BudgetaryPaymentBase = GetBudgetaryPaymentBase(snapshot.BudgetaryPaymentBase),
                BudgetaryPeriod = GetBudgetaryPeriod(snapshot.BudgetaryPeriod),
                BudgetaryDocNumber = snapshot.BudgetaryDocNumber,
                BudgetaryDocDate = GetBudgetaryDocDate(snapshot),
                Oktmo = snapshot.BudgetaryOkato,
                CodeUin = snapshot.CodeUin,
                Kbk = snapshot.Kbk
            };
        }

        public static string GetPaymentPriority(PaymentPriority paymentPriority)
        {
            var turn = (int)paymentPriority;
            return turn > 0
                ? turn.ToString(CultureInfo.InvariantCulture)
                : string.Empty;
        }

        public static string GetPayerName(PaymentOrder paymentOrder, PaymentOrderSnapshot snapshot)
        {
            if (snapshot.Payer.KontragentType == KontragentTypes.Worker)
            {
                return snapshot.Payer.Name;
            }

            var firmDetails = paymentOrder.Direction == MoneyDirection.Outgoing
                ? snapshot.Payer.Name
                : snapshot.Recipient.Name;

            return
                snapshot.Payer.Name != firmDetails ||
                snapshot.Payer.IsOoo ||
                snapshot.Payer.Name.Contains("(ИП)") ||
                snapshot.Payer.Name.Contains("Индивидуальный предприниматель")
                    ? snapshot.Payer.Name
                    : $"Индивидуальный предприниматель {snapshot.Payer.Name}";
        }

        private static string GetBudgetaryPaymentType(DateTime date, BudgetaryPaymentType budgetaryPaymentType)
        {
            var budgetary110FieldDate = new DateTime(2015, 04, 02);

            if (budgetary110FieldDate < date)
            {
                return string.Empty;
            }

            var paymentTypeName = new Dictionary<BudgetaryPaymentType, string>
            {
                {BudgetaryPaymentType.TaxPayment, "НС"},
                {BudgetaryPaymentType.Fee, "ВЗ"},
                {BudgetaryPaymentType.Pay, "ПЛ"},
                {BudgetaryPaymentType.Advance, "АВ"},
                {BudgetaryPaymentType.Duty, "ГП"},
                {BudgetaryPaymentType.Peni, "0"},
                {BudgetaryPaymentType.TaxSanction, "СА"},
                {BudgetaryPaymentType.AdministrationPenalty, "АШ"},
                {BudgetaryPaymentType.AnotherPenalty, "ИШ"},
                {BudgetaryPaymentType.Percent, "ПЦ"},
                {BudgetaryPaymentType.Other, "0"}
            };

            return paymentTypeName.GetValueOrDefault(budgetaryPaymentType, string.Empty);
        }

        private static string GetBudgetaryPaymentStatus(BudgetaryPayerStatus budgetaryPayerStatus)
        {
            return budgetaryPayerStatus != BudgetaryPayerStatus.None
                      ? ((int)budgetaryPayerStatus).ToString("00")
                      : string.Empty;
        }

        private static string GetBudgetaryPaymentBase(BudgetaryPaymentBase budgetaryPaymentBase)
        {
            try
            {
                var fieldName = Enum.GetName(typeof(BudgetaryPaymentBase), budgetaryPaymentBase);
                var fieldValue = typeof(BudgetaryPaymentBase).GetField(fieldName);
                var attribute = fieldValue.GetCustomAttribute<BudgetaryPaymentBaseAttribute>();
                return attribute != null
                    ? attribute.DetectStrings.FirstOrDefault()
                    : string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

        public static string GetBudgetaryPeriod(BudgetaryPeriod budgetaryPeriod)
        {
            if (budgetaryPeriod == null)
            {
                return string.Empty;
            }

            if (budgetaryPeriod.Type == BudgetaryPeriodType.Date && budgetaryPeriod.Date.HasValue)
            {
                return budgetaryPeriod.Date.Value.ToString("dd.MM.yyyy");
            }

            return budgetaryPeriod.Type != BudgetaryPeriodType.None && budgetaryPeriod.Type != BudgetaryPeriodType.NoPeriod
                ? $"{budgetaryPeriod.Type.GetAbbreviation()}.{budgetaryPeriod.Number:00}.{budgetaryPeriod.Year}"
                : "0";
        }

        private static string GetBudgetaryDocDate(PaymentOrderSnapshot snapshot)
        {
            if (snapshot.BudgetaryDocDate.HasValue)
            {
                return snapshot.BudgetaryDocDate.Value.ToString("dd.MM.yyyy");
            }
            return string.IsNullOrEmpty(snapshot.Kbk)
                ? string.Empty
                : "0";
        }

        public static string GetAbbreviation(this BudgetaryPeriodType type)
        {
            return type switch
            {
                BudgetaryPeriodType.Year => "ГД",
                BudgetaryPeriodType.HalfYear => "ПЛ",
                BudgetaryPeriodType.Quarter => "КВ",
                BudgetaryPeriodType.Month => "МС",
                BudgetaryPeriodType.NoPeriod => "0",
                BudgetaryPeriodType.Date => "ДТ",
                _ => string.Empty,
            };
        }
    }
}