using System.Text;
using System.Threading;
using Moedelo.CommonV2.UserContext.Domain;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.Payment;

public interface IPaymentExportService
{
    Task<byte[]> GetZipFileAsync(IUserContext userContext, Encoding encoding = null, CancellationToken cancellationToken = default(CancellationToken));
}