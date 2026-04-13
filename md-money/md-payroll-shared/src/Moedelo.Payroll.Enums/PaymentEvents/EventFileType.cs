using System.ComponentModel;

namespace Moedelo.Payroll.Enums.PaymentEvents;

public enum EventFileType
{
    [Description("Платежная ведомость")]
    Paybill,

    [Description("Расходный ордер")]
    CashOrder,

    [Description("Платежное поручение")]
    PaymentOrder,

    [Description("Платежное поручение выплаты удержаний")]
    DeductionPayment,

    [Description("Реестр")]
    SalaryProjectRegistry,

    [Description("Платежное поручение")]
    SalaryProjectPaymentOrder
}