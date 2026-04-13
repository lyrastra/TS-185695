namespace Moedelo.Common.Enums.Enums.Documents.Extensions
{
    public static class AccountingDocumentTypeExtensions
    {
        public static AdvancePaymentDocumentTypes ToAdvancePaymentDocumentType(this AccountingDocumentType type)
        {
            switch (type)
            {
                case AccountingDocumentType.OutcomingCashOrder:
                    return AdvancePaymentDocumentTypes.CashOrder;
                case AccountingDocumentType.PaymentOrder:
                    return AdvancePaymentDocumentTypes.PaymentOrder;
                case AccountingDocumentType.AccountingAdvanceStatement:
                    return AdvancePaymentDocumentTypes.Advance;
                default:
                    return AdvancePaymentDocumentTypes.Default;
            }
        }

        public static string GetDocumentTypeName(this AccountingDocumentType type)
        {
            switch (type)
            {
                case AccountingDocumentType.Waybill:
                    return "Накладная";
                case AccountingDocumentType.AccountingStatement:
                    return "Бухгалтерская справка";
                case AccountingDocumentType.PaymentOrder:
                    return "Платежное поручение";
                case AccountingDocumentType.Statement:
                    return "Акт";
                case AccountingDocumentType.IncomingCashOrder:
                case AccountingDocumentType.OutcomingCashOrder:
                    return "Кассовый ордер";
                case AccountingDocumentType.MiddlemanReport:
                case AccountingDocumentType.CommissionAgentReport:
                    return "Отчет посредника";
                case AccountingDocumentType.AccountingAdvanceStatement:
                    return "Авансовый отчет";
                case AccountingDocumentType.Invoice:
                    return "Счет-фактура";
                case AccountingDocumentType.InventoryCard:
                    return "Инвентарная карточка";
                case AccountingDocumentType.RetailReport:
                    return "Отчет о розничной продаже";
                case AccountingDocumentType.Bill:
                    return "Счет";
                case AccountingDocumentType.Inventory:
                    return "Инвентаризация";
                case AccountingDocumentType.Upd:
                    return "УПД на покупку";
                case AccountingDocumentType.SalesUpd:
                    return "УПД на продажу";
                case AccountingDocumentType.RetailRefund:
                    return "Возврат";
                case AccountingDocumentType.RequisitionWaybill:
                    return "Требование-накладная";
                case AccountingDocumentType.StockBundling:
                    return "Комплектация";
                case AccountingDocumentType.StockUnbundling:
                    return "Разукомплектация";
                case AccountingDocumentType.StockMovement:
                    return "Перемещение";
                case AccountingDocumentType.ProductIncome:
                    return "Приход без документов";
                case AccountingDocumentType.ManufacturingReport:
                    return "Отчет о выпуске готовой продукции";
                case AccountingDocumentType.ReceiptStatement:
                    return "Акт приема-передачи";
                case AccountingDocumentType.Ukd:
                    return "УКД";
                case AccountingDocumentType.PurchasesCurrencyInvoice:
                case AccountingDocumentType.SalesCurrencyInvoice:
                    return "Инвойс";
                default:
                    return string.Empty;
            }
        }
    }
}