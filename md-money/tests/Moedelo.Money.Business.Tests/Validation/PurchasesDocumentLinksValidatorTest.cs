using FluentAssertions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Tests.Validation
{
    [TestFixture]
    class PurchasesDocumentLinksValidatorTest
    {
        private PurchasesDocumentLinksValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new PurchasesDocumentLinksValidator(null, null, null, null);
        }

        [Test]
        public void ValidateAsync_ShouldThrowException_IfContainsDuplicate()
        {
            //arrange
            long[] links = { 279545622, 277599289, 279545622 };

            //act
            Func<Task> validateTask = () => DocumentLinksDuplicateValidator.Validate(links);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Документ с ид 279545622 указан 2 или более раз")
                .Result.And.Name.Should().Be("Documents");
        }
    }
}
