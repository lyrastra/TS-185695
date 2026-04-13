using System;
using System.Linq;
using FluentAssertions;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models;
using Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.TaxPostings;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using Moedelo.Money.Providing.Business.TaxPostings.Models;
using Moedelo.Requisites.Enums.TaxationSystems;
using Moedelo.TaxPostings.Enums;
using NUnit.Framework;
using TaxPostingStatus = Moedelo.TaxPostings.Enums.TaxPostingStatus;

namespace Moedelo.Money.Providing.Business.Tests.PaymentOrders.Incoming.PaymentFromCustomer
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.SingleInstance)] // можно выполнять на одном экземпляре
    public class PaymentFromCustomerUsnGeneratorTests
    {
        private const long DefaultOperationBaseId = 12;
        private const string DefaultOperationNumber = "12/2";
        private static readonly DateTime DefaultOperationDate = new DateTime(2018, 12, 13);

        private const long DefaultDocumentBaseId = 21;
        private const string DefaultDocumentNumber = "243";
        private static readonly DateTime DefaultDocumentDate = new DateTime(2018, 12, 14);

        [Test]
        [TestCase(TaxationSystemType.Usn, TaxPostingStatus.No)]
        [TestCase(TaxationSystemType.UsnAndEnvd, TaxPostingStatus.No)]
        [TestCase(TaxationSystemType.Osno, TaxPostingStatus.NotTax)]
        [TestCase(TaxationSystemType.OsnoAndEnvd, TaxPostingStatus.NotTax)]
        [TestCase(TaxationSystemType.Envd, TaxPostingStatus.NotTax)]
        [TestCase(TaxationSystemType.Patent, TaxPostingStatus.No)]
        public void Generate_ShouldReturnNoPostings_IfTaxSystemIs(TaxationSystemType taxSystem, TaxPostingStatus status)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                KontragentName = "Иванов"
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(status);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnNoPostings_IfMediationSumIsNotPresent(TaxationSystemType taxSystem)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                KontragentName = "Иванов",
                Sum = 1000m,
                IsMediation = true
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnPosting_IfMediationSumIsPresent(TaxationSystemType taxSystem)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 1000m,
                IsMediation = true,
                MediationCommissionSum = 20m
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 20m,
                        Direction = TaxPostingDirection.Incoming,
                        Description = "Удержано вознаграждение посредника",
                        DocumentId = DefaultOperationBaseId,
                        DocumentNumber = DefaultOperationNumber,
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnPosting_IfSumIsPresent(TaxationSystemType taxSystem)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 100m,
                KontragentName = "Иванов"
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 100m,
                        Direction = TaxPostingDirection.Incoming,
                        Description = $"Оплата от контрагента Иванов.",
                        DocumentId = DefaultOperationBaseId,
                        DocumentNumber = DefaultOperationNumber,
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Statement, "акту")]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill, "накладной")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Statement, "акту")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill, "накладной")]
        public void Generate_ShouldReturnPostings_IfSumIsPresentAndExistDocument(TaxationSystemType taxSystem, LinkedDocumentType documentType, string documentName)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 1000m,
                KontragentName = "Иванов",
            };
            CreateDocument(model, documentType, taxSystem, 500m);

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 500m,
                        Direction = TaxPostingDirection.Incoming,
                        DocumentId = DefaultOperationBaseId,
                        DocumentNumber = DefaultOperationNumber,
                        Description = $"Оплата по {documentName} №{DefaultDocumentNumber} от {DefaultDocumentDate:dd.MM.yyy} от контрагента Иванов.",
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                    },
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 500m,
                        Direction = TaxPostingDirection.Incoming,
                        Description = $"Оплата от контрагента Иванов.",
                        DocumentId = DefaultOperationBaseId,
                        DocumentNumber = DefaultOperationNumber,
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Statement, "акту")]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill, "накладной")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Statement, "акту")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill, "накладной")]
        public void Generate_ShouldReturnPosting_IfSumIsPresentAndExistDocumentThatCoveredOperationSum(TaxationSystemType taxSystem, LinkedDocumentType documentType, string documentName)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 1000m,
                KontragentName = "Иванов"
            };
            CreateDocument(model, documentType, taxSystem, 1000m);


            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.ByLinkedDocument)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 1000m,
                        Direction = TaxPostingDirection.Incoming,
                        DocumentId = DefaultOperationBaseId,
                        DocumentNumber = DefaultOperationNumber,
                        Description = $"Оплата по {documentName} №{DefaultDocumentNumber} от {DefaultDocumentDate:dd.MM.yyy} от контрагента Иванов.",
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnPosting_IfSumIsPresentAndExistNotTaxableDocument(TaxationSystemType taxSystem)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 2000m,
                KontragentName = "Иванов",
                DocumentLinks = new[]
                {
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId,
                        Type = LinkedDocumentType.Waybill
                    }
                },
                Waybills = new[]
                {
                    new SalesWaybill
                    {
                        DocumentBaseId = DefaultDocumentBaseId,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 1000m,
                        TaxationSystemType = Enums.TaxationSystemType.Envd
                    }
                }
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 1000m,
                        Direction = TaxPostingDirection.Incoming,
                        Description = $"Оплата от контрагента Иванов.",
                        DocumentId = DefaultOperationBaseId,
                        DocumentNumber = DefaultOperationNumber,
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnPosting_IfSumIsPresentAndExistNotTaxableDocumentThatCoveredOperationSum(TaxationSystemType taxSystem)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 1000m,
                KontragentName = "Иванов",
                DocumentLinks = new[]
                {
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId,
                        Type = LinkedDocumentType.Statement
                    }
                },
                Statements = new[]
                {
                    new SalesStatement
                    {
                        DocumentBaseId = DefaultDocumentBaseId,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 1000m,
                        TaxationSystemType = Enums.TaxationSystemType.Envd
                    }
                }
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.NotTax)
            {
                TaxationSystemType = taxSystem,
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnNoPosting_IfNdsSumGreaterThanOperationSum(TaxationSystemType taxSystem)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 100m,
                KontragentName = "Иванов",
                IncludeNds = true,
                NdsSum = 200m
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnPosting_IfNdsIsIncluded(TaxationSystemType taxSystem)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 1000m,
                KontragentName = "Иванов",
                IncludeNds = true,
                NdsSum = 152.54m
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 847.46m,
                        Direction = TaxPostingDirection.Incoming,
                        Description = $"Оплата от контрагента Иванов.",
                        DocumentId = DefaultOperationBaseId,
                        DocumentNumber = DefaultOperationNumber,
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnNoPosting_IfMediationNdsSumGreaterThanComissionSum(TaxationSystemType taxSystem)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 400,
                IsMediation = true,
                MediationCommissionSum = 100m,
                KontragentName = "Иванов",
                MediationNdsSum = 200m
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.NotTax);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn)]
        [TestCase(TaxationSystemType.UsnAndEnvd)]
        public void Generate_ShouldReturnPosting_IfMediationNdsIsPresent(TaxationSystemType taxSystem)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 1000m,
                IsMediation = true,
                MediationCommissionSum = 200,
                KontragentName = "Иванов",
                MediationNdsSum = 15.54m
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 184.46m,
                        Direction = TaxPostingDirection.Incoming,
                        Description = $"Удержано вознаграждение посредника",
                        DocumentId = DefaultOperationBaseId,
                        DocumentNumber = DefaultOperationNumber,
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Statement, "акту")]
        [TestCase(TaxationSystemType.Usn, LinkedDocumentType.Waybill, "накладной")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Statement, "акту")]
        [TestCase(TaxationSystemType.UsnAndEnvd, LinkedDocumentType.Waybill, "накладной")]
        public void Generate_ShouldReturnPostings_IfNdsIsIncludedAndDocumentIsPresent(TaxationSystemType taxSystem, LinkedDocumentType documentType, string documentName)
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = taxSystem,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 1000m,
                KontragentName = "Иванов",
                IncludeNds = true,
                NdsSum = 152.54m,
            };
            CreateDocument(model, documentType, taxSystem, 500m);

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new UsnTaxPostingsResponse(TaxPostingStatus.Yes)
            {
                TaxationSystemType = taxSystem,
                Postings = new[]
                {
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 423.73m,
                        Direction = TaxPostingDirection.Incoming,
                        DocumentId = DefaultOperationBaseId,
                        DocumentNumber = DefaultOperationNumber,
                        Description = $"Оплата по {documentName} №{DefaultDocumentNumber} от {DefaultDocumentDate:dd.MM.yyy} от контрагента Иванов.",
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId, DefaultDocumentBaseId }
                    },
                    new UsnTaxPosting
                    {
                        Date = DefaultOperationDate,
                        Sum = 423.73m,
                        Direction = TaxPostingDirection.Incoming,
                        Description = $"Оплата от контрагента Иванов.",
                        DocumentId = DefaultOperationBaseId,
                        DocumentNumber = DefaultOperationNumber,
                        RelatedDocumentBaseIds = new[] { DefaultOperationBaseId }
                    }
                }
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Generate_ShouldReturnPostingsWithoutPrecisionLoss()
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = TaxationSystemType.Usn,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 500m,
                KontragentName = "Иванов",
                IncludeNds = true,
                NdsSum = 83.33m,
                DocumentLinks = new[]
                {
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Type = LinkedDocumentType.Statement,
                        LinkSum = 100m
                    },
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Type = LinkedDocumentType.Statement,
                        LinkSum = 100m
                    },
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId,
                        Type = LinkedDocumentType.Waybill,
                        LinkSum = 100m
                    }
                },
                Statements = new[]
                {
                    new SalesStatement
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 100m,
                        TaxationSystemType = Enums.TaxationSystemType.Usn
                    },
                    new SalesStatement
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 100m,
                        TaxationSystemType = Enums.TaxationSystemType.Usn
                    }
                },
                Waybills = new[]
                {
                    new SalesWaybill
                    {
                        DocumentBaseId = DefaultDocumentBaseId,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 100m,
                        TaxationSystemType = Enums.TaxationSystemType.Usn
                    }
                }
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);

            actual.TaxStatus.Should().Be(TaxPostingStatus.Yes);
            actual.Postings.Count.Should().Be(4);
            actual.Postings.ElementAt(0).Sum.Should().Be(83.33m);
            actual.Postings.ElementAt(1).Sum.Should().Be(83.33m);
            actual.Postings.ElementAt(2).Sum.Should().Be(83.33m);
            actual.Postings.ElementAt(3).Sum.Should().Be(166.68m);
        }

        [Test]
        public void Generate_ShouldReturnPostingsWithoutNegativeSum()
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = TaxationSystemType.Usn,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 2000m,
                KontragentName = "Иванов",
                IncludeNds = true,
                NdsSum = 333.33m,
                DocumentLinks = new[]
                {
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId,
                        Type = LinkedDocumentType.Statement,
                        LinkSum = 1829m
                    }
                },
                Statements = new[]
                {
                    new SalesStatement
                    {
                        DocumentBaseId = DefaultDocumentBaseId,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 1829m
                    }
                }
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);

            actual.TaxStatus.Should().Be(TaxPostingStatus.Yes);
            actual.Postings.Count.Should().Be(2);
            actual.Postings.ElementAt(0).Sum.Should().Be(1524.17m);
            actual.Postings.ElementAt(1).Sum.Should().Be(142.50m);
        }

        [Test]
        public void Generate_ShouldReturnNoTax_IfNdsIsIncludedAndDocumentIsPresentOnEnvd()
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = TaxationSystemType.Usn,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 400m,
                KontragentName = "Иванов",
                IncludeNds = true,
                NdsSum = 66.67m,
                DocumentLinks = new[]
                {
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Type = LinkedDocumentType.Waybill
                    },
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Type = LinkedDocumentType.Statement
                    }
                },
                Waybills = new[]
                {
                    new SalesWaybill
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 274.75m,
                        TaxationSystemType = Enums.TaxationSystemType.Envd
                    }
                },
                Statements = new[]
                {
                    new SalesStatement
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 125.25m,
                        TaxationSystemType = Enums.TaxationSystemType.Envd
                    }
                }
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);

            actual.TaxStatus.Should().Be(TaxPostingStatus.NotTax);
            actual.Postings.Count.Should().Be(0);
        }

        [Test]
        public void Generate_ShouldReturnNoTax_IfNdsIsIncludedAndDocumentIsPresentOnEnvdAndDocumentSumLessThanNdsSum()
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = TaxationSystemType.Usn,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 500m,
                KontragentName = "Иванов",
                IncludeNds = true,
                NdsSum = 83.33m,
                DocumentLinks = new[]
                {
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Type = LinkedDocumentType.Statement
                    },
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Type = LinkedDocumentType.Waybill
                    }
                },
                Waybills = new[]
                {
                    new SalesWaybill
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 365.66m,
                        TaxationSystemType = Enums.TaxationSystemType.Envd
                    }
                },
                Statements = new[]
                {
                    new SalesStatement
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 125.25m,
                        TaxationSystemType = Enums.TaxationSystemType.Envd
                    }
                }
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);

            actual.TaxStatus.Should().Be(TaxPostingStatus.NotTax);
            actual.Postings.Count.Should().Be(0);
        }

        [Test]
        public void Generate_ShouldReturnPosting_IfNdsIsIncludedAndDocumentIsPresentOnUsnAndEnvd()
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = TaxationSystemType.Usn,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 200m,
                KontragentName = "Иванов",
                IncludeNds = true,
                NdsSum = 33.33m,
                DocumentLinks = new[]
                {
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Type = LinkedDocumentType.Statement
                    },
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Type = LinkedDocumentType.Statement
                    }
                },
                Statements = new[]
                {
                    new SalesStatement
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 100m,
                        TaxationSystemType = Enums.TaxationSystemType.Envd
                    },
                    new SalesStatement
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 100m,
                        TaxationSystemType = Enums.TaxationSystemType.Usn
                    }
                }
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);

            actual.TaxStatus.Should().Be(TaxPostingStatus.ByLinkedDocument);
            actual.Postings.Count.Should().Be(1);
            actual.Postings.First().Sum.Should().Be(66.67m);
        }

        [Test]
        public void Generate_ShouldReturnLinkedDocumentsPosting_IfSumIsEqualToDocumentsSum()
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = TaxationSystemType.Usn,
                DocumentBaseId = DefaultOperationBaseId,
                Date = DefaultOperationDate,
                Number = DefaultOperationNumber,
                Sum = 200m,
                KontragentName = "Иванов",
                IncludeNds = true,
                NdsSum = 33.33m,
                DocumentLinks = new[]
                {
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Type = LinkedDocumentType.Statement,
                        LinkSum = 100m
                    },
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Type = LinkedDocumentType.SalesUpd,
                        LinkSum = 100m
                    }
                },
                Statements = new[]
                {
                    new SalesStatement
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 100m
                    }
                },
                Upds = new[]
                {
                    new SalesUpd
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Date = DefaultDocumentDate,
                        Number = DefaultDocumentNumber,
                        Sum = 100m
                    }
                }
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);

            actual.TaxStatus.Should().Be(TaxPostingStatus.ByLinkedDocument);
            actual.Postings.Count.Should().Be(2);
            actual.Postings.ElementAt(0).Sum.Should().Be(83.33m);
            actual.Postings.ElementAt(1).Sum.Should().Be(83.34m);
        }
        
        [Test(Description = "Для забытого документа в описание проводки попадают номер и дата забытого документа")]
        public void Generate_ShouldReturnLinkedDocumentsPosting_IfForgottenDocument()
        {
            var model = new PaymentFromCustomerPostingsBusinessModel
            {
                TaxationSystem = TaxationSystemType.Usn,
                DocumentBaseId = DefaultOperationBaseId,
                Date = new DateTime(2018, 12, 25),
                Number = "1-p",
                Sum = 200m,
                KontragentName = "Иванов",
                IncludeNds = true,
                NdsSum = 33.33m,
                DocumentLinks = new[]
                {
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Type = LinkedDocumentType.Waybill,
                        LinkSum = 150m
                    },
                    new DocumentLink
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Type = LinkedDocumentType.SalesUpd,
                        LinkSum = 50m
                    }
                },
                Waybills = new []
                {
                    new SalesWaybill
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 1,
                        Date = new DateTime(2019, 06, 15),
                        Number = "1-w",
                        Sum = 150,
                        ForgottenDocumentDate = new DateTime(2018, 12, 18), 
                        ForgottenDocumentNumber = "1-f"
                    }
                },
                Upds = new[]
                {
                    new SalesUpd
                    {
                        DocumentBaseId = DefaultDocumentBaseId + 2,
                        Date = new DateTime(2019, 06, 16),
                        Number = "1-u",
                        Sum = 50m,
                        ForgottenDocumentDate = new DateTime(2018, 12, 26),
                        ForgottenDocumentNumber = "2-f"
                    }
                }
            };

            var actual = PaymentFromCustomerUsnPostingsGenerator.Generate(model);
            var expected = new[]
            {
                new UsnTaxPosting
                {
                    Date = new DateTime(2018, 12, 25),
                    DocumentNumber = "1-p",
                    Description = "Оплата по накладной №1-f от 18.12.2018 от контрагента Иванов."
                },
                new UsnTaxPosting
                {
                    Date = new DateTime(2018, 12, 25),
                    DocumentNumber = "1-p",
                    Description = "Оплата по УПД №2-f от 26.12.2018 от контрагента Иванов."
                }
            };

            actual.TaxStatus.Should().Be(TaxPostingStatus.ByLinkedDocument);
            actual.Postings.Should().BeEquivalentTo(expected, opt =>
                opt.Including(x => x.Date)
                    .Including(x => x.DocumentNumber)
                    .Including(x => x.Description)
            );
        }

        private static void CreateDocument(PaymentFromCustomerPostingsBusinessModel model, LinkedDocumentType documentType, TaxationSystemType taxSystem, decimal sum)
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
                    model.Waybills = new[]
                    {
                        new SalesWaybill
                        {
                            DocumentBaseId = DefaultDocumentBaseId,
                            Date = DefaultDocumentDate,
                            Number = DefaultDocumentNumber,
                            Sum = sum,
                            TaxationSystemType = (Enums.TaxationSystemType?) taxSystem
                        }
                    };
                    break;
                case LinkedDocumentType.Statement:
                    model.Statements = new[]
                    {
                        new SalesStatement
                        {
                            DocumentBaseId = DefaultDocumentBaseId,
                            Date = DefaultDocumentDate,
                            Number = DefaultDocumentNumber,
                            Sum = sum,
                            TaxationSystemType = (Enums.TaxationSystemType?) taxSystem
                        }
                    };
                    break;
                case LinkedDocumentType.SalesUpd:
                    model.Upds = new[]
                    {
                        new SalesUpd
                        {
                            DocumentBaseId = DefaultDocumentBaseId,
                            Date = DefaultDocumentDate,
                            Number = DefaultDocumentNumber,
                            Sum = sum,
                            TaxationSystemType = (Enums.TaxationSystemType?) taxSystem
                        }
                    };
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
