using FluentAssertions;
using Moedelo.Docs.Enums;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.Money.Providing.Business.Estate.Models;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.TaxPostings;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Money.Providing.Business.Stock.Models;
using Moedelo.Money.Providing.Business.TaxPostings.Models;
using Moedelo.Stock.Enums;
using Moedelo.TaxPostings.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Money.Providing.Business.NdsRatePeriods.Models;
using Moedelo.Requisites.Enums.NdsRatePeriods;
using TaxationSystemType = Moedelo.Requisites.Enums.TaxationSystems.TaxationSystemType;
using TaxPostingStatus = Moedelo.TaxPostings.Enums.TaxPostingStatus;

namespace Moedelo.Money.Providing.Business.Tests.PaymentOrders.Outgoing.PaymentToSupplier
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.SingleInstance)]
    public class PaymentToSupplierUsnProfitAndOutgoGeneratorTests
    {
        private const long DefaultOperationBaseId = 12;
        private const string DefaultOperationNumber = "12/2";
        private static readonly DateTime DefaultOperationDate = new DateTime(2018, 12, 15);

        private const long DefaultDocumentBaseId = 21;
        private const string DefaultDocumentNumber = "243";
        private static readonly DateTime DefaultDocumentDate = new DateTime(2018, 12, 14);

        private const string ForgottenDocumentNumber = "28";
        private static readonly DateTime ForgottenDocumentDate = new DateTime(2017, 11, 3);

        private const long ProductId = 46;
        private const long MaterialId = 47;

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnNoTax_IfHasNoDocuments(TaxationSystemType taxSystem)
        {
            var model = GetBusinessModel(taxSystem);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.NotTax)
            {
                Message = "Не учитывается. Добавьте документ."
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Statement, TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill, TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Upd, TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Statement, TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill, TaxationSystemType.Envd)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Upd, TaxationSystemType.Envd)]
        public void Generate_ShouldReturnNoTax_IfHasNotTaxableDocument(TaxationSystemType taxSystem, LinkedDocumentType documentType, TaxationSystemType documentTaxSystem)
        {
            var model = GetBusinessModel(taxSystem);
            CreateDocument(model, documentType, documentTaxSystem, 1000m);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Statement)]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Statement)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill)]
        public void Generate_ShouldReturnNoTax_IfHasCompensatedDocument(TaxationSystemType taxSystem, LinkedDocumentType documentType)
        {
            var model = GetBusinessModel(taxSystem);
            CreateDocument(model, documentType, taxSystem, 1000m, isCompensated: true);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill)]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Upd)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Upd)]
        public void Generate_ShouldReturnMessage_IfHasDocumentWithProducts(TaxationSystemType taxSystem, LinkedDocumentType documentType)
        {
            var model = GetBusinessModel(taxSystem);
            CreateDocument(model, documentType, taxSystem, 1000m, productRate: 1m);


            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.NotTax)
            {
                TaxationSystemType = taxSystem,
                Message = "Расход в виде стоимости товара будет учитываться при расчете налога, если факт его покупки и продажи документально подтвержден"
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill)]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Statement)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Statement)]
        public void Generate_ShouldReturnMessage_IfHasDocumentLinkedWithInventoryCardOverTaxLimit(TaxationSystemType taxSystem, LinkedDocumentType documentType)
        {
            var model = GetBusinessModel(taxSystem);
            CreateDocument(model, documentType, taxSystem, 1000m, isFromFixedAssetInvestment: true);
            model.InventoryCardsFromFixedAssetInvestment = new Dictionary<long, InventoryCard>
            {
                { DefaultDocumentBaseId, new InventoryCard { Cost = PaymentToSupplierUsnPostingsGenerator.TaxPostingLimit + 1 } }
            };

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.NotTax)
            {
                TaxationSystemType = taxSystem,
                Message = "Расход по основному средству будет учитываться при расчете налога"
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Statement, "за услуги по акту")]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Upd, "за услуги по УПД")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Statement, "за услуги по акту")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Upd, "за услуги по УПД")]
        public void Generate_ShouldReturnPosting_IfHasDocumentWithService(TaxationSystemType taxSystem, LinkedDocumentType documentType, string documentName)
        {
            var model = GetBusinessModel(taxSystem);
            CreateDocument(model, documentType, taxSystem, 500m, serviceRate: 1m);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 500m,
                        Direction = TaxPostingDirection.Outgoing,
                        DocumentId = DefaultOperationBaseId,
                        DocumentDate = DefaultOperationDate,
                        DocumentNumber = DefaultOperationNumber,
                        Description = $"Оплата {documentName} №{DefaultDocumentNumber} от {DefaultDocumentDate:dd.MM.yyy} от контрагента Иванов.",
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill, "за материалы по накладной")]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Upd, "за материалы по УПД")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill, "за материалы по накладной")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Upd, "за материалы по УПД")]
        public void Generate_ShouldReturnPosting_IfHasDocumentWithMaterial(TaxationSystemType taxSystem, LinkedDocumentType documentType, string documentName)
        {
            var model = GetBusinessModel(taxSystem);
            CreateDocument(model, documentType, taxSystem, 500m, materialRate: 1m);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 500m,
                        Direction = TaxPostingDirection.Outgoing,
                        DocumentId = DefaultOperationBaseId,
                        DocumentDate = DefaultOperationDate,
                        DocumentNumber = DefaultOperationNumber,
                        Description = $"Оплата {documentName} №{DefaultDocumentNumber} от {DefaultDocumentDate:dd.MM.yyy} от контрагента Иванов.",
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Upd, "за материалы по УПД")]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill, "за материалы по накладной")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Upd, "за материалы по УПД")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill, "за материалы по накладной")]
        public void Generate_ShouldReturnPosting_IfHasDocumentWithPartRate(TaxationSystemType taxSystem, LinkedDocumentType documentType, string documentName)
        {
            var model = GetBusinessModel(taxSystem);
            CreateDocument(model, documentType, taxSystem, 1000m, materialRate: 0.5m, productRate: 0.5m);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 500m,
                        Direction = TaxPostingDirection.Outgoing,
                        DocumentId = DefaultOperationBaseId,
                        DocumentDate = DefaultOperationDate,
                        DocumentNumber = DefaultOperationNumber,
                        Description = $"Оплата {documentName} №{DefaultDocumentNumber} от {DefaultDocumentDate:dd.MM.yyy} от контрагента Иванов.",
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Upd)]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Upd)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Upd)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Upd)]
        public void Generate_ShouldReturnPostings_IfHasUpdWithPartRates(TaxationSystemType taxSystem, LinkedDocumentType documentType)
        {
            var model = GetBusinessModel(taxSystem);
            CreateDocument(model, documentType, taxSystem, 300m, materialRate: 0.5m, serviceRate: 0.5m);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 150m,
                        Direction = TaxPostingDirection.Outgoing,
                        DocumentId = DefaultOperationBaseId,
                        DocumentDate = DefaultOperationDate,
                        DocumentNumber = DefaultOperationNumber,
                        Description = $"Оплата за материалы по УПД №{DefaultDocumentNumber} от {DefaultDocumentDate:dd.MM.yyy} от контрагента Иванов.",
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                    },
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 150m,
                        Direction = TaxPostingDirection.Outgoing,
                        DocumentId = DefaultOperationBaseId,
                        DocumentDate = DefaultOperationDate,
                        DocumentNumber = DefaultOperationNumber,
                        Description = $"Оплата за услуги по УПД №{DefaultDocumentNumber} от {DefaultDocumentDate:dd.MM.yyy} от контрагента Иванов.",
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnPosting_IfHasInventoryCard(TaxationSystemType taxSystem)
        {
            var model = GetBusinessModel(taxSystem);
            CreateDocument(model, LinkedDocumentType.InventoryCard, taxSystem, 500m);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 500m,
                        Direction = TaxPostingDirection.Outgoing,
                        DocumentId = DefaultOperationBaseId,
                        DocumentDate = DefaultOperationDate,
                        DocumentNumber = DefaultOperationNumber,
                        Description = $"Оплата по инвентарной карточке №{DefaultDocumentNumber} от {DefaultDocumentDate:dd.MM.yyy} от контрагента Иванов.",
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Statement, "за услуги по акту", 0, 1)]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill, "за материалы по накладной", 1, 0)]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Upd, "за услуги по УПД", 0, 1)]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Upd, "за материалы по УПД", 1, 0)]
        //[TestCase(TaxationSystemType.Usn, LinkedDocumentType.InventoryCard, "по инвентарной карточке")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Statement, "за услуги по акту", 0, 1)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill, "за материалы по накладной", 1, 0)]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Upd, "за услуги по УПД", 0, 1)]
        //[TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.InventoryCard, "по инвентарной карточке")]
        public void Generate_ShouldReturnLinkedDocumentsPostings_IfHasDocumentAndDocumentDateIsGreatThanPaymentDate(
            TaxationSystemType taxSystem, 
            LinkedDocumentType documentType, 
            string documentName,
            int materialRate,
            int serviceRate)
        {
            var documentDate = DefaultDocumentDate.AddDays(5);
            var model = GetBusinessModel(taxSystem);
            CreateDocument(model, documentType, taxSystem, 500m, documentDate: documentDate, materialRate: materialRate, serviceRate: serviceRate);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.ByLinkedDocument)
            {
                TaxationSystemType = taxSystem,
                LinkedDocuments = new[]
                {
                    new LinkedDocumentTaxPostings<UsnTaxPosting>
                    {
                        DocumentBaseId = DefaultDocumentBaseId,
                        Date = documentDate,
                        Number = DefaultDocumentNumber,
                        Type = (Abstractions.Enums.LinkedDocumentType)documentType,
                        Postings = new[]
                        {
                            new UsnTaxPosting
                            {
                                Date = documentDate,
                                DocumentDate = documentDate,
                                Sum = 500m,
                                Direction = TaxPostingDirection.Outgoing,
                                DocumentId = DefaultDocumentBaseId,
                                DocumentNumber = DefaultOperationNumber,
                                Description = $"Оплата {documentName} №{DefaultDocumentNumber} от {documentDate:dd.MM.yyy} от контрагента Иванов. Оплата по платежному поручению №{model.Number} от {model.Date:dd.MM.yyyy}.",
                                RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                            }
                        }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnLinkedDocumentsPostings_IfHasInventoryCardAndInventoryCardDateIsGreatThanPaymentDate(TaxationSystemType taxSystem)
        {
            var documentDate = DefaultDocumentDate.AddDays(5);

            var model = GetBusinessModel(taxSystem);
            CreateDocument(model, LinkedDocumentType.InventoryCard, taxSystem, 500m, documentDate: documentDate);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.ByLinkedDocument)
            {
                TaxationSystemType = taxSystem,
                LinkedDocuments = new[]
                {
                    new LinkedDocumentTaxPostings<UsnTaxPosting>
                    {
                        DocumentBaseId = DefaultDocumentBaseId,
                        Date = documentDate,
                        Number = DefaultDocumentNumber,
                        Type = (Abstractions.Enums.LinkedDocumentType)LinkedDocumentType.InventoryCard,
                        Postings = new[]
                        {
                            new UsnTaxPosting
                            {
                                Date = documentDate,
                                DocumentDate = documentDate,
                                Sum = 500m,
                                Direction = TaxPostingDirection.Outgoing,
                                DocumentId = DefaultDocumentBaseId,
                                DocumentNumber = DefaultOperationNumber,
                                Description = $"Оплата по инвентарной карточке №{DefaultDocumentNumber} от {documentDate:dd.MM.yyy} от контрагента Иванов. Оплата по платежному поручению №{model.Number} от {model.Date:dd.MM.yyyy}.",
                                RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                            }
                        }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill, "за материалы по накладной")]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Statement, "за услуги по акту")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill, "за материалы по накладной")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Statement, "за услуги по акту")]
        public void Generate_ShouldReturnPosting_IfHasDocumentLinkedWithInventoryCard(TaxationSystemType taxSystem, LinkedDocumentType documentType, string documentName)
        {
            var documentDate = DefaultOperationDate.AddDays(-1);
            var inventoryCard = new InventoryCard
            {
                DocumentBaseId = 1234L,
                Date = documentDate,
                Number = "221",
                Cost = 1000m
            };
            var model = GetBusinessModel(taxSystem);
            CreateDocument(
                model,
                documentType,
                taxSystem,
                500m,
                isFromFixedAssetInvestment: true,
                documentDate: documentDate,
                productRate: 1 // для накладной (заодно проверим, что товары учитываются как материалы)
            );

            model.InventoryCardsFromFixedAssetInvestment = new Dictionary<long, InventoryCard>
            {
                { DefaultDocumentBaseId, inventoryCard }
            };

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 500m,
                        Direction = TaxPostingDirection.Outgoing,
                        DocumentId = DefaultOperationBaseId,
                        DocumentDate = DefaultOperationDate,
                        DocumentNumber = DefaultOperationNumber,
                        Description = $"Оплата {documentName} №{DefaultDocumentNumber} от {DefaultDocumentDate:dd.MM.yyy} от контрагента Иванов.",
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, inventoryCard.DocumentBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill, "за материалы по накладной")]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Statement, "за услуги по акту")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill, "за материалы по накладной")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Statement, "за услуги по акту")]
        public void Generate_ShouldReturnPosting_IfHasDocumentLinkedWithInventoryCardAndDocumentDateIsGreatThanDocumentDate(TaxationSystemType taxSystem, LinkedDocumentType documentType, string documentName)
        {
            var documentDate = DefaultOperationDate.AddDays(1);
            var inventoryCard = new InventoryCard
            {
                DocumentBaseId = 1234L,
                Date = DefaultOperationDate.AddDays(1),
                Number = "221",
                Cost = 1000m
            };
            var model = GetBusinessModel(taxSystem);
            CreateDocument(
                model,
                documentType,
                taxSystem,
                500m,
                isFromFixedAssetInvestment: true,
                documentDate: documentDate,
                productRate: 1 // для накладной (заодно проверим, что товары учитываются как материалы)
            );
            model.InventoryCardsFromFixedAssetInvestment = new Dictionary<long, InventoryCard>
            {
                { DefaultDocumentBaseId, inventoryCard }
            };

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.ByLinkedDocument)
            {
                TaxationSystemType = taxSystem,
                LinkedDocuments = new[]
                {
                    new LinkedDocumentTaxPostings<UsnTaxPosting>
                    {
                        DocumentBaseId = inventoryCard.DocumentBaseId,
                        Date = inventoryCard.Date,
                        Number = inventoryCard.Number,
                        Type = (Abstractions.Enums.LinkedDocumentType)LinkedDocumentType.InventoryCard,
                        Postings = new[]
                        {
                            new UsnTaxPosting
                            {
                                // тут полная "каша": в описании первичный документ, номер от платежки, а идентификатор от ИК, но видимо, "как на проде" (изменений по коду не было, починил тесты)
                                Date = documentDate,
                                DocumentDate = documentDate,
                                Sum = 500m,
                                Direction = TaxPostingDirection.Outgoing,
                                DocumentId = inventoryCard.DocumentBaseId,
                                DocumentNumber = DefaultOperationNumber,
                                Description = $"Оплата {documentName} №{DefaultDocumentNumber} от {documentDate:dd.MM.yyy} от контрагента Иванов. Оплата по платежному поручению №{model.Number} от {model.Date:dd.MM.yyyy}.",
                                RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, inventoryCard.DocumentBaseId }
                            }
                        }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnNoPosting_IfNdsSumGreaterThanOperationSum(TaxationSystemType taxSystem)
        {
            var model = new PaymentToSupplierUsnPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 100m,
                IncludeNds = true,
                NdsSum = 200m
            };

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill, "за материалы по накладной")]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Upd, "за материалы по УПД")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill, "за материалы по накладной")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Upd, "за материалы по УПД")]
        public void Generate_ShouldReturnPosting_IfHasForgottenDocument(TaxationSystemType taxSystem, LinkedDocumentType documentType, string documentName)
        {
            var paymentDate = DefaultDocumentDate.AddDays(-5);
            var model = GetBusinessModel(taxSystem);
            model.Date = paymentDate;
            CreateDocument(model, documentType, taxSystem, 1000m, isForgotten: true, materialRate: 0.5m, productRate:0.5m);

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = paymentDate,
                        Sum = 500m,
                        Direction = TaxPostingDirection.Outgoing,
                        DocumentId = DefaultOperationBaseId,
                        DocumentDate = paymentDate,
                        DocumentNumber = DefaultOperationNumber,
                        Description = $"Оплата {documentName} №{ForgottenDocumentNumber} от {ForgottenDocumentDate:dd.MM.yyy} от контрагента Иванов.",
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test(Description = "Документы-вложения во ВА в составе ИК до 100 000 р. на УНС с НДС 20% учитываются в НУ без НДС")]
        [TestCase(LinkedDocumentType.Waybill)]
        [TestCase(LinkedDocumentType.Statement)]
        public void Generate_ShouldReturnPosting_IfHasDocumentLinkedWithInventoryCardAndUsnOnStandardNds(LinkedDocumentType documentType)
        {
            var docDate = new DateTime(2025, 01, 02);
            var paymentDate = new DateTime(2025, 01, 07);
            var inventoryCardDate = new DateTime(2025, 01, 05);
            
            var taxSystem = TaxationSystemType.Usn;
            var model = GetBusinessModel(taxSystem, 600m);
            model.Date = paymentDate;
            CreateDocument(
                model,
                documentType,
                taxSystem,
                600m,
                isFromFixedAssetInvestment: true,
                documentDate: docDate,
                ndsRate: 0.2m,
                productRate: 1 // для накладной (заодно проверим, что товары учитываются как материалы)
            );
            // УСН на "стандартном НДС" в дату документа
            model.NdsRatePeriods = new[]
            {
                new NdsRatePeriod
                {
                    Rate = NdsRateType.Nds20,
                    StartDate = docDate, // состояние настройки определяем на дату документа
                    EndDate = docDate,
                }
            };
            model.InventoryCardsFromFixedAssetInvestment = new Dictionary<long, InventoryCard>
            {
                {
                    DefaultDocumentBaseId,
                    new InventoryCard
                    {
                        DocumentBaseId = 1234L,
                        Date = inventoryCardDate,
                        Number = "221",
                        Cost = 1000m
                    }
                }
            };

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting { Date = paymentDate, Sum = 500m } 
                }
            };

            actual.Should()
                .BeEquivalentTo(expected, opt => opt
                    .Using<UsnTaxPosting>(c => c
                        .Subject.Should().BeEquivalentTo(
                            c.Expectation,
                            opt2 => opt2
                                .Including(x => x.Sum)
                                .Including(x => x.Date)
                        )
                    ).WhenTypeIs<UsnTaxPosting>()
                );
        }
        
        [Test(Description = "Документы-вложения во ВА в составе ИК до 100 000 р. на УСН учитываются в НУ с НДС при настройке НДС 5/7/Без НДС")]
        [TestCase(LinkedDocumentType.Waybill, NdsRateType.Nds5)]
        [TestCase(LinkedDocumentType.Waybill, NdsRateType.Nds7)]
        [TestCase(LinkedDocumentType.Waybill, NdsRateType.None)]
        [TestCase(LinkedDocumentType.Waybill, default)] // нет настройки (приравниваем к "без ндс")
        [TestCase(LinkedDocumentType.Statement, NdsRateType.Nds5)]
        [TestCase(LinkedDocumentType.Statement, NdsRateType.Nds7)]
        [TestCase(LinkedDocumentType.Statement, NdsRateType.None)]
        [TestCase(LinkedDocumentType.Statement, default)]
        public void Generate_ShouldReturnPosting_IfHasDocumentLinkedWithInventoryCardAndUsnOnNotStandardNds(LinkedDocumentType documentType, NdsRateType ndsRateSetting)
        {
            var docDate = new DateTime(2025, 01, 02);
            var paymentDate = new DateTime(2025, 01, 07);
            var inventoryCardDate = new DateTime(2025, 01, 05);
            
            var taxSystem = TaxationSystemType.Usn;
            var model = GetBusinessModel(taxSystem, 600m);
            model.Date = paymentDate;
            CreateDocument(
                model,
                documentType,
                taxSystem,
                600m,
                isFromFixedAssetInvestment: true,
                documentDate: docDate,
                ndsRate: 0.2m,
                productRate: 1 // для накладной (заодно проверим, что товары учитываются как материалы)
            );
            // УСН на "стандартном НДС" в дату документа
            model.NdsRatePeriods = new[]
            {
                new NdsRatePeriod
                {
                    Rate = ndsRateSetting,
                    StartDate = docDate, // состояние настройки определяем на дату документа
                    EndDate = docDate,
                }
            };
            model.InventoryCardsFromFixedAssetInvestment = new Dictionary<long, InventoryCard>
            {
                {
                    DefaultDocumentBaseId,
                    new InventoryCard
                    {
                        DocumentBaseId = 1234L,
                        Date = inventoryCardDate,
                        Number = "221",
                        Cost = 1000m
                    }
                }
            };

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting { Date = paymentDate, Sum = 600m } 
                }
            };

            actual.Should()
                .BeEquivalentTo(expected, opt => opt
                    .Using<UsnTaxPosting>(c => c
                        .Subject.Should().BeEquivalentTo(
                            c.Expectation,
                            opt2 => opt2
                                .Including(x => x.Sum)
                                .Including(x => x.Date)
                        )
                    ).WhenTypeIs<UsnTaxPosting>()
                );
        }
        
        [Test]
        [TestCase(LinkedDocumentType.Waybill)]
        [TestCase(LinkedDocumentType.Upd)]
        public void Generate_ShouldReturnPostingByMaterialsWithoutNds_IfUsnOnStandardNds(LinkedDocumentType documentType)
        {
            var taxSystem = TaxationSystemType.Usn;
            var model = GetBusinessModel(taxSystem, sum: 600); // платеж на 600 р
            model.Date = DefaultDocumentDate; // для простоты: п/п от одной даты с документом
            
            // документ на 840р: материалы - 240, товары - 600 (НДС 20%)
            var materialRate = 240m / 840m;
            var productRate = 600m / 840m;
            CreateDocument(model, documentType, taxSystem, 840m, materialRate: materialRate, productRate: productRate, ndsRate: 0.2m);
            model.DocumentLinks.ElementAt(0).LinkSum = 450; // платеж связан с документом на 450 р
            // УСН на "стандартном НДС" в дату документа
            model.NdsRatePeriods = new[]
            {
                new NdsRatePeriod
                {
                    Rate = NdsRateType.Nds20,
                    StartDate = DefaultDocumentDate.AddDays(-1),
                    EndDate = DefaultDocumentDate.AddDays(1),
                }
            };

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    // 450 * (240 / 840) * (200 / 240)
                    new UsnTaxPosting { Sum = 107.14m } 
                }
            };

            actual.Should()
                .BeEquivalentTo(expected, opt => opt
                    .Using<UsnTaxPosting>(c => c
                        .Subject.Should().BeEquivalentTo(
                            c.Expectation,
                            opt2 => opt2.Including(x => x.Sum)
                        )
                    ).WhenTypeIs<UsnTaxPosting>()
                );
        }
        
        [Test]
        [TestCase(LinkedDocumentType.Waybill)]
        [TestCase(LinkedDocumentType.Upd)]
        public void Generate_ShouldReturnPostingByMaterialsWithoutNds_IfUsnOnStandardNds2026(LinkedDocumentType documentType)
        {
            var taxSystem = TaxationSystemType.Usn;
            var model = GetBusinessModel(taxSystem, sum: 600); // платеж на 600 р
            model.Date = new DateTime(2026, 01, 01); // для простоты: п/п от одной даты с документом
            
            // документ на 854р: материалы - 244, товары - 610 (НДС 22%)
            var materialRate = 244m / 854m;
            var productRate = 610m / 854m;
            CreateDocument(model, documentType, taxSystem, 854m, documentDate: model.Date, materialRate: materialRate, productRate: productRate, ndsRate: 0.22m);
            model.DocumentLinks.ElementAt(0).LinkSum = 450; // платеж связан с документом на 450 р
            // УСН на "стандартном НДС" в дату документа
            model.NdsRatePeriods = new[]
            {
                new NdsRatePeriod
                {
                    Rate = NdsRateType.Nds22,
                    StartDate = model.Date,
                    EndDate = model.Date.AddDays(1),
                }
            };

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    // 450 * (244 / 854) * (200 / 244)
                    new UsnTaxPosting { Sum = 105.39m } 
                }
            };

            actual.Should()
                .BeEquivalentTo(expected, opt => opt
                    .Using<UsnTaxPosting>(c => c
                        .Subject.Should().BeEquivalentTo(
                            c.Expectation,
                            opt2 => opt2.Including(x => x.Sum)
                        )
                    ).WhenTypeIs<UsnTaxPosting>()
                );
        }
        
        [Test]
        [TestCase(LinkedDocumentType.Statement)]
        [TestCase(LinkedDocumentType.Upd)]
        public void Generate_ShouldReturnPostingByServicesWithoutNds_IfUsnOnStandardNds(LinkedDocumentType documentType)
        {
            var taxSystem = TaxationSystemType.Usn;
            var model = GetBusinessModel(taxSystem, sum: 600); // платеж на 600 р
            model.Date = DefaultDocumentDate; // для простоты: п/п от одной даты с документом
            
            // документ на 240 р (все услуги, НДС 20%)
            CreateDocument(model, documentType, taxSystem, 240m, serviceRate: 1m, ndsRate: 0.2m);
            model.DocumentLinks.ElementAt(0).LinkSum = 120; // платеж связан с документом на 120 р
            // УСН на "стандартном НДС" в дату документа
            model.NdsRatePeriods = new[]
            {
                new NdsRatePeriod
                {
                    Rate = NdsRateType.Nds20,
                    StartDate = DefaultDocumentDate.AddDays(-1),
                    EndDate = DefaultDocumentDate.AddDays(1),
                }
            };

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    // 120 * (240 / 240) * (200 / 240)
                    new UsnTaxPosting { Sum = 100m }
                }
            };

            actual.Should()
                .BeEquivalentTo(expected, opt => opt
                    .Using<UsnTaxPosting>(c => c
                        .Subject.Should().BeEquivalentTo(
                            c.Expectation,
                            opt2 => opt2.Including(x => x.Sum)
                        )
                    ).WhenTypeIs<UsnTaxPosting>()
                );
        }

        [Test]
        [TestCase(LinkedDocumentType.Statement)]
        [TestCase(LinkedDocumentType.Upd)]
        public void Generate_ShouldReturnPostingByServicesWithoutNds_IfUsnOnStandardNds2026(LinkedDocumentType documentType)
        {
            var taxSystem = TaxationSystemType.Usn;
            var model = GetBusinessModel(taxSystem, sum: 600); // платеж на 600 р
            model.Date = new DateTime(2026, 01, 01); // для простоты: п/п от одной даты с документом
            
            // документ на 244 р (все услуги, НДС 22%)
            CreateDocument(model, documentType, taxSystem, 244m, documentDate: model.Date, serviceRate: 1m, ndsRate: 0.22m);
            model.DocumentLinks.ElementAt(0).LinkSum = 120; // платеж связан с документом на 120 р
            // УСН на "стандартном НДС" в дату документа
            model.NdsRatePeriods = new[]
            {
                new NdsRatePeriod
                {
                    Rate = NdsRateType.Nds22,
                    StartDate = model.Date.AddDays(-1),
                    EndDate = model.Date.AddDays(1),
                }
            };

            var actual = PaymentToSupplierUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    // 120 * (244 / 244) * (200 / 244)
                    new UsnTaxPosting { Sum = 98.36m }
                }
            };

            actual.Should()
                .BeEquivalentTo(expected, opt => opt
                    .Using<UsnTaxPosting>(c => c
                        .Subject.Should().BeEquivalentTo(
                            c.Expectation,
                            opt2 => opt2.Including(x => x.Sum)
                        )
                    ).WhenTypeIs<UsnTaxPosting>()
                );
        }

        private static PaymentToSupplierUsnPostingsBusinessModel GetBusinessModel(TaxationSystemType taxSystem, decimal sum = 1000)
        {
            return new PaymentToSupplierUsnPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                IsUsnProfitAndOutgo = true,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = sum,
                Kontragent = new Kontragent
                {
                    Name = "Иванов"
                }
            };
        }

        private static void CreateDocument(
            PaymentToSupplierUsnPostingsBusinessModel model,
            LinkedDocumentType documentType,
            TaxationSystemType taxSystem,
            decimal sum,
            DateTime? documentDate = null,
            bool isCompensated = false,
            bool isFromFixedAssetInvestment = false,
            bool isForgotten = false,
            decimal productRate = 0m,
            decimal materialRate = 0m,
            decimal serviceRate = 0m,
            decimal ndsRate = 0m)
        {
            model.DocumentLinks = new[]
            {
                new DocumentLink
                {
                    DocumentBaseId = DefaultDocumentBaseId,
                    Type = documentType,
                    LinkSum = sum
                }
            };
            switch (documentType)
            {
                case LinkedDocumentType.Waybill:
                    CreateWaybill(model, (Enums.TaxationSystemType)taxSystem, documentDate ?? DefaultDocumentDate, sum, isCompensated, isFromFixedAssetInvestment, isForgotten, productRate, materialRate, ndsRate);
                    break;
                case LinkedDocumentType.Statement:
                    CreateStatement(model, (Enums.TaxationSystemType)taxSystem, documentDate ?? DefaultDocumentDate, sum, isCompensated, isFromFixedAssetInvestment, ndsRate);
                    break;
                case LinkedDocumentType.Upd:
                    CreateUpd(model, (Enums.TaxationSystemType)taxSystem, documentDate ?? DefaultDocumentDate, sum, isForgotten, productRate, materialRate, serviceRate, ndsRate);
                    break;
                case LinkedDocumentType.InventoryCard:
                    CreateInventoryCard(model, (Enums.TaxationSystemType)taxSystem, documentDate ?? DefaultDocumentDate, sum);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private static void CreateWaybill(PaymentToSupplierUsnPostingsBusinessModel model, Enums.TaxationSystemType taxSystem, DateTime date, decimal sum, bool isCompensated, bool isFromFixedAssetInvestment, bool isForgotten, decimal productRate, decimal materialRate, decimal ndsRate)
        {
            var waybill = new PurchasesWaybill
            {
                DocumentBaseId = DefaultDocumentBaseId,
                Date = date,
                Number = DefaultDocumentNumber,
                Sum = sum,
                TaxationSystemType = taxSystem,
                IsCompensated = isCompensated,
                IsFromFixedAssetInvestment = isFromFixedAssetInvestment,
            };
            if (isForgotten)
            {
                waybill.ForgottenDocumentNumber = ForgottenDocumentNumber;
                waybill.ForgottenDocumentDate = ForgottenDocumentDate;
            }
            var waybillItems = new List<WaybillItem>();
            if (productRate > 0)
            {
                model.StockProducts = new[]
                {
                    new StockProduct
                    {
                        Id = ProductId,
                        Type = StockProductTypeEnum.Product
                    }
                };
                waybillItems.Add(new WaybillItem
                {
                    StockProductId = ProductId,
                    SumWithNds = sum * productRate,
                    SumWithoutNds = sum * productRate / (1 + ndsRate)
                });
            }
            if (materialRate > 0)
            {
                model.StockProducts = new[]
                {
                    new StockProduct
                    {
                        Id = MaterialId,
                        Type = StockProductTypeEnum.Material
                    }
                };
                waybillItems.Add(new WaybillItem
                {
                    StockProductId = MaterialId,
                    SumWithNds = sum * materialRate,
                    SumWithoutNds = sum * materialRate / (1 + ndsRate),
                });
            }
            waybill.Items = waybillItems;
            model.Waybills = new[] { waybill };
        }

        private static void CreateStatement(PaymentToSupplierUsnPostingsBusinessModel model, Enums.TaxationSystemType taxSystem, DateTime date, decimal sum, bool isCompensated, bool isFromFixedAssetInvestment, decimal ndsRate)
        {
            model.Statements = new[]
            {
                new PurchasesStatement
                {
                    DocumentBaseId = DefaultDocumentBaseId,
                    Date = date,
                    Number = DefaultDocumentNumber,
                    Sum = sum,
                    TaxationSystemType = taxSystem,
                    IsCompensated = isCompensated,
                    IsFromFixedAssetInvestment = isFromFixedAssetInvestment,
                    Items = new []
                    {
                        new PurchasesStatementItem
                        {
                            SumWithNds = sum,
                            SumWithoutNds = sum / (1 + ndsRate)
                        }
                    }
                }
            };
        }

        private static void CreateUpd(PaymentToSupplierUsnPostingsBusinessModel model, Enums.TaxationSystemType taxSystem, DateTime date, decimal sum, bool isForgotten, decimal productRate, decimal materialRate, decimal serviceRate, decimal ndsRate)
        {
            var upd = new PurchasesUpd
            {
                DocumentBaseId = DefaultDocumentBaseId,
                Date = date,
                Number = DefaultDocumentNumber,
                Sum = sum,
                TaxationSystemType = taxSystem
            };
            if (isForgotten)
            {
                upd.ForgottenDocumentNumber = ForgottenDocumentNumber;
                upd.ForgottenDocumentDate = ForgottenDocumentDate;
            }
            var updItems = new List<UpdItem>();
            if (productRate > 0)
            {
                model.StockProducts = new[]
                {
                    new StockProduct
                    {
                        Id = ProductId,
                        Type = StockProductTypeEnum.Product
                    }
                };
                updItems.Add(new UpdItem
                {
                    Type = ItemType.Goods,
                    StockProductId = ProductId,
                    SumWithNds = sum * productRate,
                    SumWithoutNds = sum * productRate / (1 + ndsRate)
                });
            }
            if (materialRate > 0)
            {
                model.StockProducts = new[]
                {
                    new StockProduct
                    {
                        Id = MaterialId,
                        Type = StockProductTypeEnum.Material
                    }
                };
                updItems.Add(new UpdItem
                {
                    Type = ItemType.Goods,
                    StockProductId = MaterialId,
                    SumWithNds = sum * materialRate,
                    SumWithoutNds = sum * materialRate / (1 + ndsRate),
                });
            }
            if (serviceRate > 0)
            {
                updItems.Add(new UpdItem
                {
                    Type = ItemType.Service,
                    SumWithNds = sum * serviceRate,
                    SumWithoutNds = sum * serviceRate / (1 + ndsRate)
                });
            }
            upd.Items = updItems.ToArray();
            model.Upds = new[] { upd };
        }

        private static void CreateInventoryCard(PaymentToSupplierUsnPostingsBusinessModel model, Enums.TaxationSystemType taxSystem, DateTime date, decimal sum)
        {
            model.InventoryCards = new[]
            {
                new InventoryCard
                {
                    DocumentBaseId = DefaultDocumentBaseId,
                    Date = date,
                    Number = DefaultDocumentNumber,
                    Cost = sum,
                    TaxationSystemType = taxSystem
                }
            };
        }
    }
}
