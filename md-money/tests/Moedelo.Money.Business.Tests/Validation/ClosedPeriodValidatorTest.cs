using FluentAssertions;
using Moedelo.Money.Business.Abstractions.ClosedPeriod;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Tests.Validation
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    internal class ClosedPeriodValidatorTest
    {
        private Mock<IClosedPeriodReader> reader;
        private ClosedPeriodValidator validator;

        [SetUp]
        public void Setup()
        {
            reader = new Mock<IClosedPeriodReader>();
            validator = new ClosedPeriodValidator(reader.Object);
        }

        [Test]
        public void ValidateAsync_ShouldThrowException_IfDateLessThanAccountingWasReleased()
        {
            //arrange
            var date = new DateTime(2012, 01, 01);

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(date);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Нельзя создавать п/п с датой ранее 2013-01-01")
                .Result.And.Name.Should().Be("Date");
        }

        [Test]
        public void ValidateAsync_ShouldThrowException_IfDateInClosedPeriod()
        {
            //arrange
            var date = new DateTime(2019, 03, 01);
            reader.Setup(x => x.GetLastClosedPeriodDateAsync())
                .ReturnsAsync(new DateTime(2019, 09, 10));

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(date);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Дата попадает в закрытый период (до 2019-09-10)")
                .Result.And.Name.Should().Be("Date");
        }

        [Test]
        public void ValidateAsync_ShouldThrowException_IfDateBeforeEntringOfBalances()
        {
            //arrange
            var date = new DateTime(2019, 03, 01);
            reader.Setup(x => x.GetBalancesDateAsync())
                .ReturnsAsync(new DateTime(2019, 09, 1));

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(date);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Дата документа ранее даты ввода остатков (2019-09-01)")
                .Result.And.Name.Should().Be("Date");
        }

        [Test]
        public async Task ValidateAsync_ShouldPass_IfDateIsOutOfClosedPeriod()
        {
            // arrange
            var date = DateTime.Today;
            reader.Setup(x => x.GetLastClosedDateAsync())
                .ReturnsAsync(DateTime.Today.AddDays(-10));

            //act
            await validator.ValidateAsync(date);
        }
    }
}
