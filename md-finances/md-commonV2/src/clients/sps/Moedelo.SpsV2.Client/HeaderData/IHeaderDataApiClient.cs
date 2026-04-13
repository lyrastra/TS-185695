using System.Threading.Tasks;
using Moedelo.SpsV2.Dto.HeaderData;

namespace Moedelo.SpsV2.Client.HeaderData
{
    public interface IHeaderDataApiClient
    {
        Task<HeaderDataDto> GetHeaderDataAsync();
    }
}