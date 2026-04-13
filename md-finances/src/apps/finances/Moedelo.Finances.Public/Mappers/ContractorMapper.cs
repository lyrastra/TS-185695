using System.Collections.Generic;
using System.Linq;
using Moedelo.Finances.Domain.Models.Money;
using Moedelo.Finances.Public.ClientData.Contractors;

namespace Moedelo.Finances.Public.Mappers
{
    public static class ContractorMapper
    {
        public static List<ContractorClientData> Map(IReadOnlyList<Contractor> contractors)
        {
            return contractors.Select(Map).ToList();
        }

        public static ContractorClientData Map(Contractor contractor)
        {
            return new ContractorClientData
            {
                Id = contractor.Id,
                Name = contractor.Name,
                Type = contractor.Type
            };
        }
    }
}