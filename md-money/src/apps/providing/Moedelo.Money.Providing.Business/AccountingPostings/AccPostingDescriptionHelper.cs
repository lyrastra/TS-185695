using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Providing.Business.AccountingPostings
{
    static class AccPostingDescriptionHelper
    {
        public static string GetPaymentPostingDescription(IReadOnlyCollection<BaseDocument> linkedDocs, Contract contract)
        {
            if (linkedDocs?.Any() != true)
            {
                return "Оплата";
            }

            if (linkedDocs.Count == 1)
            {
                var document = linkedDocs.First();
                return GetSingleDocDescription(document, contract);
            }

            return GetMultipleDocsDescription(linkedDocs, contract);
        }

        private static string GetMultipleDocsDescription(IReadOnlyCollection<BaseDocument> linkedDocs, Contract contract)
        {
            var documents = linkedDocs.GroupBy(ld => ld.Type)
                .Select(gr =>
                {
                    switch (gr.Key)
                    {
                        case LinkedDocumentType.Statement:
                            return $"работ по актам: {string.Join(", ", gr.Select(GetDocumentName))}";

                        case LinkedDocumentType.Waybill:
                            return $"товаров по накладным: {string.Join(", ", gr.Select(GetDocumentName))}";

                        case LinkedDocumentType.Upd:
                        case LinkedDocumentType.SalesUpd:
                            return $"по УПД: {string.Join(", ", gr.Select(GetDocumentName))}";

                        case LinkedDocumentType.ReceiptStatement:
                            return $"аванса по договору аренды (лизинга) №{contract.Number} от {contract.Date:dd.MM.yyyy}";

                        default:
                            throw new ArgumentOutOfRangeException($"Not supported document type: {gr.Key}");
                    }
                });
            return TrimDescription($"Оплата {string.Join("; ", documents)}");
        }

        private static string TrimDescription(string result)
        {
            // BP-6955 длина описания в проводке не должна превышать 1000 символов
            if (result.Length > 1000)
            {
                result = result.Substring(0, 996) + "...";
            }
            return result;
        }

        private static string GetSingleDocDescription(BaseDocument document, Contract contract)
        {
            switch (document.Type)
            {
                case LinkedDocumentType.Statement:
                    return $"Оплата работ по акту {GetDocumentName(document)}";

                case LinkedDocumentType.Waybill:
                    return $"Оплата товаров по накладной {GetDocumentName(document)}";

                case LinkedDocumentType.Upd:
                case LinkedDocumentType.SalesUpd:
                    return $"Оплата по УПД {GetDocumentName(document)}";

                case LinkedDocumentType.ReceiptStatement:
                    return GetReceiptStatementPostingDescription(contract);

                default:
                    throw new ArgumentOutOfRangeException($"Not supported document type: {document.Type}");
            }
        }

        public static string GetInventoryCardPostingDescription(BaseDocument document)
        {
            return $"Оплата по инвентарной карточке {GetDocumentName(document)}";
        }

        public static string GetReceiptStatementPostingDescription(Contract contract)
        {
            return $"Оплата аванса по договору аренды (лизинга) №{contract.Number} от {contract.Date:dd.MM.yyyy}";
        }

        private static string GetDocumentName(BaseDocument document)
        {
            return $"№{document.Number} от {document.Date:dd.MM.yyyy}";
        }

        public static string GetRentPaymentPostingDescription()
        {
            return $"Арендный (лизинговый) платеж";
        }
    }
}
