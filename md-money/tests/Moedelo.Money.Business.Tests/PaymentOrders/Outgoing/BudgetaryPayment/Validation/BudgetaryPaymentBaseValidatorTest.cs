using FluentAssertions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Tests.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    [TestFixture]
    class BudgetaryPaymentBaseValidatorTest
    {
        private readonly BudgetaryPaymentBaseValidator validator = new BudgetaryPaymentBaseValidator();

        [Test]
        [TestCase(BudgetaryPeriodType.None)]
        [TestCase(BudgetaryPeriodType.NoPeriod)]
        [TestCase(BudgetaryPeriodType.Year)]
        [TestCase(BudgetaryPeriodType.HalfYear)]
        [TestCase(BudgetaryPeriodType.Quarter)]
        [TestCase(BudgetaryPeriodType.Month)]
        public void ValidateAsync_ShouldThrowException_IfPeriodTypeIsNotSatisfyToPaymentBase(BudgetaryPeriodType periodType)
        {
            //arrange
            var paymentBase = BudgetaryPaymentBase.Tr;
            var period = new BudgetaryPeriod
            {
                Type = periodType
            };

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(paymentBase, period);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage($"Тип периода {(int)periodType} не соответствует основанию платежа ТР. Ожидается 9")
                .Result.And.Name.Should().Be("Period.Type");
        }
    }
}
