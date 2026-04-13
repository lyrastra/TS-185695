using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.QRCode.Client
{
    public interface IQrCodeApiClient : IDI
    {
        Task<HttpFileModel> GenerateAsync(string codeText, int firmId, int userId, float? sizeMultiplier = null);
    }
}