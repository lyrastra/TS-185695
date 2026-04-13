using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonApiV2.Dto.Support;

namespace Moedelo.CommonApiV2.Client.Support;

public interface ISupportDataClient
{
    Task<SupportDataDto> GetAsync(int firmId, int userId, CancellationToken cancellationToken = default);
}