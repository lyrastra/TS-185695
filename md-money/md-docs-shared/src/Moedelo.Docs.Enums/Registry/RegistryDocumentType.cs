using System.ComponentModel;

namespace Moedelo.Docs.Enums.Registry
{
    public enum RegistryDocumentType
    {
        [Description("Накладная (покупки)")]
        PurchasesWaybill = 1,

        [Description("Накладная (продажи)")]
        SalesWaybill = 2,

        [Description("Акт (покупки)")]
        PurchasesStatement = 3,

        [Description("Акт (продажи)")]
        SalesStatement = 4,

        [Description("Универсальный передаточный документ (покупки)")]
        PurchasesUpd = 5,

        [Description("Универсальный передаточный документ (продажи)")]
        SalesUpd = 6,

        [Description("Авансовый отчет (покупки)")]
        PurchasesAdvanceStatement = 7,

        [Description("Счет (продажи)")]
        SalesBill = 8,

        [Description("Счет-фактура (покупки)")]
        PurchasesInvoice = 9,

        [Description("Счет-фактура (продажи)")]
        SalesInvoice = 10,

        [Description("Отчет о розничной продаже (продажи)")]
        SalesRetailReport = 11,

        [Description("Отчет посредника (продажи)")]
        SalesMiddlemanReport = 12,

        [Description("Входящий инвойс (покупки)")]
        PurchasesCurrencyInvoice = 13,

        [Description("Исходящий инвойс (продажи)")]
        SalesCurrencyInvoice = 14,

        [Description("Входящий договор (покупки)")]
        PurchasesContract = 15,

        [Description("Исходящий договор (продажи)")]
        SalesContract = 16,
    }
}