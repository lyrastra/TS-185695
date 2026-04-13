using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.RuCaptchaClient
{
    public interface ICaptchaResolverClient : IDI
    {
        Task<string> DecryptCaptchaAsync(string base64File);
    }
}