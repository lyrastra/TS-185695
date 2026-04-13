using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Payment;
using Moedelo.InfrastructureV2.WebApi.Extensions;

namespace Moedelo.Finances.Public.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentExportController(
        IUserContext userContext,
        IPaymentExportService paymentExport) : BaseApiController
    {
        const string ArchiveName = "Выписки.zip";
        const string ContentType = "application/zip";

        [HttpGet]
        [Route("PaymentExport")]
        public async Task<IHttpActionResult> GetZipFileAsync(CancellationToken cancellationToken)
        {
            var excerpt = await paymentExport
                .GetZipFileAsync(userContext)
                .ConfigureAwait(false);
            return this.File(excerpt, ArchiveName, ContentType);
        }
    }
}