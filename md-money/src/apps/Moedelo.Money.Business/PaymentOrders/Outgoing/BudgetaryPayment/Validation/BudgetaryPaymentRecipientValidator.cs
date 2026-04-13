using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System;
using System.Text.RegularExpressions;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    static class BudgetaryPaymentRecipientValidator
    {
        private static readonly DateTime DateOfStart = new DateTime(2021, 01, 01);
        private static readonly DateTime Deadline = new DateTime(2021, 05, 01);
        private static readonly Regex BikTofk = new Regex(@"^\d{9}$", RegexOptions.Compiled);
        private static readonly Regex TreasurySettlementAccount = new Regex(@"^((03\d{18})|(40204\d{15}))$", RegexOptions.Compiled);
        private static readonly Regex UnifiedTreasurySettlementAccount = new Regex(@"^(40102|00000)\d{15}$", RegexOptions.Compiled);


        public static void Validate(DateTime date, BudgetaryRecipient recipient)
        {
            if (date < DateOfStart)
            {
                return;
            }

            if (date >= DateOfStart && date < Deadline &&
                TreasurySettlementAccount.IsMatch(recipient.SettlementAccount) == false &&
                string.IsNullOrEmpty(recipient.UnifiedSettlementAccount))
            {
                return;
            }

            ValidateUnified(recipient);
        }

        private static void ValidateUnified(BudgetaryRecipient recipient)
        {
            if (string.IsNullOrEmpty(recipient.SettlementAccount))
            {
                throw new BusinessValidationException("Recipient.SettlementAccount", "Не указан казначейский счет");
            }

            if (string.IsNullOrEmpty(recipient.UnifiedSettlementAccount))
            {
                throw new BusinessValidationException("Recipient.BankCorrespondentAccount", "Не указан единый казначейский счет");
            }

            if (string.IsNullOrEmpty(recipient.BankBik))
            {
                throw new BusinessValidationException("Recipient.BankBik", "Не указан БИК");
            }

            if (TreasurySettlementAccount.IsMatch(recipient.SettlementAccount) == false)
            {
                throw new BusinessValidationException("Recipient.SettlementAccount", "Номер счета должен начинаться с цифр 03 или 40204");
            }

            if (UnifiedTreasurySettlementAccount.IsMatch(recipient.UnifiedSettlementAccount) == false)
            {
                throw new BusinessValidationException("Recipient.BankCorrespondentAccount", "Номер счета должен начинаться с цифр 40102 или 00000");
            }

            if (BikTofk.IsMatch(recipient.BankBik) == false)
            {
                throw new BusinessValidationException("Recipient.BankBik", "Неверный формат БИК");
            }
        }
    }
}
