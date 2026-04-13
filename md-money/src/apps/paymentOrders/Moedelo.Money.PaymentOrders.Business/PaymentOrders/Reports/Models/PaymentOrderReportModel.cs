using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Attributes;
using Moedelo.Money.PaymentOrders.Domain.Models;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using System;
using System.Globalization;
using System.Linq;

namespace Moedelo.Money.PaymentOrders.Business.PaymentOrders.Reports.Models
{
    internal class PaymentOrderReportModel
    {
        private readonly PaymentOrder paymentOrder;
        private readonly PaymentOrderSnapshot snapshot;

        public PaymentOrderReportModel(
            PaymentOrder paymentOrder,
            PaymentOrderSnapshot snapshot)
        {
            this.paymentOrder = paymentOrder;
            this.snapshot = snapshot;
        }

        public DateTime Date => paymentOrder.Date;
        public string Number => paymentOrder.Number;
        public decimal Sum => paymentOrder.Sum;
        public MoneyDirection Direction => paymentOrder.Direction;
        public string Description => paymentOrder.Description;
        public string PaymentPriority => paymentOrder.PaymentPriority == Enums.PaymentPriority.Default
            ? string.Empty
            : ((int)paymentOrder.PaymentPriority).ToString(CultureInfo.InvariantCulture);

        public string SettlementNumber => Direction == MoneyDirection.Outgoing
            ? PayerSettlementNumber
            : RecipientSettlementNumber;


        public string FirmDetails
        {
            get { return Direction == MoneyDirection.Outgoing ? PayerName : RecipientName; }
        }
        public string Kbk => snapshot.Kbk;

        public string CodeUin => snapshot.CodeUin;

        public string Oktmo => snapshot.BudgetaryOkato;

        public BudgetaryPaymentType BudgetaryPaymentType => snapshot.BudgetaryPaymentType;

        public string BudgetaryPayerStatus => snapshot.BudgetaryPayerStatus == Enums.BudgetaryPayerStatus.None
            ? string.Empty
            : ((int)snapshot.BudgetaryPayerStatus).ToString("00");

        public string BudgetaryPaymentBase
        {
            get
            {
                try
                {
                    var value = typeof(BudgetaryPaymentBase).GetField(Enum.GetName(typeof(BudgetaryPaymentBase), snapshot.BudgetaryPaymentBase));
                    var attr = (BudgetaryPaymentBaseAttribute)Attribute.GetCustomAttribute(value, typeof(BudgetaryPaymentBaseAttribute));
                    return attr != null
                        ? attr.DetectStrings.FirstOrDefault()
                        : string.Empty;
                }
                catch (ArgumentNullException)
                {
                    return string.Empty;
                }
            }
        }

        public string BudgetaryDocNumber => snapshot.BudgetaryDocNumber;
        public DateTime? BudgetaryDocDate => snapshot.BudgetaryDocDate;
        public BudgetaryPeriod BudgetaryPeriod => snapshot.BudgetaryPeriod;

        public string KindOfPay => snapshot.KindOfPay;


        public bool PayerIsOoo => snapshot.Payer.IsOoo;
        public string PayerName => snapshot.Payer.Name;
        public string PayerAddress => snapshot.Payer.Address;
        public string PayerSettlementNumber => snapshot.Payer.SettlementNumber;
        public string PayerInn => snapshot.Payer.Inn;
        public string PayerKpp => snapshot.Payer.Kpp;
        public string PayerBankName => snapshot.Payer.BankName;
        public string PayerBankCity => snapshot.Payer.BankCity;
        public string PayerBankBik => snapshot.Payer.BankBik;
        public string PayerCorrespondentAccount => snapshot.Payer.BankCorrespondentAccount;


        public string RecipientName => snapshot.Recipient.Name;
        public string RecipientSettlementNumber => snapshot.Recipient.SettlementNumber;
        public string RecipientInn => snapshot.Recipient.Inn;
        public string RecipientKpp => snapshot.Recipient.Kpp;
        public string RecipientBankName => snapshot.Recipient.BankName;
        public string RecipientBankCity => snapshot.Recipient.BankCity;
        public string RecipientBankBik => snapshot.Recipient.BankBik;
        public string RecipientCorrespondentAccount => snapshot.Recipient.BankCorrespondentAccount;
    }
}
