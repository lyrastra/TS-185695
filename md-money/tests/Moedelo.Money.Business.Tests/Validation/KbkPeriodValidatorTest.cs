using FluentAssertions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Business.Validation.Kbks;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Tests.Validation
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    public class KbkPeriodValidatorTest
    {
        private Mock<IKbkReader> kbkReader;
        private KbkValidator validator;

        [SetUp]
        public void Setup()
        {
            kbkReader = new Mock<IKbkReader>();
            validator = new KbkValidator(
                kbkReader.Object);
        }

        [Test]
        [TestCase(BudgetaryPeriodType.Year)]
        [TestCase(BudgetaryPeriodType.HalfYear)]
        [TestCase(BudgetaryPeriodType.Quarter)]
        [TestCase(BudgetaryPeriodType.Month)]
        [TestCase(BudgetaryPeriodType.Date)]
        public void ValidateAsync_ShouldThrowException_IfKbkIsNotSatisfyToDate(BudgetaryPeriodType periodType)
        {
            //arrange
            var date = new DateTime(2016, 09, 05);
            var period = new BudgetaryPeriod
            {
                Type = periodType,
                Number = 1,
                Year = 2019
            };
            var kbk = new Kbk
            {
                Id = 181,
                Number = "18210202090071010160",
                StartDate = new DateTime(2017, 01, 01),
                EndDate = DateTime.MaxValue,
                ActualStartDate = new DateTime(2017, 01, 01)
            };

            //act
            Func<Task> validateTask = () => validator.ValidateKbkPeriodAsync(kbk, date, period, BudgetaryAccountCodes.Ndfl);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("КБК 18210202090071010160 не соответствует дате платежа")
                .Result.And.Name.Should().Be("KbkNumber");
        }

        [Test]
        [TestCase(BudgetaryPeriodType.Year)]
        [TestCase(BudgetaryPeriodType.HalfYear)]
        [TestCase(BudgetaryPeriodType.Quarter)]
        [TestCase(BudgetaryPeriodType.Month)]
        [TestCase(BudgetaryPeriodType.Date)]
        public void ValidateAsync_ShouldThrowException_IfKbkIsNotSatisfyToPeriod(BudgetaryPeriodType periodType)
        {
            //arrange
            var date = new DateTime(2019, 09, 05);
            var period = new BudgetaryPeriod
            {
                Type = periodType,
                Number = 1,
                Year = 2019
            };
            var kbk = new Kbk
            {
                Id = 180,
                Number = "18210202090071000160",
                StartDate = new DateTime(2010, 01, 01),
                EndDate = new DateTime(2016, 12, 31),
                ActualStartDate = new DateTime(2017, 01, 01)
            };

            //act
            Func<Task> validateTask = () => validator.ValidateKbkPeriodAsync(kbk, date, period, BudgetaryAccountCodes.Ndfl);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("КБК 18210202090071000160 не соответствует периоду платежа")
                .Result.And.Name.Should().Be("KbkNumber");
        }

        [Test]
        public async Task ValidateAsync_ShouldPass_IfNoPeriod()
        {
            //arrange
            var date = new DateTime(2019, 09, 05);
            var period = new BudgetaryPeriod
            {
                Type = BudgetaryPeriodType.NoPeriod,
                Number = 1,
                Year = 2019
            };
            var kbk = new Kbk
            {
                Id = 180,
                Number = "18210202090071000160",
                StartDate = new DateTime(2010, 01, 01),
                EndDate = new DateTime(2016, 12, 31),
                ActualStartDate = new DateTime(2017, 01, 01)
            };

            //act
            await validator.ValidateKbkPeriodAsync(kbk, date, period, BudgetaryAccountCodes.Ndfl);
        }
    }
}
