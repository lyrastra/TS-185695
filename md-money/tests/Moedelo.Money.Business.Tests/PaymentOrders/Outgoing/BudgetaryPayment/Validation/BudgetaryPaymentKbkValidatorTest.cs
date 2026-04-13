using FluentAssertions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation;
using Moedelo.Money.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Tests.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    [TestFixture]
    class BudgetaryPaymentKbkValidatorTest
    {
        private Mock<IKbkReader> kbkReader;
        private BudgetaryPaymentKbkValidator validator;

        [SetUp]
        public void Setup()
        {
            kbkReader = new Mock<IKbkReader>();
            validator = new BudgetaryPaymentKbkValidator(
                kbkReader.Object);
        }

        [Test]
        public void ValidateAsync_ShouldThrowException_IfKbkNumberIsNotSatisfyToKbkId()
        {
            //arrange
            kbkReader.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Kbk
                {
                    Id = 181,
                    Number = "18210202090071010160",
                });

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(BudgetaryAccountCodes.Ndfl, 181, "18210203090071010160");

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("КБК 18210203090071010160 не соответствует номеру КБК с идентификатором 181")
                .Result.And.Name.Should().Be("KbkNumber");
        }

        [Test]
        public async Task ValidateAsync_ShouldPass_ForOtherTaxes()
        {
            //act
            await validator.ValidateAsync(BudgetaryAccountCodes.OtherTaxes, null, "123123123123123123123");
        }
    }
}
