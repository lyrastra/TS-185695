using FluentAssertions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.SettlementAccounts;
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
    public class SettlementAccountsValidatorTest
    {
        private Mock<ISettlementAccountsReader> reader;
        private SettlementAccountsValidator validator;

        [SetUp]
        public void Setup()
        {
            reader = new Mock<ISettlementAccountsReader>();
            validator = new SettlementAccountsValidator(reader.Object);
        }

        [Test]
        public void ValidateAsync_ShouldThrowException_IfSettlementAccountIsNotFound()
        {
            //arrange
            var settlementAccountId = 1234;
            var date = DateTime.Today.AddDays(-10);
            reader.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(SettlementAccount));

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(settlementAccountId);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Не найден рассчетный счет с идентификатором 1234")
                .Result.And.Name.Should().Be("SettlementAccountId");
        }

        [Test]
        public void ValidateAsync_ShouldThrowException_IfSettlementAccountSubcontoIsNull()
        {
            //arrange
            var settlementAccountId = 1234;
            var date = DateTime.Today.AddDays(-10);
            reader.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new SettlementAccount());

            //act
            Func<Task> validateTask = () => validator.ValidateAsync(settlementAccountId);

            //assert
            validateTask.Should().ThrowExactlyAsync<BusinessValidationException>()
                .WithMessage("Отсутствует субконто рассчетного счета с идентификатором 1234")
                .Result.And.Name.Should().Be("SettlementAccountId");
        }

        [Test]
        public async Task ValidateAsync_ShouldPass()
        {
            //arrange
            var settlementAccountId = 1234;
            var date = DateTime.Today.AddDays(-10);
            reader.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new SettlementAccount
                {
                    Id = settlementAccountId,
                    SubcontoId = 100500
                });

            //act
            await validator.ValidateAsync(settlementAccountId);
        }
    }
}
