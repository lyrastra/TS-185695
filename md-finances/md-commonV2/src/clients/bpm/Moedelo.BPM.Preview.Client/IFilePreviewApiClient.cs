using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BPM.Preview.Client
{
    public interface IFilePreviewApiClient : IDI
    {
        Task<int> GetPreviewAsync(int fileId, int pageIndex);
        Task<int> GetPreviewPageCountAsync(int fileId);
        Task RotateAsync(int fileId, int pageIndex, int degree);
        Task CopyAsync(int fromFileId, int toFileId);
    }
}
