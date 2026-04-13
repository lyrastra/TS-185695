using FluentAssertions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using NUnit.Framework;
using System;

namespace Moedelo.Money.Business.Tests.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    [TestFixture]
    class BudgetaryPaymentRecipientValidatorTest
    {
        [Test]
        public void ValidateAsync_ShouldPass_NoValidateBeforeDateOfStart()
        {
            //arrange
            var date = new DateTime(2020, 12, 31);
            var recipient = new BudgetaryRecipient
            {
                BankBik = "012345678",
                SettlementAccount = "03100643000000015500",
                UnifiedSettlementAccount = "40102810045370000047"
            };

            //act
            BudgetaryPaymentRecipientValidator.Validate(date, recipient);
        }

        [Test]
        public void ValidateAsync_ShouldThrowException_IfWrongTreasurySettlementAccountAfterDeadLine()
        {
            //arrange
            var date = new DateTime(2021, 05, 01);
            var recipient = new BudgetaryRecipient
            {
                SettlementAccount = "12345678901234567890"
            };

            //act
            Action validateAction = () => BudgetaryPaymentRecipientValidator.Validate(date, recipient);

            //assert
            validateAction.Should().ThrowExactly<BusinessValidationException>();
        }

        [Test]
        public void ValidateAsync_ShouldThrowException_IfWrongUnifiedTreasurySettlementAccountAfterDeadLine()
        {
            //arrange
            var date = new DateTime(2021, 05, 01);
            var recipient = new BudgetaryRecipient
            {
                SettlementAccount = "03100643000000015500",
                UnifiedSettlementAccount = "12345678901234567890"
            };

            //act
            Action validateAction = () => BudgetaryPaymentRecipientValidator.Validate(date, recipient);

            //assert
            validateAction.Should().ThrowExactly<BusinessValidationException>();
        }

        [Test]
        public void ValidateAsync_ShouldPass_IfStandardSettlementAccountsBeforeDeadline()
        {
            //arrange
            var date = new DateTime(2021, 04, 30);
            var recipient = new BudgetaryRecipient
            {
                BankBik = "042345678",
                SettlementAccount = "40802810123160000183"
            };

            //act
            BudgetaryPaymentRecipientValidator.Validate(date, recipient);
        }

        [Test]
        public void ValidateAsync_ShouldPass_IfCorrectTreasurySettlementAccountsAfterDeadline()
        {
            //arrange
            var date = new DateTime(2021, 05, 01);
            var recipient = new BudgetaryRecipient
            {
                BankBik = "012345678",
                SettlementAccount = "03100643000000015500",
                UnifiedSettlementAccount = "40102810045370000047"
            };

            //act
            BudgetaryPaymentRecipientValidator.Validate(date, recipient);
        }

        [Test]
        public void ValidateAsync_ShouldPass_IfCorrectBajkonurSettlementAccountsAfterDeadline()
        {
            //arrange
            var date = new DateTime(2021, 05, 01);
            var recipient = new BudgetaryRecipient
            {
                BankBik = "040037002",
                SettlementAccount = "40204810800000950001",
                UnifiedSettlementAccount = "00000000000000000000"
            };

            //act
            BudgetaryPaymentRecipientValidator.Validate(date, recipient);
        }

        [Test]
        public void ValidateAsync_ShouldThrow_IfStandardSettlementAndUnifiedTreasurySettlementAccountsIsExists()
        {
            //arrange
            var date = new DateTime(2021, 01, 01);
            var recipient = new BudgetaryRecipient
            {
                BankBik = "012345678",
                SettlementAccount = "40802810123160000183",
                UnifiedSettlementAccount = "40102810045370000047"
            };

            //act
            Action validateAction = () => BudgetaryPaymentRecipientValidator.Validate(date, recipient);

            //assert
            validateAction.Should().ThrowExactly<BusinessValidationException>();
        }
    }
}
