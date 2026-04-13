using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.RptV2.Dto.Turnovers;

namespace Moedelo.RptV2.Client.Turnovers
{
    public interface ITurnoverApiClient : IDI
    {
        Task<List<TurnoverDto>> GetTurnoverAsync(int userID, int firmID, TurnoverRequest request);

        Task<List<SubcontoTurnoverDto>> GetSubcontoTurnoversAsync(int userId, int firmId, SubcontoTurnoversRequestDto request);
    }
}
