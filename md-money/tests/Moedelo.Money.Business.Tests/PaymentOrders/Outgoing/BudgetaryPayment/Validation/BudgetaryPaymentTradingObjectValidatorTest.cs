using FluentAssertions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation;
using Moedelo.Money.Business.TradingObjects;
using Moedelo.Money.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Tests.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    [TestFixture]
    class BudgetaryPaymentTradingObjectValidatorTest
    {
        private Mock<ITradingObjectReader> reader;
        private BudgetaryPaymentTradingObjectValidator validator;

        [SetUp]
        public void Setup()
        {
            reader = new Mock<ITradingObjectReader>();
            validator = new BudgetaryPaymentTradingObjectValidator(
                reader.Object);
        }

        [Test]
        public void ValidateAsync_ShouldThrowException_IfTradingObjectNotFound()
        {
            //arrange
            reader.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(TradingObject));

            var accountCode = BudgetaryAccountCodes.TradingFees;
            var tradingObjectId = 1234;

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(accountCode, tradingObjectId);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Не найден торговый объект с идентификатором 1234")
                .Result.And.Name.Should().Be("TradingObjectId");
        }

        [Test]
        public async Task ValidateAsync_ShouldPass_IfTradingObjectExistsAndAccountCodeNotTradingFees()
        {
            //arrange
            reader.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(TradingObject));

            var accountCode = BudgetaryAccountCodes.Ndfl;
            var tradingObjectId = 1234;

            //act
            await validator.ValidateAsync(accountCode, tradingObjectId);
        }

        [Test]
        public async Task ValidateAsync_ShouldPass_IfTradingObjectExists()
        {
            //arrange
            reader.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new TradingObject
                {
                    Id = 1234,
                    Number = 100500
                });

            var accountCode = BudgetaryAccountCodes.TradingFees;
            var tradingObjectId = 1234;

            //act
            await validator.ValidateAsync(accountCode, tradingObjectId);
        }
    }
}
