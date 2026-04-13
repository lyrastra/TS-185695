using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Billing;
using Moedelo.HomeV2.Dto.ClientSource;
using Moedelo.HomeV2.Dto.TrialCard;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.HomeV2.Client.TrialCard
{
    public interface ITrialCardClient : IDI
    {
        Task<TrialCardDto> GetTrialCardAsync(GetTrialCardRequestDto requestDto);
        Task<List<ClientSourceDto>> GetAllClientSource();

        Task<List<TrialCardDto>> GenerateTrialCardsWithDifferentNames(int clientSourceId, int quantity,
            TrialCardTypes cardType);

        Task<string> GenerateTrialCardsWithOneName(int clientSourceId, int quantity, TrialCardTypes cardType);

        Task<ClientSourceDto> GetClientSource(GetTrialCardRequestDto request);

        Task<List<TrialCardsCountWithClientSourceDto>> GetTrialCardsCountWithClientSource();

        Task<List<TrialCardsStatisticsDto>> GetTrialCardsStatistics(DateTime startDate, DateTime endDate);

        Task<int> SaveClientSource(ClientSourceDto request);

        Task<bool> AreExistTrialCardsByClientSourceId(int clientSourceId);

        Task DeleteClientSource(int id);
    }
}