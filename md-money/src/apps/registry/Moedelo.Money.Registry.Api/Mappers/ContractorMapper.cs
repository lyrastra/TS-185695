using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Api.Mappers
{
    static class ContractorMapper
    {
        public static Contractor Map(this Kafka.Abstractions.Models.ContractorBase contractor)
        {
            return new()
            {
                Id = contractor.Id,
                Type = contractor.Type,
                Name = contractor.Name
            };
        }

        public static Contractor Map(this Kafka.Abstractions.Models.Contractor contractor)
        {
            return new()
            {
                Id = contractor.Id,
                Type = contractor.Type,
                Name = contractor.Name
            };
        }
    }
}
