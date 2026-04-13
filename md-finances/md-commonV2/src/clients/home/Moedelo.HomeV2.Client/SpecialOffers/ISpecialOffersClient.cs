using Moedelo.Common.Enums.Enums.SpecialOffers;
using Moedelo.HomeV2.Dto.SpecialOffers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.HomeV2.Client.SpecialOffers
{
    public interface ISpecialOffersClient : IDI
    {
        Task<List<SpecialOffersDto>> GetAllSpecialOffers();

        Task<List<SpecialOffersDto>> GetAllSpecialOffersByCritrions(IEnumerable<SpecialOffersFilterCriterions> filterCriterions);

        Task<SpecialOffersDto> GetSpecialOfferById(int id);

        Task<SpecialOffersDto> CreateSpecialOffer(SpecialOffersDto specialOffersDto);

        Task UpdateSpecialOffer(SpecialOffersDto specialOffersDto);

        Task DeleteSpecialOfferById(int id);
    }
}
