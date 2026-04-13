using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing.BudgetaryPayment
{
    internal static class BudgetaryRecipientMapper
    {
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