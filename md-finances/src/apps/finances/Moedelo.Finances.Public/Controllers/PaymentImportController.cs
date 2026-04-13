using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.PaymentImport;
using Moedelo.Finances.Public.ClientData.PaymentImport;
using Moedelo.InfrastructureV2.WebApi.Providers;
using Moedelo.Finances.Public.Mappers.Setup;
using System.Web.Http.Description;
using Moedelo.Common.Enums.Enums.PaymentImport;
using Moedelo.Finances.Domain.Interfaces.Business.Payment;

namespace Moedelo.Finances.Public.Controllers
{
    [RoutePrefix("PaymentImport")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PaymentImportController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly IPaymentImportService paymentImportService;

        public PaymentImportController(
            IPaymentImportService paymentImportService,
            IUserContext userContext)
        {
            this.paymentImportService = paymentImportService;
            this.userContext = userContext;
        }

        [HttpPost]
        [Route("Import")]
        public async Task<IHttpActionResult> ImportAsync()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return StatusCode(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = await Request.Content
                .ReadAsMultipartAsync(new CustomMultipartFormDataStreamProvider())
                .ConfigureAwait(false);

            var fileData = await GetFileDataAsync(provider).ConfigureAwait(false);
            if (fileData == null)
            {
                return Error((HttpStatusCode)422, "Missing file data");
            }

            var status = await paymentImportService.ImportAsync(userContext, fileData).ConfigureAwait(false);
            return Data(PaymentImportMapper.MapToClient(status), HttpStatusCode.Accepted);
        }

        [HttpPost]
        [Route("ImportByFileId")]
        public async Task<IHttpActionResult> ImportByFileIdAsync(ImportFromUserClientData clientData)
        {
            if (!ModelState.IsValid)
            {
                return ValidationError();
            }

            var status = await paymentImportService.ImportAsync(userContext, Map(clientData)).ConfigureAwait(false);
            return Data(PaymentImportMapper.MapToClient(status), HttpStatusCode.Accepted);
        }

        [HttpGet]
        [Route("ImportMessages")]
        public async Task<IHttpActionResult> GetImportMessagesAsync(CancellationToken ctx)
        {
            var result = await paymentImportService.GetImportMessagesAsync(userContext, ctx).ConfigureAwait(false);
            return Data(result);
        }

        private static async Task<FileData> GetFileDataAsync(CustomMultipartFormDataStreamProvider provider)
        {
            FileData fileData = null;

            if (provider.FileData != null && provider.FileData.Any())
            {
                var file = provider.FileData[0];
                var name = file.Headers.ContentDisposition.FileName.Trim('"');
                var content = await file.ReadAsByteArrayAsync().ConfigureAwait(false);

                fileData = new FileData
                {
                    Name = name,
                    Content = content,
                    Size = content.Length
                };
            }

            return fileData;
        }

        private ImportFromUser Map(ImportFromUserClientData clientData)
        {
            return new ImportFromUser
            {
                FileId = clientData.FileId,
                CheckDocuments = clientData.CheckDocuments,
                SecondSettlementAccount = clientData.SecondSettlementAccount,
                SettlementAccountType = clientData.SettlementAccountType
            };
        }
    }
}
