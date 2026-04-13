using Moedelo.Money.PaymentOrders.Dto;
using Moedelo.Money.PaymentOrders.Domain.Models.Snapshot;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.PaymentOrders.Api.Mappers
{
    internal static class KontragentRequisitesMapper
    {
        public static KontragentWithRequisitesDto MapToKontragent(int? id, OrderDetails requisites)
        {
            if (id == null)
            {
                return null;
            }
            return new KontragentWithRequisitesDto
            {
                Id = id.Value,
                Name = requisites?.Name,
                Inn = requisites?.Inn,
                Kpp = requisites?.Kpp,
                SettlementAccount = requisites?.SettlementNumber,
                BankBik = requisites?.BankBik,
                BankName = requisites?.BankName,
                BankCorrespondentAccount = requisites?.BankCorrespondentAccount
            };
        }

        public static ContractorWithRequisitesDto MapToContractor(int? id, ContractorType type, OrderDetails requisites)
        {
            if (id == null && type != ContractorType.Ip)
            {
                return null;
            }
            
            return new ContractorWithRequisitesDto
            {
                Id = type == ContractorType.Ip ? 0 : id.Value,
                Name = requisites?.Name,
                Type = type,
                Inn = requisites?.Inn,
                Kpp = requisites?.Kpp,
                SettlementAccount = requisites?.SettlementNumber,
                BankBik = requisites?.BankBik,
                BankName = requisites?.BankName,
                BankCorrespondentAccount = requisites?.BankCorrespondentAccount
            };
        }

        public static BudgetaryRecipientRequisitesDto MapToBudgetaryRecipient(OrderDetails requisites)
        {
            return new BudgetaryRecipientRequisitesDto
            {
                Name = requisites.Name,
                Inn = requisites.Inn,
                Kpp = requisites.Kpp,
                SettlementAccount = requisites.SettlementNumber,
                BankName = requisites.BankName,
                BankBik = requisites.BankBik,
                BankCorrespondentAccount = requisites.BankCorrespondentAccount,
                Okato = requisites.Okato,
                Oktmo = requisites.Oktmo
            };
        }

        public static KontragentRequisites Map(KontragentWithRequisitesDto requisites)
        {
            return new KontragentRequisites
            {
                Name = requisites.Name,
                Inn = requisites.Inn,
                Kpp = requisites.Kpp,
                SettlementAccount = requisites.SettlementAccount,
                BankName = requisites.BankName,
                BankBik = requisites.BankBik,
                BankCorrespondentAccount = requisites.BankCorrespondentAccount
            };
        }

        public static KontragentRequisites Map(ContractorWithRequisitesDto requisites)
        {
            return new KontragentRequisites
            {
                Name = requisites.Name,
                Inn = requisites.Inn,
                Kpp = requisites.Kpp,
                SettlementAccount = requisites.SettlementAccount,
                BankName = requisites.BankName,
                BankBik = requisites.BankBik,
                BankCorrespondentAccount = requisites.BankCorrespondentAccount
            };
        }

        public static KontragentRequisites Map(BudgetaryRecipientRequisitesDto requisites)
        {
            return new KontragentRequisites
            {
                Name = requisites.Name,
                Inn = requisites.Inn,
                Kpp = requisites.Kpp,
                SettlementAccount = requisites.SettlementAccount,
                BankName = requisites.BankName,
                BankBik = requisites.BankBik,
                BankCorrespondentAccount = requisites.BankCorrespondentAccount,
                Okato = requisites.Okato,
                Oktmo = requisites.Oktmo
            };
        }
    }
}
