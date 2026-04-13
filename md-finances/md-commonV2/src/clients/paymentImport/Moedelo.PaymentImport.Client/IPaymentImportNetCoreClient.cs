using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PaymentImport.Dto;
using System.Threading.Tasks;

namespace Moedelo.PaymentImport.Client
{
    public interface IPaymentImportNetCoreClient : IDI
    {
        Task<ImportStatusDto> CheckImportFileAsync(int firmId, int userId, ImportFromUserDto data);
    }
}
