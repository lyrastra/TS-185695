using Moedelo.Money.Api.Models.PaymentOrders;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Api.Mappers
{
    public static class KontragentMapper
    {
        public static ContractorResponseDto MapToDto(KontragentWithRequisites kontragent)
        {
            if (kontragent == null)
            {
                return null;
            }
            return new ContractorResponseDto
            {
                Id = kontragent.Id,
                Name = kontragent.Name,
                Form = kontragent.Form,
                Inn = kontragent.Inn,
                Kpp = kontragent.Kpp,
                SettlementAccount = kontragent.SettlementAccount,
                BankName = kontragent.BankName,
                BankCorrespondentAccount = kontragent.BankCorrespondentAccount,
                BankBik = kontragent.BankBik
            };
        }

        public static ContractorResponseDto MapToDto(ContractorWithRequisites contractor)
        {
            if (contractor == null)
            {
                return null;
            }
            return new ContractorResponseDto
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Form = contractor.Form,
                Inn = contractor.Inn,
                Kpp = contractor.Kpp,
                SettlementAccount = contractor.SettlementAccount,
                BankName = contractor.BankName,
                BankCorrespondentAccount = contractor.BankCorrespondentAccount,
                BankBik = contractor.BankBik
            };
        }

        public static KontragentWithRequisites MapToKontragent(ContractorSaveDto contractor)
        {
            return new KontragentWithRequisites
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Inn = contractor.Inn,
                Kpp = contractor.Kpp,
                SettlementAccount = contractor.SettlementAccount,
                BankName = contractor.BankName,
                BankCorrespondentAccount = contractor.BankCorrespondentAccount,
                BankBik = contractor.BankBik
            };
        }

        public static KontragentWithRequisites MapToKontragent(ConfirmContractorDto contractor)
        {
            return new KontragentWithRequisites
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Inn = contractor.Inn,
                Kpp = contractor.Kpp,
                SettlementAccount = contractor.SettlementAccount,
                BankName = contractor.BankName,
                BankCorrespondentAccount = contractor.BankCorrespondentAccount,
                BankBik = contractor.BankBik
            };
        }

        public static ContractorWithRequisites MapToContractor(ConfirmContractorDto contractor)
        {
            return new ContractorWithRequisites
            {
                Id = contractor.Id,
                Type = contractor.ContractorType,
                Name = contractor.Name,
                Inn = contractor.Inn,
                Kpp = contractor.Kpp,
                SettlementAccount = contractor.SettlementAccount,
                BankName = contractor.BankName,
                BankCorrespondentAccount = contractor.BankCorrespondentAccount,
                BankBik = contractor.BankBik
            };
        }

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

        public static ContractorWithRequisites MapToContractor(OtherContractorSaveDto contractor, ContractorType type)
        {
            return new ContractorWithRequisites
            {
                Id = contractor.Id.GetValueOrDefault(),
                Name = contractor.Name,
                Type = type,
                Inn = contractor.Inn,
                Kpp = contractor.Kpp,
                SettlementAccount = contractor.SettlementAccount,
                BankName = contractor.BankName,
                BankCorrespondentAccount = contractor.BankCorrespondentAccount,
                BankBik = contractor.BankBik
            };
        }

        public static ContractorWithRequisites MapToContractor(Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.ContractorDto contractor)
        {
            return new ContractorWithRequisites
            {
                Id = contractor.Id,
                Type = ContractorType.Kontragent, // Из импорта проставляется тип Контрагент
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
