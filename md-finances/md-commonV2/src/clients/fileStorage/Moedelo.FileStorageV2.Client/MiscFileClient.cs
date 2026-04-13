using System.Threading.Tasks;
using Moedelo.FileStorageV2.Dto;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.FileStorageV2.Client
{
    [InjectAsSingleton]
    public class MiscFileClient : IMiscFileClient
    {
        public Task UploadAsync(int firmId, int userId, MiscFileDto dto)
        {
            return Task.CompletedTask;
        }
    }
}