using Moedelo.Money.Domain;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto;

namespace Moedelo.Money.Business.PaymentOrders
{
    internal static class KontragentMapper
    {
        public static KontragentWithRequisites MapKontragentRequisites(KontragentWithRequisitesDto kontragent)
        {
            if (kontragent == null)
            {
                return null;
            }
            return new KontragentWithRequisites
            {
                Id = kontragent.Id,
                Name = kontragent.Name,
                Inn = kontragent.Inn,
                Kpp = kontragent.Kpp,
                SettlementAccount = kontragent.SettlementAccount,
                BankName = kontragent.BankName,
                BankBik = kontragent.BankBik,
                BankCorrespondentAccount = kontragent.BankCorrespondentAccount
            };
        }

        public static ContractorWithRequisites MapContractorRequisites(ContractorWithRequisitesDto contractor)
        {
            if (contractor == null)
            {
                return null;
            }
            return new ContractorWithRequisites
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Type = contractor.Type,
                Inn = contractor.Inn,
                Kpp = contractor.Kpp,
                SettlementAccount = contractor.SettlementAccount,
                BankName = contractor.BankName,
                BankBik = contractor.BankBik,
                BankCorrespondentAccount = contractor.BankCorrespondentAccount
            };
        }

        public static KontragentWithRequisitesDto MapKontragentRequisitesToDto(KontragentWithRequisites kontragent)
        {
            return new KontragentWithRequisitesDto
            {
                Id = kontragent.Id,
                Name = kontragent.Name,
                Inn = kontragent.Inn,
                Kpp = kontragent.Kpp,
                SettlementAccount = kontragent.SettlementAccount,
                BankName = kontragent.BankName,
                BankBik = kontragent.BankBik,
                BankCorrespondentAccount = kontragent.BankCorrespondentAccount
            };
        }

        public static ContractorWithRequisitesDto MapContractorRequisitesToDto(ContractorWithRequisites contractor)
        {
            return new ContractorWithRequisitesDto
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Type = contractor.Type,
                Inn = contractor.Inn,
                Kpp = contractor.Kpp,
                SettlementAccount = contractor.SettlementAccount,
                BankName = contractor.BankName,
                BankBik = contractor.BankBik,
                BankCorrespondentAccount = contractor.BankCorrespondentAccount
            };
        }

        public static Kafka.Abstractions.Models.Contractor MapKontragentWithRequisitesToKafka(KontragentWithRequisites kontragent)
        {
            return new Kafka.Abstractions.Models.Contractor
            {
                Id = kontragent.Id,
                Name = kontragent.Name,
                Type = ContractorType.Kontragent,
                Inn = kontragent.Inn,
                Kpp = kontragent.Kpp,
                SettlementAccount = kontragent.SettlementAccount,
                BankName = kontragent.BankName,
                BankBik = kontragent.BankBik,
                BankCorrespondentAccount = kontragent.BankCorrespondentAccount
            };
        }

        public static Kafka.Abstractions.Models.Contractor MapContractorWithRequisitesToKafka(ContractorWithRequisites contractor)
        {
            return new Kafka.Abstractions.Models.Contractor
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Type = contractor.Type,
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
