using Moedelo.AccPostings.Enums;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.SettlementAccounts;

namespace Moedelo.Money.Providing.Business.AccountingPostings.Extensions
{
    static class SubcontoExtensions
    {
        public static Subconto ToSubconto(this SettlementAccount settlementAccount)
        {
            return new Subconto
            {
                Id = settlementAccount.SubcontoId,
                Name = settlementAccount.Number,
                Type = SubcontoType.SettlementAccount
            };
        }

        public static Subconto ToSubconto(this Kontragent kontergent)
        {
            return new Subconto
            {
                Id = kontergent.SubcontoId,
                Name = kontergent.Name,
                Type = SubcontoType.Kontragent
            };
        }

        public static Subconto ToSubconto(this Contract contract)
        {
            return new Subconto
            {
                Id = contract.SubcontoId,
                Name = contract.Name,
                Type = SubcontoType.Contract
            };
        }
    }
}
