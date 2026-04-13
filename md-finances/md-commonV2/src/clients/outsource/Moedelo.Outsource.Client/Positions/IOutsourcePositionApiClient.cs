using System.Threading.Tasks;
using Moedelo.Outsource.Dto.Positions;

namespace Moedelo.Outsource.Client.Positions;

public interface IOutsourcePositionApiClient
{
    Task<int> InsertAsync(int firmId, int userId, PositionPostDto model);
}