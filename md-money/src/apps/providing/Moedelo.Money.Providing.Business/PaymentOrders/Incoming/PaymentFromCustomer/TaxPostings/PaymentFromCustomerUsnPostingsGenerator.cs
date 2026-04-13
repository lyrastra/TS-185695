using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Money.Providing.Business.TaxPostings.Models;
using Moedelo.Requisites.Enums.TaxationSystems;
using Moedelo.TaxPostings.Enums;
using TaxPostingStatus = Moedelo.TaxPostings.Enums.TaxPostingStatus;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.TaxPostings
{
    internal static class PaymentFromCustomerUsnPostingsGenerator
    {
        private const int envdStopYear = 2021;

        public static UsnTaxPostingsResponse Generate(PaymentFromCustomerPostingsBusinessModel model)
        {
            if (model.TaxationSystem != TaxationSystemType.Usn &&
                model.TaxationSystem != TaxationSystemType.UsnAndEnvd &&
                model.TaxationSystem != TaxationSystemType.Patent)
            {
                return new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);
            }

            if (model.Sum <= 0)
            {
                return new UsnTaxPostingsResponse(TaxPostingStatus.No);
            }

            if (model.IsMediation)
            {
                return CreateMediationPosting(model);
            }

            if (model.IncludeNds && model.Sum <= model.NdsSum)
            {
                return new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);
            }

            return CreatePostings(model);
        }

        private static UsnTaxPostingsResponse CreateMediationPosting(PaymentFromCustomerPostingsBusinessModel model)
        {
            if (model.TaxationSystem == TaxationSystemType.Patent)
            {
                return new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);
            }

            var postingSum = (model.MediationCommissionSum ?? 0) - (model.MediationNdsSum ?? 0);
            if (postingSum <= 0)
            {
                return new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);
            }

            return new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = model.TaxationSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = model.Date.Date,
                        Sum = postingSum,
                        Direction = TaxPostingDirection.Incoming,
                        Description = "Удержано вознаграждение посредника",
                        DocumentId = model.DocumentBaseId,
                        DocumentNumber = model.Number,
                        RelatedDocumentBaseIds = new[] { model.DocumentBaseId }
                    }
                }
            };
        }

        private static UsnTaxPostingsResponse CreatePostings(PaymentFromCustomerPostingsBusinessModel model)
        {
            var postings = new List<UsnTaxPosting>();

            var result = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = model.TaxationSystem,
                Postings = postings,
            };

            var ndsCalculator = InitNdsCalculator(model);

            var waybillDict = model.Waybills.ToDictionary(x => x.DocumentBaseId);
            var statementDict = model.Statements.ToDictionary(x => x.DocumentBaseId);
            var updDict = model.Upds.ToDictionary(x => x.DocumentBaseId);
            foreach (var documentLink in model.DocumentLinks)
            {
                UsnTaxPosting posting = null;
                switch (documentLink.Type)
                {
                    case LinkedDocumentType.Waybill:
                        waybillDict.TryGetValue(documentLink.DocumentBaseId, out var waybill);
                        posting = GetWaybillPosting(model, waybill, documentLink.LinkSum, ndsCalculator);
                        break;
                    case LinkedDocumentType.Statement:
                        statementDict.TryGetValue(documentLink.DocumentBaseId, out var statement);
                        posting = GetStatementPosting(model, statement, documentLink.LinkSum, ndsCalculator);
                        break;
                    case LinkedDocumentType.SalesUpd:
                        updDict.TryGetValue(documentLink.DocumentBaseId, out var upd);
                        posting = GetUpdPosting(model, upd, documentLink.LinkSum, ndsCalculator);
                        break;
                    default:
                        continue;
                }
                if (posting == null)
                {
                    continue;
                }
                postings.Add(posting);
            }

            var sumWithoutNds = ndsCalculator.GetSumWithoutNdsForPayment();
            if (sumWithoutNds > 0)
            {
                // BP-11602 этот кусок, вероятно, криво перенесли из буккипинга
                // Нужно для расчета НДС, чтобы при округлении копеечки не падали в проводку по п/п
                if (model.IncludeNds && model.NdsSum > 0)
                {
                    var documentsSum = 0m;
                    documentsSum += model.Waybills.Sum(x => x.Sum);
                    documentsSum += model.Statements.Sum(x => x.Sum);
                    documentsSum += model.Upds.Sum(x => x.Sum);

                    if (model.Sum == documentsSum &&
                        postings.Count > 0)
                    {
                        postings.Last().Sum += sumWithoutNds;
                        result.TaxStatus = TaxPostingStatus.ByLinkedDocument;
                        return result;
                    }
                }

                var paymentPosting = new UsnTaxPosting
                {
                    Date = model.Date.Date,
                    Sum = sumWithoutNds,
                    Direction = TaxPostingDirection.Incoming,
                    Description = $"Оплата{KontragentDescription(model.KontragentName)}.",
                    DocumentId = model.DocumentBaseId,
                    DocumentNumber = model.Number,
                    RelatedDocumentBaseIds = new[] { model.DocumentBaseId }
                };
                postings.Add(paymentPosting);
                return result;
            }

            result.TaxStatus = result.Postings.Count > 0
                ? TaxPostingStatus.ByLinkedDocument
                : TaxPostingStatus.NotTax;

            return result;
        }

        private static UsnNdsCalculator InitNdsCalculator(PaymentFromCustomerPostingsBusinessModel model)
        {
            var ndsSum = IncludeNds(model) ? model.NdsSum.GetValueOrDefault() : 0m;

            var excludeSum = 0m;
            excludeSum += model.Waybills.Where(x => !IsTaxable(x, model.TaxationSystem, model.Date)).Sum(x => x.Sum);
            excludeSum += model.Statements.Where(x => !IsTaxable(x, model.TaxationSystem, model.Date)).Sum(x => x.Sum);
            excludeSum += model.Upds.Where(x => !IsTaxable(x)).Sum(x => x.Sum);

            var ndsCalculator = new UsnNdsCalculator(model.Sum, ndsSum, excludeSum);
            return ndsCalculator;
        }

        private static UsnTaxPosting GetWaybillPosting(
            PaymentFromCustomerPostingsBusinessModel model,
            SalesWaybill waybill,
            decimal paidSum,
            UsnNdsCalculator ndsCalculator)
        {
            if (IsTaxable(waybill, model.TaxationSystem, model.Date) == false)
            {
                return null;
            }

            var sumWithoutnds = ndsCalculator.CalculateSumWithoutNdsForDocument(paidSum);
            return new UsnTaxPosting
            {
                Date = model.Date,
                Sum = sumWithoutnds,
                Direction = TaxPostingDirection.Incoming,
                DocumentId = model.DocumentBaseId,
                DocumentNumber = model.Number,
                Description = GetDescription(waybill, model.KontragentName),
                RelatedDocumentBaseIds = new[] { model.DocumentBaseId, waybill.DocumentBaseId }
            };
        }

        private static UsnTaxPosting GetStatementPosting(
            PaymentFromCustomerPostingsBusinessModel model,
            SalesStatement statement,
            decimal paidSum,
            UsnNdsCalculator ndsCalculator)
        {
            if (IsTaxable(statement, model.TaxationSystem, model.Date) == false)
            {
                return null;
            }

            var sumWithoutnds = ndsCalculator.CalculateSumWithoutNdsForDocument(paidSum);
            return new UsnTaxPosting
            {
                Date = model.Date,
                Sum = sumWithoutnds,
                Direction = TaxPostingDirection.Incoming,
                DocumentId = model.DocumentBaseId,
                DocumentNumber = model.Number,
                Description = GetDescription(statement, model.KontragentName),
                RelatedDocumentBaseIds = new[] { model.DocumentBaseId, statement.DocumentBaseId }
            };
        }

        private static UsnTaxPosting GetUpdPosting(
            PaymentFromCustomerPostingsBusinessModel model,
            SalesUpd upd,
            decimal paidSum,
            UsnNdsCalculator ndsCalculator)
        {
            if (IsTaxable(upd) == false)
            {
                return null;
            }

            var sumWithoutnds = ndsCalculator.CalculateSumWithoutNdsForDocument(paidSum);
            return new UsnTaxPosting
            {
                Date = model.Date,
                Sum = sumWithoutnds,
                Direction = TaxPostingDirection.Incoming,
                DocumentId = model.DocumentBaseId,
                DocumentNumber = model.Number,
                Description = GetDescription(upd, model.KontragentName),
                RelatedDocumentBaseIds = new[] { model.DocumentBaseId, upd.DocumentBaseId }
            };
        }

        private static bool IncludeNds(PaymentFromCustomerPostingsBusinessModel model)
        {
            return model.IncludeNds && (model.NdsSum ?? 0m) > 0;
        }

        private static bool IsTaxable(SalesWaybill waybill, TaxationSystemType paymentTaxStatus, DateTime paymentDate)
        {
            var consideredByPayment = waybill == null ? false : waybill.Date.Year < envdStopYear && paymentDate.Year >= envdStopYear;
            var result = waybill != null && waybill.Sum > 0 &&
               (waybill.TaxationSystemType == null ||
               waybill.TaxationSystemType == Enums.TaxationSystemType.Usn ||
               waybill.TaxationSystemType == Enums.TaxationSystemType.UsnAndEnvd ||
               waybill.TaxationSystemType == Enums.TaxationSystemType.Patent ||
               (waybill.TaxationSystemType == Enums.TaxationSystemType.Envd &&
                (paymentTaxStatus == TaxationSystemType.Usn ||
                paymentTaxStatus == TaxationSystemType.Patent) &&
                consideredByPayment));
            return result;
        }

        private static bool IsTaxable(SalesStatement statement, TaxationSystemType paymentTaxStatus, DateTime paymentDate)
        {
            var consideredByPayment = statement == null ? false : statement.Date.Year < envdStopYear && paymentDate.Year >= envdStopYear;
            var result = statement != null && statement.Sum > 0 &&
                (statement.TaxationSystemType == null ||
                statement.TaxationSystemType == Enums.TaxationSystemType.Usn ||
                statement.TaxationSystemType == Enums.TaxationSystemType.UsnAndEnvd ||
                statement.TaxationSystemType == Enums.TaxationSystemType.Patent ||
                (statement.TaxationSystemType == Enums.TaxationSystemType.Envd &&
                 (paymentTaxStatus == TaxationSystemType.Usn ||
                 paymentTaxStatus == TaxationSystemType.Patent) &&
                 consideredByPayment));
            return result;
        }

        private static bool IsTaxable(SalesUpd upd)
        {
            return upd != null && upd.Sum > 0 &&
                (upd.TaxationSystemType == null ||
                upd.TaxationSystemType == Enums.TaxationSystemType.Usn ||
                upd.TaxationSystemType == Enums.TaxationSystemType.UsnAndEnvd ||
                upd.TaxationSystemType == Enums.TaxationSystemType.Patent);
        }

        private static string GetDescription(SalesWaybill waybill, string kontragentName)
        {
            var documentName = waybill.ForgottenDocumentDate > DateTime.MinValue
                ? GetDocumentName(waybill.ForgottenDocumentNumber, waybill.ForgottenDocumentDate.Value)
                : GetDocumentName(waybill.Number, waybill.Date);

            return $"Оплата по накладной {documentName}{KontragentDescription(kontragentName)}.";
        }

        private static string GetDescription(SalesStatement statement, string kontragentName)
        {
            return $"Оплата по акту {GetDocumentName(statement.Number, statement.Date)}{KontragentDescription(kontragentName)}.";
        }

        private static string GetDescription(SalesUpd upd, string kontragentName)
        {
            var documentName = upd.ForgottenDocumentDate > DateTime.MinValue
                ? GetDocumentName(upd.ForgottenDocumentNumber, upd.ForgottenDocumentDate.Value)
                : GetDocumentName(upd.Number, upd.Date);

            return $"Оплата по УПД {documentName}{KontragentDescription(kontragentName)}.";
        }

        private static string KontragentDescription(string kontragentName)
        {
            if (string.IsNullOrWhiteSpace(kontragentName))
            {
                return string.Empty;
            }
            return $" от контрагента {kontragentName}";
        }

        private static string GetDocumentName(string number, DateTime date)
        {
            return $"№{number} от {date:dd.MM.yyy}";
        }
    }
}
