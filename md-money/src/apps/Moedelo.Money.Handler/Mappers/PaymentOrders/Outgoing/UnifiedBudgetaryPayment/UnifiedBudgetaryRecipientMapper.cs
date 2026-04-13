using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    internal static class UnifiedBudgetaryRecipientMapper
    {
        public static UnifiedBudgetaryRecipient MapToDomain(Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Models.UnifiedBudgetaryPaymentRecipient recipient)
        {
            return new UnifiedBudgetaryRecipient
            {
                Name = recipient.Name,
                Inn = recipient.Inn,
                Kpp = recipient.Kpp,
                SettlementAccount = recipient.SettlementAccount,
                BankName = recipient.BankName,
                UnifiedSettlementAccount = recipient.BankCorrespondentAccount,
                BankBik = recipient.BankBik,
                Oktmo = recipient.Oktmo
            };
        }
    }
}