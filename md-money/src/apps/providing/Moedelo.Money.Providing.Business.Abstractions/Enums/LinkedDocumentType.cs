using System.ComponentModel;

namespace Moedelo.Money.Providing.Business.Abstractions.Enums
{
    public enum LinkedDocumentType
    {
        [Description("Накладная")]
        Waybill = 1,

        [Description("Бухгалтерская справка")]
        AccountingStatement = 4,

        [Description("Платежное поручение")]
        PaymentOrder = 5,

        [Description("Акт")]
        Statement = 6,

        /// <summary>
        /// Авансовый отчет
        /// </summary>
        [Description("Авансовый отчет")]
        AccountingAdvanceStatement = 11,

        [Description("Инвентарная карточка")]
        InventoryCard = 18,

        /// <summary>
        /// Отчет посредника
        /// </summary>
        [Description("Отчет посредника")]
        MiddlemanReport = 25,

        /// <summary>
        /// Входящий универсальный передаточный документ
        /// </summary>
        [Description("УПД")]
        Upd = 33,

        /// <summary>
        /// Исходящий универсальный передаточный документ
        /// </summary>
        [Description("УПД")]
        SalesUpd = 36,

        /// <summary>
        /// Акт приема-передачи
        /// </summary>
        [Description("Акт приема-передачи")]
        ReceiptStatement = 54,

        /// <summary>
        /// Исходящий инвойс
        /// </summary>
        [Description("Исходящий инвойс")]
        SalesCurrencyInvoice = 55
    }
}
