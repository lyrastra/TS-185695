using Moedelo.Money.Api.Models.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.Api.Mappers.PaymentOrders.Outgoing.BudgetaryPayment
{
    internal static class BudgetaryRecipientMapper
    {
        public static BudgetaryRecipientResponseDto MapToDto(BudgetaryRecipient recipient)
        {
            return new BudgetaryRecipientResponseDto
            {
                Name = recipient.Name,
                Inn = recipient.Inn,
                Kpp = recipient.Kpp,
                SettlementAccount = recipient.SettlementAccount,
                BankName = recipient.BankName,
                BankCorrespondentAccount = recipient.UnifiedSettlementAccount,
                BankBik = recipient.BankBik,
                Okato = recipient.Okato,
                Oktmo = recipient.Oktmo
            };
        }

        public static BudgetaryRecipient MapToDomain(BudgetaryRecipientSaveDto recipient)
        {
            return new BudgetaryRecipient
            {
                Name = recipient.Name,
                Inn = recipient.Inn,
                Kpp = recipient.Kpp,
                SettlementAccount = recipient.SettlementAccount,
                BankName = recipient.BankName,
                UnifiedSettlementAccount = recipient.BankCorrespondentAccount,
                BankBik = recipient.BankBik,
                Okato = recipient.Okato,
                Oktmo = recipient.Oktmo
            };
        }

        public static BudgetaryRecipient MapToDomain(Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models.BudgetaryRecipient recipient)
        {
            return new BudgetaryRecipient
            {
                Name = recipient.Name,
                Inn = recipient.Inn,
                Kpp = recipient.Kpp,
                SettlementAccount = recipient.SettlementAccount,
                BankName = recipient.BankName,
                UnifiedSettlementAccount = recipient.BankCorrespondentAccount,
                BankBik = recipient.BankBik,
                Okato = recipient.Okato,
                Oktmo = recipient.Oktmo
            };
        }
    }
}