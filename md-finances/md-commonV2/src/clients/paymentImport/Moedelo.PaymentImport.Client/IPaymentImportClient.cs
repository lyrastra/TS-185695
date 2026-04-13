using System.Threading;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PaymentImport.Dto;
using System.Threading.Tasks;

namespace Moedelo.PaymentImport.Client
{
    public interface IPaymentImportClient : IDI
    {
        Task<ImportStatusDto> ImportFromUserAsync(int firmId, int userId, ImportFromUserDto fileId);
        Task<ImportStatusDto> ImportFromUserAsync(int firmId, int userId, string fileId, bool processSettlementAccount, bool checkDocuments);
        Task ImportFromIntegrationAsync(int firmId, int userId, string fileId, bool isManual);
        Task ImportFromRobokassaAsync(int firmId, int userId, int fileId);
        Task<ImportStatusDto> ImportFromReconcilationAsync(int firmId, int userId, ReconcilationImportDto dto);
        Task<string> GetImportMessagesAsync(int firmId, int userId, CancellationToken ctx = default);
    }
}