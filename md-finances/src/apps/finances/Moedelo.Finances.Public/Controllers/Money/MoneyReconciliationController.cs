using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.Utils;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Reconciliation;
using Moedelo.Finances.Domain.Interfaces.Business.Reports;
using Moedelo.Finances.Public.ClientData.Money.Reconciliation;
using Moedelo.Finances.Public.Mappers;
using Moedelo.Finances.Public.Mappers.Money;

namespace Moedelo.Finances.Public.Controllers.Money
{
    [RoutePrefix("Money/Reconciliation")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MoneyReconciliationController : BaseApiController
    {
        private readonly IUserContext userContext;
        private readonly IReconciliationService service;
        private readonly IReconciliationForUserInitiator reconciliationForUserService;
        private readonly IReconciliationRequestReportService reconciliationRequestReportService;
        private readonly IReconciliationReportForUserService reconciliationReportForUserService;
        private readonly IReconcilationFinishedIndicator reconciliationIndicator;

        public MoneyReconciliationController(
            IUserContext userContext,
            IReconciliationService service,
            IReconciliationForUserInitiator reconciliationForUserService,
            IReconciliationRequestReportService reconciliationRequestReportService,
            IReconciliationReportForUserService reconciliationReportForUserService,
            IReconcilationFinishedIndicator reconciliationIndicator)
        {
            this.userContext = userContext;
            this.service = service;
            this.reconciliationForUserService = reconciliationForUserService;
            this.reconciliationRequestReportService = reconciliationRequestReportService;
            this.reconciliationReportForUserService = reconciliationReportForUserService;
            this.reconciliationIndicator = reconciliationIndicator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAsync(int? settlementAccountId)
        {
            var result = await service.GetLastAsync(userContext, settlementAccountId).ConfigureAwait(false);
            return result == null ? 
                NotFound() : 
                Data(result.MapToClient());
        }

        [HttpGet]
        [Route("{sessionId:guid}")]
        public async Task<IHttpActionResult> GetBySessionIdAsync(Guid sessionId)
        {
            var result = await service.GetBySessionIdAsync(userContext, sessionId).ConfigureAwait(false);
            if (result == null)
            {
                return NotFound();
            }
            return Data(result.MapToClient());
        }

        [HttpPost]
        [Route("Complete")]
        public async Task<IHttpActionResult> CompleteAsync(ReconcilationCompleteClientData clientData)
        {
            await service.CompleteAsync(userContext, clientData.SessionId, clientData.ExcessOperations, clientData.MissingOperations).ConfigureAwait(false);
            return Ok();
        }

        [HttpPost]
        [Route("Cancel")]
        public async Task<IHttpActionResult> CancelAsync(ReconcilationSessionIdClientData clientData)
        {
            await service.CancelAsync(userContext.FirmId, clientData.SessionId).ConfigureAwait(false);
            return Ok();
        }

        [HttpPost]
        [Route("Reconcile")]
        public async Task<IHttpActionResult> ReconcileAsync([FromUri]int settlementAccountId, [FromBody]ReconciliationRequestClientData clientData)
        {
            if (settlementAccountId <= 0)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

            var sessionId = await service.GetLastSessionInProcessAsync(userContext.FirmId, settlementAccountId).ConfigureAwait(false);
            if (sessionId.HasValue)
            {
                return Json(new ReconcileErrorResultClientData { SessionId = sessionId.Value, Status = ReconciliationStatus.InProgress });
            }

            var result = await reconciliationForUserService.InitiateAsync(userContext, settlementAccountId, clientData.OnDate ?? DateTime.Now).ConfigureAwait(false);
            if (!result)
            {
                return Json(new ReconcileErrorResultClientData { SessionId = Guid.Empty, Status = ReconciliationStatus.Error });
            }

            return Json(new ReconcileOkResultClientData());
        }

        [HttpPost]
        [Route("Report")]
        public async Task<IHttpActionResult> ReportAsync(ReconciliationReportClientData request)
        {
            var model = ReconciliationReportMapper.Map(request);
            var isSuccess = await reconciliationRequestReportService.RequestReportAsync(userContext, model).ConfigureAwait(false);

            return StatusCode(isSuccess
                ? HttpStatusCode.OK
                : HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("Report/Xls")]
        public async Task<HttpResponseMessage> XlsReportAsync(ReconciliationReportForUserClientData request)
        {
            var model = ReconciliationReportForUserMapper.Map(request);
            var report = await reconciliationReportForUserService.GetReportForUserAsync(userContext, model);

            if (report == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = HttpResponseMessageUtils.CreateFileContent(report.FileName, report.MimeType, report.Content)
            };
        }

        /// <summary>
        ///  Индикатор для оповещения о завершении последнеей(неоповещенной) сверке
        /// </summary>
        [HttpGet]
        [Route("Indicator")]
        public async Task<IHttpActionResult> Indicator()
        {
            var idicator = await reconciliationIndicator.LetSeeAsync(userContext.FirmId).ConfigureAwait(false);
            return Json(idicator);
        }
    }
}
