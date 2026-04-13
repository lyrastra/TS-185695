using Moedelo.Money.Providing.Business.Estate.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using System;

namespace Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.TaxPostings
{
    static class PaymentToSupplierUsnPostingsDescriptionFormer
    {
        public static string GetDescription(PaymentToSupplierUsnPostingsBusinessModel model, PurchasesWaybill waybill)
        {
            var description = $"Оплата за материалы по накладной {GetDocumentNumberAndDate(waybill.ForgottenDocumentNumber ?? waybill.Number, waybill.ForgottenDocumentDate ?? waybill.Date)}{KontragentDescription(model.Kontragent.Name)}.";
            if (model.Date < (waybill.ForgottenDocumentDate ?? waybill.Date))
            {
                description += $" Оплата по платежному поручению {GetDocumentNumberAndDate(model.Number, model.Date)}.";
            }
            return description;
        }

        public static string GetDescription(PaymentToSupplierUsnPostingsBusinessModel model, PurchasesStatement statement)
        {
            var description = $"Оплата за услуги по акту {GetDocumentNumberAndDate(statement.Number, statement.Date)}{KontragentDescription(model.Kontragent.Name)}.";
            if (model.Date < statement.Date)
            {
                description += $" Оплата по платежному поручению {GetDocumentNumberAndDate(model.Number, model.Date)}.";
            }
            return description;
        }

        public static string GetMaterialDescription(PaymentToSupplierUsnPostingsBusinessModel model, PurchasesUpd upd)
        {
            var description = $"Оплата за материалы по УПД {GetDocumentNumberAndDate(upd.ForgottenDocumentNumber ?? upd.Number, upd.ForgottenDocumentDate ?? upd.Date)}{KontragentDescription(model.Kontragent.Name)}.";
            if (model.Date < (upd.ForgottenDocumentDate ?? upd.Date))
            {
                description += $" Оплата по платежному поручению {GetDocumentNumberAndDate(model.Number, model.Date)}.";
            }
            return description;
        }

        public static string GetServiceDescription(PaymentToSupplierUsnPostingsBusinessModel model, PurchasesUpd upd)
        {
            var description = $"Оплата за услуги по УПД {GetDocumentNumberAndDate(upd.ForgottenDocumentNumber ?? upd.Number, upd.ForgottenDocumentDate ?? upd.Date)}{KontragentDescription(model.Kontragent.Name)}.";
            if (model.Date < (upd.ForgottenDocumentDate ?? upd.Date))
            {
                description += $" Оплата по платежному поручению {GetDocumentNumberAndDate(model.Number, model.Date)}.";
            }
            return description;
        }

        public static string GetDescription(PaymentToSupplierUsnPostingsBusinessModel model, InventoryCard inventoryCard)
        {
            var description = $"Оплата по инвентарной карточке {GetDocumentNumberAndDate(inventoryCard.Number, inventoryCard.Date)}{KontragentDescription(model.Kontragent.Name)}.";
            if (model.Date < inventoryCard.Date)
            {
                description += $" Оплата по платежному поручению {GetDocumentNumberAndDate(model.Number, model.Date)}.";
            }
            return description;
        }

        private static string GetDocumentNumberAndDate(string number, DateTime date)
        {
            return $"№{number} от {date:dd.MM.yyyy}";
        }

        public static string KontragentDescription(string kontragentName)
        {
            if (string.IsNullOrWhiteSpace(kontragentName))
            {
                return string.Empty;
            }
            return $" от контрагента {kontragentName}";
        }
    }
}

