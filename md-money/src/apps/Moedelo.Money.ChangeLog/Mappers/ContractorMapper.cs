using Moedelo.Changelog.Shared.Domain.Definitions.Money.Common;
using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.ChangeLog.Mappers
{
    static class ContractorMapper
    {
        public static ContractorInfo MapToDefinitionState(this Contractor contractor)
        {
            return new()
            {
                Name = contractor.Name,
                Inn = contractor.Inn,
                Kpp = contractor.Kpp,
                SettlementAccount = contractor.SettlementAccount,
                BankCorrespondentAccount = contractor.BankCorrespondentAccount,
                BankBik = contractor.BankBik,
                BankName = contractor.BankName
            };
        }
    }
}
