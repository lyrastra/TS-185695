using FluentAssertions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Tests.Validation
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class BudgetaryPeriodValidatorTest
    {
        private readonly BudgetaryPeriodValidator validator = new();

        [Test]
        public void ValidateAsync_ShouldThrowException_IfPeriodTypeIsInvalid()
        {
            //arrange
            var period = new BudgetaryPeriod
            {
                Type = BudgetaryPeriodType.None
            };

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(period);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Значение 0 не поддерживается")
                .Result.And.Name.Should().Be("Period.Type");
        }

        [Test]
        [TestCase(BudgetaryPeriodType.Month)]
        [TestCase(BudgetaryPeriodType.Quarter)]
        [TestCase(BudgetaryPeriodType.HalfYear)]
        [TestCase(BudgetaryPeriodType.Year)]
        public void ValidateAsync_ShouldThrowException_IfPeriodYearBefore2011(BudgetaryPeriodType periodType)
        {
            //arrange
            var period = new BudgetaryPeriod
            {
                Type = periodType,
                Year = 2009
            };

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(period);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Не поддерживается период ранее 2010 года")
                .Result.And.Name.Should().Be("Period.Year");
        }

        [Test]
        public void ValidateAsync_ShouldThrowException_IfPeriodDateBefore2011()
        {
            //arrange
            var period = new BudgetaryPeriod
            {
                Type = BudgetaryPeriodType.Date,
                Date = new DateTime(2009, 01, 01)
            };

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(period);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Не поддерживается дата ранее 2010 года")
                .Result.And.Name.Should().Be("Period.Date");
        }

        [Test]
        [TestCase(0)]
        [TestCase(3)]
        public void ValidateAsync_ShouldThrowException_IfPeriodHalfYearHasInvalidValue(int halfYear)
        {
            //arrange
            var period = new BudgetaryPeriod
            {
                Type = BudgetaryPeriodType.HalfYear,
                Number = halfYear,
                Year = 2019
            };

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(period);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Номер полугодия должен быть равен 1 или 2")
                .Result.And.Name.Should().Be("Period.HalfYear");
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        public void ValidateAsync_ShouldThrowException_IfPeriodQuarterHasInvalidValue(int quarter)
        {
            //arrange
            var period = new BudgetaryPeriod
            {
                Type = BudgetaryPeriodType.Quarter,
                Number = quarter,
                Year = 2019
            };

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(period);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Номер квартала должен быть в промежутке между 1 и 4")
                .Result.And.Name.Should().Be("Period.Quarter");
        }

        [Test]
        [TestCase(0)]
        [TestCase(13)]
        public void ValidateAsync_ShouldThrowException_IfPeriodMonthHasInvalidValue(int month)
        {
            //arrange
            var period = new BudgetaryPeriod
            {
                Type = BudgetaryPeriodType.Month,
                Number = month,
                Year = 2019
            };

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(period);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Номер месяца должен быть в промежутке между 1 и 12")
                .Result.And.Name.Should().Be("Period.Month");
        }

        [Test]
        public async Task ValidateAsync_ShouldPass_WhenNoPeriod()
        {
            //arrange
            var period = new BudgetaryPeriod
            {
                Type = BudgetaryPeriodType.NoPeriod
            };

            //act
            await validator.ValidateAsync(period);
        }

        [Test]
        public async Task ValidateAsync_ShouldPass_WhenYearOver2011()
        {
            //arrange
            var period = new BudgetaryPeriod
            {
                Type = BudgetaryPeriodType.Year,
                Year = 2019
            };

            //act
            await validator.ValidateAsync(period);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task ValidateAsync_ShouldPass_WhenHalfYearIsValid(int halfYear)
        {
            //arrange
            var period = new BudgetaryPeriod
            {
                Type = BudgetaryPeriodType.HalfYear,
                Number = halfYear,
                Year = 2019
            };

            //act
            await validator.ValidateAsync(period);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public async Task ValidateAsync_ShouldPass_WhenQuarterIsValid(int quarter)
        {
            //arrange
            var period = new BudgetaryPeriod
            {
                Type = BudgetaryPeriodType.Quarter,
                Number = quarter,
                Year = 2019
            };

            //act
            await validator.ValidateAsync(period);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public async Task ValidateAsync_ShouldPass_WhenMonthIsValid(int month)
        {
            //arrange
            var period = new BudgetaryPeriod
            {
                Type = BudgetaryPeriodType.Month,
                Number = month,
                Year = 2019
            };

            //act
            await validator.ValidateAsync(period);
        }
    }
}
