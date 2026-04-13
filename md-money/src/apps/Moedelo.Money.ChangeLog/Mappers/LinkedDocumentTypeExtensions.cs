using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.Money.ChangeLog.Mappers
{
    public static class LinkedDocumentTypeExtensions
    {
        public static string MapToName(this LinkedDocumentType type)
        {
            // todo: может быть уже где-то есть подобное? если нет - проверить названия (поговорить с учёткой Козлов Р.)
            return type switch {
                LinkedDocumentType.Waybill => "Накладная",
                LinkedDocumentType.Invoice => "Счет-фактура",
                LinkedDocumentType.AccountingStatement => "Бухгалтерская справка",
                LinkedDocumentType.PaymentOrder => "Платёжное поручение",
                LinkedDocumentType.Statement => "Акт",
                LinkedDocumentType.IncomingCashOrder => "Входящий кассовый ордер",
                LinkedDocumentType.OutcomingCashOrder => "Исходящий кассовый ордер",
                LinkedDocumentType.AccountingAdvanceStatement => "Авансовый отчёт",
                LinkedDocumentType.Bill => "Счёт",
                LinkedDocumentType.Project => "Проект",
                LinkedDocumentType.InventoryCard => "Инвентарная карточка",
                LinkedDocumentType.MiddlemanReport => "Отчёт посредника",
                // todo: LinkedDocumentType.PurseOperation => "Операция ???",
                LinkedDocumentType.Upd => "Входящий УПД",
                // todo: LinkedDocumentType.RetailRefund => expr,
                LinkedDocumentType.SalesUpd => "Исходящий УПД",
                LinkedDocumentType.MainContract => "Основной договор",
                LinkedDocumentType.ReceiptStatement => "Акт приема-передачи",
                LinkedDocumentType.SalesCurrencyInvoice => "Исходящий инвойс",
                LinkedDocumentType.PurchaseCurrencyInvoice => "Входящий инвойс",
                _ => type.ToString()
            };
        }
    }
}
