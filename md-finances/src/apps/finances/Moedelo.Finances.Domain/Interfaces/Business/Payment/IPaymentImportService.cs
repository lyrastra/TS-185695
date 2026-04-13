using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.PaymentImport;

namespace Moedelo.Finances.Domain.Interfaces.Business.Payment;

public interface IPaymentImportService
{
    Task<ImportStatus> ImportAsync(IUserContext userContext, FileData fileData);
    Task<ImportStatus> ImportAsync(IUserContext userContext, ImportFromUser data);
    Task<string> GetImportMessagesAsync(IUserContext userContext, CancellationToken ctx);
}