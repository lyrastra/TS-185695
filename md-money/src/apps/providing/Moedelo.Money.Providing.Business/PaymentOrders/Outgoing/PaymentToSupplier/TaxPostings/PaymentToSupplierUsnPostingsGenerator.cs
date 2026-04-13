using Moedelo.Docs.Enums;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.Money.Providing.Business.Estate.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Money.Providing.Business.TaxPostings.Models;
using Moedelo.Stock.Enums;
using Moedelo.TaxPostings.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier.TaxPostings;
using TaxationSystemType = Moedelo.Money.Enums.TaxationSystemType;
using TaxPostingStatus = Moedelo.TaxPostings.Enums.TaxPostingStatus;

namespace Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.TaxPostings
{
    static class PaymentToSupplierUsnPostingsGenerator
    {
        internal const decimal TaxPostingLimit = 100000m;

        public static UsnTaxPostingsResponse Generate(PaymentToSupplierUsnPostingsBusinessModel model)
        {
            if (model.IsUsnProfitAndOutgo == false ||
                model.Sum <= 0)
            {
                return new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);
            }

            if (model.DocumentLinks.Count <= 0)
            {
                return new UsnTaxPostingsResponse(TaxPostingStatus.NotTax)
                {
                    Message = "Не учитывается. Добавьте документ."
                };
            }

            if (model.Waybills.All(x => IsTaxable(x) == false) &&
                model.Statements.All(x => IsTaxable(x) == false) &&
                model.Upds.All(x => IsTaxable(x) == false) &&
                model.InventoryCards.All(x => IsTaxable(x) == false) &&
                model.ReceiptStatements.All(x => IsTaxable(x) == false))
            {
                return new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);
            }

            return CreatePostings(model);
        }

        private static UsnTaxPostingsResponse CreatePostings(PaymentToSupplierUsnPostingsBusinessModel model)
        {
            var postings = new List<UsnTaxPosting>();
            var linkedDocumentsPostings = new List<LinkedDocumentTaxPostings<UsnTaxPosting>>();

            var result = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = model.TaxationSystem,
                LinkedDocuments = linkedDocumentsPostings,
                Postings = postings
            };

            var paymentSum = model.Sum;

            var waybillDict = model.Waybills.ToDictionary(x => x.DocumentBaseId);
            var statementDict = model.Statements.ToDictionary(x => x.DocumentBaseId);
            var updDict = model.Upds.ToDictionary(x => x.DocumentBaseId);
            var inventoryCardDict = model.InventoryCards.ToDictionary(x => x.DocumentBaseId);
            var stockProductTypeDict = model.StockProducts.ToDictionary(x => x.Id, x => x.Type);
            foreach (var documentLink in model.DocumentLinks)
            {
                if (paymentSum <= 0)
                {
                    break;
                }

                if (documentLink.LinkSum <= 0)
                {
                    continue;
                }

                var postingSum = Math.Min(paymentSum, documentLink.LinkSum);
                paymentSum -= postingSum;

                (UsnTaxPosting[] Postings, LinkedDocumentTaxPostings<UsnTaxPosting> LinkedDocument) documentPostings =
                    GetDocumentPostings(model, documentLink, postingSum, waybillDict, statementDict, updDict, inventoryCardDict, stockProductTypeDict);
                if (documentPostings.Postings != null)
                {
                    postings.AddRange(documentPostings.Postings);
                }
                if (documentPostings.LinkedDocument != null)
                {
                    AddOrUpdateLinkedDocument(linkedDocumentsPostings, documentPostings.LinkedDocument);
                }
            }

            if (model.Waybills.Any(x => HasProducts(x, stockProductTypeDict)) ||
                model.Upds.Any(x => HasProducts(x, stockProductTypeDict)))
            {
                result.Message += "Расход в виде стоимости товара будет учитываться при расчете налога, если факт его покупки и продажи документально подтвержден";
            }

            if (model.InventoryCards.Any(HasFixedAssetInvestmentTaxOverLimit) ||
                model.Waybills.Any(x => HasFixedAssetInvestmentTaxOverLimit(x, model.InventoryCardsFromFixedAssetInvestment)) ||
                model.Statements.Any(x => HasFixedAssetInvestmentTaxOverLimit(x, model.InventoryCardsFromFixedAssetInvestment)))
            {
                result.Message += "Расход по основному средству будет учитываться при расчете налога";
            }

            if (model.ReceiptStatements.Any())
            {
                result.Message += "Авансовый платеж будет учтен в расходах при закрытии месяца в соответствии с графиком платежей по арендованному основному средству";
            }

            if (result.Postings.Count == 0 &&
                result.LinkedDocuments.Count == 0 &&
                (model.Waybills.Any(IsTaxableByHand) ||
                model.Statements.Any(IsTaxableByHand) ||
                model.Upds.Any(IsTaxableByHand)))
            {
                result.TaxStatus = TaxPostingStatus.ByLinkedDocument;
                result.Message += "Учет по связанным документам ведется вручную";
                return result;
            }

            // странно, что не учитывается статус "вручную"
            result.TaxStatus = result.Postings.Count > 0
                ? TaxPostingStatus.Yes
                : result.LinkedDocuments.Count > 0
                    ? TaxPostingStatus.ByLinkedDocument
                    : TaxPostingStatus.NotTax;

            return result;
        }

        private static bool IsTaxable(PurchasesWaybill waybill)
        {
            if (waybill is not { Sum: > 0 } || waybill.IsCompensated)
            {
                return false;
            }

            return IsNullOrUsn(waybill.TaxationSystemType);
        }

        private static bool IsTaxable(PurchasesStatement statement)
        {
            if (statement is not { Sum: > 0 } || statement.IsCompensated)
            {
                return false;
            }

            return IsNullOrUsn(statement.TaxationSystemType);
        }

        private static bool IsTaxable(PurchasesUpd upd)
        {
            if (upd is not { Sum: > 0 })
            {
                return false;
            }

            return IsNullOrUsn(upd.TaxationSystemType);
        }

        private static bool IsNullOrUsn(TaxationSystemType? docTaxSystem)
        {
            return docTaxSystem is null or TaxationSystemType.Usn or TaxationSystemType.UsnAndEnvd;
        }

        private static bool IsTaxable(InventoryCard inventoryCard)
        {
            return inventoryCard is { Cost: > 0 } 
                   && IsNullOrUsn(inventoryCard.TaxationSystemType);
        }

        private static bool IsTaxable(Estate.Models.ReceiptStatement receiptStatement)
        {
            return receiptStatement != null && receiptStatement.Sum > 0;
        }

        private static bool HasProducts(PurchasesWaybill waybill, IDictionary<long, StockProductTypeEnum> stockProductsTypes)
        {
            return IsTaxable(waybill) &&
                waybill.Items.Any(x => x.StockProductId.HasValue && stockProductsTypes.TryGetValue(x.StockProductId.Value, out var stockProductsType) &&
                stockProductsType == StockProductTypeEnum.Product);
        }

        private static bool HasProducts(PurchasesUpd upd, IDictionary<long, StockProductTypeEnum> stockProductsTypes)
        {
            return IsTaxable(upd) &&
                upd.Items.Any(x => x.Type == ItemType.Goods && x.StockProductId.HasValue && stockProductsTypes.TryGetValue(x.StockProductId.Value, out var stockProductsType) &&
                stockProductsType == StockProductTypeEnum.Product);
        }

        private static bool HasFixedAssetInvestmentTaxOverLimit(PurchasesWaybill waybill, IDictionary<long, InventoryCard> inventoryCardsFromFixedAssetInvestment)
        {
            return IsTaxable(waybill) &&
                waybill.IsFromFixedAssetInvestment &&
                inventoryCardsFromFixedAssetInvestment.ContainsKey(waybill.DocumentBaseId) &&
                inventoryCardsFromFixedAssetInvestment[waybill.DocumentBaseId].Cost > TaxPostingLimit;
        }

        private static bool HasFixedAssetInvestmentTaxOverLimit(PurchasesStatement statement, IDictionary<long, InventoryCard> inventoryCardsFromFixedAssetInvestment)
        {
            return IsTaxable(statement) &&
                statement.IsFromFixedAssetInvestment &&
                inventoryCardsFromFixedAssetInvestment.ContainsKey(statement.DocumentBaseId) &&
                inventoryCardsFromFixedAssetInvestment[statement.DocumentBaseId].Cost > TaxPostingLimit;
        }

        private static bool HasFixedAssetInvestmentTaxOverLimit(InventoryCard inventoryCard)
        {
            return inventoryCard.Cost > TaxPostingLimit;
        }

        private static bool IsTaxableByHand(PurchasesWaybill waybill)
        {
            return waybill.TaxPostingType == Enums.ProvidePostingType.ByHand;
        }

        private static bool IsTaxableByHand(PurchasesStatement statement)
        {
            return statement.TaxPostingType == Enums.ProvidePostingType.ByHand;
        }

        private static bool IsTaxableByHand(PurchasesUpd upd)
        {
            return upd.TaxPostingType == Enums.ProvidePostingType.ByHand;
        }

        private static (UsnTaxPosting[], LinkedDocumentTaxPostings<UsnTaxPosting>) GetDocumentPostings(PaymentToSupplierUsnPostingsBusinessModel model, DocumentLink documentLink, decimal postingSum,
            IDictionary<long, PurchasesWaybill> waybills,
            IDictionary<long, PurchasesStatement> statements,
            IDictionary<long, PurchasesUpd> upds,
            IDictionary<long, InventoryCard> inventoryCards,
            IDictionary<long, StockProductTypeEnum> stockProductsTypes)
        {
            var postings = new List<UsnTaxPosting>(2);
            LinkedDocumentTaxPostings<UsnTaxPosting> linkedDocument = null;
            switch (documentLink.Type)
            {
                case LinkedDocumentType.Waybill:
                    // fixme: костыль для фикса проблемы, когда документ отсекается мастером ввода остатков
                    if (waybills.TryGetValue(documentLink.DocumentBaseId, out var waybill) == false)
                    {
                        break;
                    }
                    (UsnTaxPosting Posting, LinkedDocumentTaxPostings<UsnTaxPosting> LinkedDocument) waybillPosting = waybill.IsFromFixedAssetInvestment
                        ? GetWaybillFromFixedAssetInvestmentPosting(model, waybill, postingSum)
                        : GetWaybillPosting(model, waybill, postingSum, stockProductsTypes);
                    if (waybillPosting.Posting != null)
                    {
                        postings.Add(waybillPosting.Posting);
                    }
                    linkedDocument = waybillPosting.LinkedDocument;
                    break;
                case LinkedDocumentType.Statement:
                    // fixme: костыль для фикса проблемы, когда документ отсекается мастером ввода остатков
                    if (statements.TryGetValue(documentLink.DocumentBaseId, out var statement) == false)
                    {
                        break;
                    }
                    (UsnTaxPosting Posting, LinkedDocumentTaxPostings<UsnTaxPosting> LinkedDocument) statementPosting = statement.IsFromFixedAssetInvestment
                        ? GetStatementFromFixedAssetInvestmentPosting(model, statement, postingSum)
                        : GetStatementPosting(model, statement, postingSum);
                    if (statementPosting.Posting != null)
                    {
                        postings.Add(statementPosting.Posting);
                    }
                    linkedDocument = statementPosting.LinkedDocument;
                    break;
                case LinkedDocumentType.Upd:
                    upds.TryGetValue(documentLink.DocumentBaseId, out var upd);
                    (UsnTaxPosting[] Postings, LinkedDocumentTaxPostings<UsnTaxPosting> LinkedDocument) updPostings = GetUpdPostings(model, upd, postingSum, stockProductsTypes);
                    if (updPostings.Postings != null)
                    {
                        postings.AddRange(updPostings.Postings);
                    }
                    linkedDocument = updPostings.LinkedDocument;
                    break;
                // обработка костыля из мастера ввода остатков:
                // по идее п/п не должно напрямую работать с ИК,
                // только с первичкой, которая может быть связана с ИК
                case LinkedDocumentType.InventoryCard:
                    inventoryCards.TryGetValue(documentLink.DocumentBaseId, out var inventoryCard);
                    (UsnTaxPosting Posting, LinkedDocumentTaxPostings<UsnTaxPosting> LinkedDocument) inventoryCardPosting = GetInventoryCardPosting(model, inventoryCard, postingSum);
                    if (inventoryCardPosting.Posting != null)
                    {
                        postings.Add(inventoryCardPosting.Posting);
                    }
                    linkedDocument = inventoryCardPosting.LinkedDocument;
                    break;
            }
            return (postings.ToArray(), linkedDocument);
        }

        private static (UsnTaxPosting, LinkedDocumentTaxPostings<UsnTaxPosting>) GetWaybillPosting(PaymentToSupplierUsnPostingsBusinessModel model, PurchasesWaybill waybill, decimal postingSum, IDictionary<long, StockProductTypeEnum> stockProductsTypes)
        {
            if (IsTaxable(waybill) == false)
            {
                return (null, null);
            }

            if (waybill.TaxPostingType == Enums.ProvidePostingType.ByHand)
            {
                return (null, null);
            }

            var materialRate = PurchasesDocTaxSumHelper
                .GetTaxCoefficients(waybill, model.NdsRatePeriods, stockProductsTypes)
                .Materials;
            if (materialRate == 0)
            {
                return (null, null);
            }

            var materialPostingSum = Math.Round(postingSum * materialRate, 2, MidpointRounding.AwayFromZero);
            var date = DateTimeHelper.Max(waybill.ForgottenDocumentDate ?? waybill.Date, model.Date);
            var posting = new UsnTaxPosting
            {
                Date = date,
                Sum = materialPostingSum,
                Direction = TaxPostingDirection.Outgoing,
                DocumentId = model.Date >= (waybill.ForgottenDocumentDate ?? waybill.Date)
                    ? model.DocumentBaseId
                    : waybill.DocumentBaseId,
                DocumentDate = date,
                DocumentNumber = model.Number,
                Description = PaymentToSupplierUsnPostingsDescriptionFormer.GetDescription(model, waybill),
                RelatedDocumentBaseIds = new[] { model.DocumentBaseId, waybill.DocumentBaseId }
            };

            if (posting.DocumentId == model.DocumentBaseId)
            {
                return (posting, null);
            }

            var linkedDocument = new LinkedDocumentTaxPostings<UsnTaxPosting>
            {
                DocumentBaseId = waybill.DocumentBaseId,
                Date = waybill.Date,
                Name = waybill.Number,
                Number = waybill.Number,
                Type = (Abstractions.Enums.LinkedDocumentType)LinkedDocumentType.Waybill,
                Postings = new[] { posting }
            };
            return (null, linkedDocument);
        }

        // тут создается некий франкенштейн из первичного документа и ИК (или п/п)
        private static (UsnTaxPosting, LinkedDocumentTaxPostings<UsnTaxPosting>) GetWaybillFromFixedAssetInvestmentPosting(PaymentToSupplierUsnPostingsBusinessModel model, PurchasesWaybill waybill, decimal postingSum)
        {
            if (IsTaxable(waybill) == false)
            {
                return (null, null);
            }

            if (model.InventoryCardsFromFixedAssetInvestment.TryGetValue(waybill.DocumentBaseId, out var inventoryCard) == false)
            {
                return (null, null);
            }

            if (inventoryCard.Cost > TaxPostingLimit)
            {
                return (null, null);
            }

            var inventoryCardDate = inventoryCard.CommissioningDate ?? inventoryCard.Date;
            var date = DateTimeHelper.Max(inventoryCardDate, model.Date);

            // Только случай ИК <= 100K (гарантируют проверки выше): вложения принудительно рассматриваются как материалы.
            var fakeMaterialMap = new Dictionary<long, StockProductTypeEnum> { { -1, StockProductTypeEnum.Material } };
            foreach (var waybillItem in waybill.Items)
            {
                waybillItem.StockProductId = -1;
            }
            var materialsRate = PurchasesDocTaxSumHelper
                .GetTaxCoefficients(waybill, model.NdsRatePeriods, fakeMaterialMap)
                .Materials;
            
            var posting = new UsnTaxPosting
            {
                Date = date,
                Sum = Math.Round(postingSum * materialsRate, 2, MidpointRounding.AwayFromZero),
                Direction = TaxPostingDirection.Outgoing,
                DocumentId = model.Date >= inventoryCardDate
                    ? model.DocumentBaseId
                    : inventoryCard.DocumentBaseId,
                DocumentDate = date,
                DocumentNumber = model.Number,
                Description = PaymentToSupplierUsnPostingsDescriptionFormer.GetDescription(model, waybill),
                RelatedDocumentBaseIds = new[] { model.DocumentBaseId, inventoryCard.DocumentBaseId }
            };

            if (posting.DocumentId == model.DocumentBaseId)
            {
                return (posting, null);
            }

            var linkedDocument = new LinkedDocumentTaxPostings<UsnTaxPosting>
            {
                DocumentBaseId = inventoryCard.DocumentBaseId,
                Date = inventoryCard.Date,
                Name = inventoryCard.Number,
                Number = inventoryCard.Number,
                Type = (Abstractions.Enums.LinkedDocumentType)LinkedDocumentType.InventoryCard,
                Postings = new[] { posting }
            };
            return (null, linkedDocument);
        }

        private static (UsnTaxPosting, LinkedDocumentTaxPostings<UsnTaxPosting>) GetStatementPosting(PaymentToSupplierUsnPostingsBusinessModel model, PurchasesStatement statement, decimal postingSum)
        {
            if (IsTaxable(statement) == false)
            {
                return (null, null);
            }

            if (statement.TaxPostingType == Enums.ProvidePostingType.ByHand)
            {
                return (null, null);
            }

            var date = DateTimeHelper.Max(statement.Date, model.Date);
            var taxCoef = PurchasesDocTaxSumHelper.GetTaxCoefficients(statement, model.NdsRatePeriods).Services;
            var posting = new UsnTaxPosting
            {
                Date = date,
                Sum = Math.Round(postingSum * taxCoef, 2, MidpointRounding.AwayFromZero),
                Direction = TaxPostingDirection.Outgoing,
                DocumentId = model.Date >= statement.Date
                    ? model.DocumentBaseId
                    : statement.DocumentBaseId,
                DocumentDate = date,
                DocumentNumber = model.Number,
                Description = PaymentToSupplierUsnPostingsDescriptionFormer.GetDescription(model, statement),
                RelatedDocumentBaseIds = new[] { model.DocumentBaseId, statement.DocumentBaseId }
            };

            if (posting.DocumentId == model.DocumentBaseId)
            {
                return (posting, null);
            }

            var linkedDocument = new LinkedDocumentTaxPostings<UsnTaxPosting>
            {
                DocumentBaseId = statement.DocumentBaseId,
                Date = statement.Date,
                Name = statement.Number,
                Number = statement.Number,
                Type = (Abstractions.Enums.LinkedDocumentType)LinkedDocumentType.Statement,
                Postings = new[] { posting }
            };
            return (null, linkedDocument);
        }

        // тут создается некий франкенштейн из первичного документа и ИК (или п/п)
        private static (UsnTaxPosting, LinkedDocumentTaxPostings<UsnTaxPosting>) GetStatementFromFixedAssetInvestmentPosting(PaymentToSupplierUsnPostingsBusinessModel request, PurchasesStatement statement, decimal postingSum)
        {
            if (IsTaxable(statement) == false)
            {
                return (null, null);
            }

            if (request.InventoryCardsFromFixedAssetInvestment.TryGetValue(statement.DocumentBaseId, out var inventoryCard) == false)
            {
                return (null, null);
            }

            if (inventoryCard.Cost > TaxPostingLimit)
            {
                return (null, null);
            }

            var inventoryCardDate = inventoryCard.CommissioningDate ?? inventoryCard.Date;
            var date = DateTimeHelper.Max(inventoryCardDate, request.Date);
            var serviceRate = PurchasesDocTaxSumHelper
                .GetTaxCoefficients(statement, request.NdsRatePeriods)
                .Services;

            var posting = new UsnTaxPosting
            {
                Date = date,
                Sum = Math.Round(serviceRate * postingSum, 2, MidpointRounding.AwayFromZero),
                Direction = TaxPostingDirection.Outgoing,
                DocumentId = request.Date >= inventoryCardDate
                    ? request.DocumentBaseId
                    : inventoryCard.DocumentBaseId,
                DocumentDate = date,
                DocumentNumber = request.Number,
                Description = PaymentToSupplierUsnPostingsDescriptionFormer.GetDescription(request, statement),
                RelatedDocumentBaseIds = new[] { request.DocumentBaseId, inventoryCard.DocumentBaseId }
            };

            if (posting.DocumentId == request.DocumentBaseId)
            {
                return (posting, null);
            }

            var linkedDocument = new LinkedDocumentTaxPostings<UsnTaxPosting>
            {
                DocumentBaseId = inventoryCard.DocumentBaseId,
                Date = inventoryCard.Date,
                Name = inventoryCard.Number,
                Number = inventoryCard.Number,
                Type = (Abstractions.Enums.LinkedDocumentType)LinkedDocumentType.InventoryCard,
                Postings = new[] { posting }
            };
            return (null, linkedDocument);
        }

        private static (UsnTaxPosting[], LinkedDocumentTaxPostings<UsnTaxPosting>) GetUpdPostings(PaymentToSupplierUsnPostingsBusinessModel model, PurchasesUpd upd, decimal postingSum, IDictionary<long, StockProductTypeEnum> stockProductsTypes)
        {
            if (IsTaxable(upd) == false)
            {
                return (null, null);
            }

            if (upd.TaxPostingType == Enums.ProvidePostingType.ByHand)
            {
                return (null, null);
            }

            var postings = new List<UsnTaxPosting>(2);
            var date = DateTimeHelper.Max(upd.ForgottenDocumentDate ?? upd.Date, model.Date);

            var taxCoefficients = PurchasesDocTaxSumHelper.GetTaxCoefficients(
                upd,
                model.NdsRatePeriods,
                stockProductsTypes);

            var materialRate = taxCoefficients.Materials;
            if (materialRate > 0)
            {
                var materialPostingSum = Math.Round(postingSum * materialRate, 2, MidpointRounding.AwayFromZero);
                postings.Add(new UsnTaxPosting
                {
                    Date = date,
                    Sum = materialPostingSum,
                    Direction = TaxPostingDirection.Outgoing,
                    DocumentId = model.Date >= (upd.ForgottenDocumentDate ?? upd.Date)
                        ? model.DocumentBaseId
                        : upd.DocumentBaseId,
                    DocumentDate = date,
                    DocumentNumber = model.Number,
                    Description = PaymentToSupplierUsnPostingsDescriptionFormer.GetMaterialDescription(model, upd),
                    RelatedDocumentBaseIds = new[] { model.DocumentBaseId, upd.DocumentBaseId }
                });
            }

            var serviceRate = taxCoefficients.Services;
            if (serviceRate > 0)
            {
                var servicePostingSum = Math.Round(postingSum * serviceRate, 2, MidpointRounding.AwayFromZero);
                postings.Add(new UsnTaxPosting
                {
                    Date = date,
                    Sum = servicePostingSum,
                    Direction = TaxPostingDirection.Outgoing,
                    DocumentId = model.Date >= (upd.ForgottenDocumentDate ?? upd.Date)
                        ? model.DocumentBaseId
                        : upd.DocumentBaseId,
                    DocumentDate = date,
                    DocumentNumber = model.Number,
                    Description = PaymentToSupplierUsnPostingsDescriptionFormer.GetServiceDescription(model, upd),
                    RelatedDocumentBaseIds = new[] { model.DocumentBaseId, upd.DocumentBaseId }
                });
            }

            if (model.Date >= (upd.ForgottenDocumentDate ?? upd.Date))
            {
                return (postings.ToArray(), null);
            }

            var linkedDocument = new LinkedDocumentTaxPostings<UsnTaxPosting>
            {
                DocumentBaseId = upd.DocumentBaseId,
                Date = upd.Date,
                Name = upd.Number,
                Number = upd.Number,
                Type = (Abstractions.Enums.LinkedDocumentType)LinkedDocumentType.Upd,
                Postings = postings.ToArray()
            };
            return (null, linkedDocument);
        }

        private static (UsnTaxPosting, LinkedDocumentTaxPostings<UsnTaxPosting>) GetInventoryCardPosting(PaymentToSupplierUsnPostingsBusinessModel model, InventoryCard inventoryCard, decimal postingSum)
        {
            if (IsTaxable(inventoryCard) == false)
            {
                return (null, null);
            }

            if (inventoryCard.Cost > TaxPostingLimit)
            {
                return (null, null);
            }

            var inventoryCardDate = inventoryCard.CommissioningDate ?? inventoryCard.Date;
            var date = DateTimeHelper.Max(inventoryCardDate, model.Date);
            var posting = new UsnTaxPosting
            {
                Date = date,
                Sum = Math.Round(postingSum, 2, MidpointRounding.AwayFromZero),
                Direction = TaxPostingDirection.Outgoing,
                DocumentId = model.Date >= inventoryCardDate
                    ? model.DocumentBaseId
                    : inventoryCard.DocumentBaseId,
                DocumentDate = date,
                DocumentNumber = model.Number,
                Description = PaymentToSupplierUsnPostingsDescriptionFormer.GetDescription(model, inventoryCard),
                RelatedDocumentBaseIds = new[] { model.DocumentBaseId, inventoryCard.DocumentBaseId }
            };

            if (posting.DocumentId == model.DocumentBaseId)
            {
                return (posting, null);
            }

            var linkedDocument = new LinkedDocumentTaxPostings<UsnTaxPosting>
            {
                DocumentBaseId = inventoryCard.DocumentBaseId,
                Date = inventoryCard.Date,
                Name = inventoryCard.Number,
                Number = inventoryCard.Number,
                Type = (Abstractions.Enums.LinkedDocumentType)LinkedDocumentType.InventoryCard,
                Postings = new[] { posting }
            };
            return (null, linkedDocument);
        }

        private static void AddOrUpdateLinkedDocument(List<LinkedDocumentTaxPostings<UsnTaxPosting>> linkedDocuments, LinkedDocumentTaxPostings<UsnTaxPosting> linkedDocument)
        {
            var existsLinkedDocument = linkedDocuments.FirstOrDefault(x => x.DocumentBaseId == linkedDocument.DocumentBaseId);
            if (existsLinkedDocument != null)
            {
                var postings = new List<UsnTaxPosting>(existsLinkedDocument.Postings.Count + linkedDocument.Postings.Count);
                postings.AddRange(existsLinkedDocument.Postings);
                postings.AddRange(linkedDocument.Postings);
                existsLinkedDocument.Postings = postings.ToArray();
                return;
            }
            linkedDocuments.Add(linkedDocument);
        }
    }
}