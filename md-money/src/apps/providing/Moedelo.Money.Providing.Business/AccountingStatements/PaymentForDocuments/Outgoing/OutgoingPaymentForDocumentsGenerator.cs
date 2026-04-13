using Moedelo.AccPostings.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.AccountingPostings.Extensions;
using Moedelo.Money.Providing.Business.AccountingStatements.Models;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Outgoing
{
    static class OutgoingPaymentForDocumentsGenerator
    {
        const ushort envdStopYear = 2021;
        internal static PaymentForDocumentsGenerateResult[] Generate(OutgoingPaymentForDocumentsGenerateRequest data)
        {
            if (!data.IsMainKontragent)
            {
                return Array.Empty<PaymentForDocumentsGenerateResult>();
            }

            var results = new List<PaymentForDocumentsGenerateResult>(data.Links.Count);
            var baseDocumentsById = data.Links.ToDictionary(x => x.DocumentBaseId);

            results.AddRange(data.Waybills.Select(waybill =>
            {
                baseDocumentsById.TryGetValue(waybill.DocumentBaseId, out var link);
                return GetByWaybill(waybill, link, data);
            }));

            results.AddRange(data.Statements.Select(statement =>
            {
                baseDocumentsById.TryGetValue(statement.DocumentBaseId, out var link);
                return GetByStatement(statement, link, data);
            }));

            results.AddRange(data.Upds.Select(statement =>
            {
                baseDocumentsById.TryGetValue(statement.DocumentBaseId, out var link);
                return GetByUpd(statement, link, data);
            }));

            return results.Where(x => x != null).ToArray();
        }

        private static PaymentForDocumentsGenerateResult GetByWaybill(PurchasesWaybill waybill, DocumentLink link, OutgoingPaymentForDocumentsGenerateRequest data)
        {
            if (link == null)
            {
                throw new ArgumentException($"Not found waybill by DocumentBaseId = {link.DocumentBaseId}");
            }

            if (!SyntheticAccountCode._60.ContainsSubaccount(waybill.KontragentAccountCode))
            {
                return null;
            }

            var date = DateTimeHelper.Max(waybill.Date, data.PaymentDate);

            var docDescription = $"Зачет предоплаты по накладной №{waybill.Number} от {waybill.Date:dd.MM.yyyy} от контрагента {data.Kontragent.Name}";

            var postingDescription = $"Признание предоплаты оплатой товаров по накладной № {waybill.Number} от {waybill.Date:dd.MM.yyyy}";

            return new PaymentForDocumentsGenerateResult
            {
                AccountingStatement = CreateAccStatement(date, link.LinkSum, docDescription),
                AccoutingPosting = CreateAccPosting(link.LinkSum, postingDescription, data.Kontragent, data.Contract),
                PrimaryDocBaseId = waybill.DocumentBaseId,
                PrimaryDocDate = waybill.Date
            };
        }

        private static PaymentForDocumentsGenerateResult GetByStatement(PurchasesStatement statement, DocumentLink link, OutgoingPaymentForDocumentsGenerateRequest data)
        {
            if (link == null)
            {
                throw new ArgumentException($"Not found statement by DocumentBaseId = {link.DocumentBaseId}");
            }

            if (!SyntheticAccountCode._60.ContainsSubaccount(statement.KontragentAccountCode))
            {
                return null;
            }

            var date = DateTimeHelper.Max(statement.Date, data.PaymentDate);

            var docDescription = $"Зачет предоплаты по акту №{statement.Number} от {statement.Date:dd.MM.yyyy} от контрагента {data.Kontragent.Name}";

            var postingDescription = $"Признание предоплаты оплатой работ по акту № {statement.Number} от {statement.Date:dd.MM.yyyy}";

            // этот функционал выглядит лишним, но есть требование от пользователя (TS-52341 && TS-105608) 
            var taxSystem = statement.TaxationSystemType == TaxationSystemType.Envd && !(data.PaymentDate.Year >= envdStopYear)
                ? TaxationSystemType.Envd
                : default(TaxationSystemType?);

            return new PaymentForDocumentsGenerateResult
            {
                AccountingStatement = CreateAccStatement(date, link.LinkSum, docDescription, taxSystem),
                AccoutingPosting = CreateAccPosting(link.LinkSum, postingDescription, data.Kontragent, data.Contract),
                PrimaryDocBaseId = statement.DocumentBaseId,
                PrimaryDocDate = statement.Date
            };
        }

        private static PaymentForDocumentsGenerateResult GetByUpd(PurchasesUpd upd, DocumentLink link, OutgoingPaymentForDocumentsGenerateRequest data)
        {
            if (link == null)
            {
                throw new ArgumentException($"Not found upd by DocumentBaseId = {link.DocumentBaseId}");
            }

            if (!SyntheticAccountCode._60.ContainsSubaccount(upd.KontragentAccountCode))
            {
                return null;
            }

            var date = DateTimeHelper.Max(upd.Date, data.PaymentDate);

            var docDescription = $"Зачет предоплаты по УПД №{upd.Number} от {upd.Date:dd.MM.yyyy} от контрагента {data.Kontragent.Name}";

            var postingDescription = $"Признание предоплаты оплатой по УПД № {upd.Number} от {upd.Date:dd.MM.yyyy}";

            return new PaymentForDocumentsGenerateResult
            {
                AccountingStatement = CreateAccStatement(date, link.LinkSum, docDescription),
                AccoutingPosting = CreateAccPosting(link.LinkSum, postingDescription, data.Kontragent, data.Contract),
                PrimaryDocBaseId = upd.DocumentBaseId,
                PrimaryDocDate = upd.Date
            };
        }

        private static AccountingStatement CreateAccStatement(DateTime date, decimal sum, string description, TaxationSystemType? taxSystem = null)
        {
            return new AccountingStatement
            {
                Date = date,
                Sum = sum,
                Description = description,
                TaxationSystemType = taxSystem
            };
        }

        private static AccountingPosting CreateAccPosting(decimal sum, string description, Kontragent kontragent, Contract contract)
        {
            return new AccountingPosting
            {
                Sum = sum,
                DebitCode = SyntheticAccountCode._60_01,
                CreditCode = SyntheticAccountCode._60_02,
                DebitSubcontos = new[] { kontragent.ToSubconto(), contract.ToSubconto() },
                CreditSubcontos = new[] { kontragent.ToSubconto(), contract.ToSubconto() },
                Description = description
            };
        }
    }
}
