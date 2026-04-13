using Moedelo.Money.Domain;

namespace Moedelo.Money.Handler.Mappers
{
    internal static class KontragentMapper
    {
        public static KontragentWithRequisites MapToKontragent(Kafka.Abstractions.Models.Contractor contractor)
        {
            return new KontragentWithRequisites
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Inn = contractor.Inn,
                Kpp = contractor.Kpp,
                SettlementAccount = contractor.SettlementAccount,
                BankName = contractor.BankName,
                BankBik = contractor.BankBik,
                BankCorrespondentAccount = contractor.BankCorrespondentAccount
            };
        }

        public static ContractorWithRequisites MapToContractor(Kafka.Abstractions.Models.Contractor contractor)
        {
            return new ContractorWithRequisites
            {
                Id = contractor.Id,
                Type = contractor.Type,
                Name = contractor.Name,
                Inn = contractor.Inn,
                Kpp = contractor.Kpp,
                SettlementAccount = contractor.SettlementAccount,
                BankName = contractor.BankName,
                BankBik = contractor.BankBik,
                BankCorrespondentAccount = contractor.BankCorrespondentAccount
            };
        }
    }
}
