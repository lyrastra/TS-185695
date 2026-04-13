using System.Threading.Tasks;
using Moedelo.Eds.Dto.RequestArchive;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Eds.Client.RequestArchive
{
    public interface IRequestArchiveClient : IDI
    {
        Task<EdsRequestDto> GetByIdAsync(int id);
        Task<RequestArchiveResponse> GetByCriteriaAsync(RequestCriteria criteria);
        Task<RequestArchiveResponse> UpdateRowsAsync(UpdateRequestDto updateRequest);
        Task<byte[]> GetExcelSheetByCriteria(RequestCriteria criteria);
    }
}