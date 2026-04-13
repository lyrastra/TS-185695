using Moedelo.CommonV2.EventBus;
using Moedelo.CommonV2.EventBus.Integrations;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.Utils;
using Moedelo.Finances.Domain.Interfaces.Business.Reports;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Reconcilation;
using Moedelo.Finances.Domain.Models.Money.Reconciliation;
using Moedelo.InfrastructureV2.Domain.Interfaces.EventBus;
using Moedelo.InfrastructureV2.WebApi.Providers;
using Moedelo.RequisitesV2.Client.SettlementAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.PaymentImport.Client.Reconciliation;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.Finances.Api.Controllers
{
    [RoutePrefix("Money/Reconciliation/Test")]
    public class ReconciliationTestController : ApiController
    {
        private readonly IUserContext userContext;
        private readonly ISettlementAccountClient settlementAccountClient;
        private readonly IBalanceReconcilationDao balanceReconcilationDao;
        private readonly IReconciliationReportService reconciliationReportService;
        private readonly IReconciliationTempFileStorageClient reconciliationTempFileStorageClient;
        private readonly IPublisher<MovementListReviseForUserEvent> integrationsMovementListReviseForUserPublisher;

        public ReconciliationTestController(
            IUserContext userContext,
            IPublisherFactory publisherFactory,
            ISettlementAccountClient settlementAccountClient,
            IBalanceReconcilationDao balanceReconcilationDao,
            IReconciliationReportService reconciliationReportService,
            IReconciliationTempFileStorageClient reconciliationTempFileStorageClient)
        {
            this.userContext = userContext;
            this.settlementAccountClient = settlementAccountClient;
            this.balanceReconcilationDao = balanceReconcilationDao;
            this.reconciliationReportService = reconciliationReportService;
            this.reconciliationTempFileStorageClient = reconciliationTempFileStorageClient;
            integrationsMovementListReviseForUserPublisher = publisherFactory.GetForAllClient(EventBusMessages.IntegrationsMovementListReviseForUserEvent);
        }

        [HttpPost]
        [Route("ReconcileForUser")]
        public async Task<IHttpActionResult> ReconcileForUserAsync()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new CustomMultipartFormDataStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider).ConfigureAwait(false);

            var file = provider.FileData.FirstOrDefault();
            if (file == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("File not found") });
            }

            var sessionId = Guid.Parse(provider.FormData["SessionId"]);
            var settlementNumber = provider.FormData["SettlementNumber"];
            var onDate = DateTime.Parse(provider.FormData["EndDate"]);
            var reconciliation = await balanceReconcilationDao.GetBySessionIdAsync(userContext.FirmId, sessionId).ConfigureAwait(false);
            if (reconciliation == null)
            {
                await InitReconciliationForUserAsync(sessionId, settlementNumber, onDate).ConfigureAwait(false);
            }

            var fileName = file.Headers.ContentDisposition.FileName.Trim('"');
            var fileData = await file.ReadAsByteArrayAsync().ConfigureAwait(false);
            var fileId = await reconciliationTempFileStorageClient
                .SaveAsync(new SaveReconciliationTempFileDto { FileName = fileName, FileData = fileData })
                .ConfigureAwait(false);

            await integrationsMovementListReviseForUserPublisher.PublishAsync(new MovementListReviseForUserEvent
            {
                FirmId = userContext.FirmId,
                StartDate = DateTime.Parse(provider.FormData["StartDate"]),
                EndDate = onDate,
                Guid = sessionId,
                IsManual = true,
                MongoObjectId = fileId,
                SettlementNumber = settlementNumber,
                Status = Common.Enums.Enums.Integration.MovementReviseStatus.Success
            }).ConfigureAwait(false);

            return Ok(sessionId);
        }

        [HttpPost]
        [Route("StopReconciliationForUser")]
        public async Task<IHttpActionResult> StopReconciliationForUserAsync()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new CustomMultipartFormDataStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider).ConfigureAwait(false);

            var sessionId = Guid.Parse(provider.FormData["SessionId"]);

            await integrationsMovementListReviseForUserPublisher.PublishAsync(new MovementListReviseForUserEvent
            {
                FirmId = userContext.FirmId,
                StartDate = DateTime.Parse(provider.FormData["StartDate"]),
                EndDate = DateTime.Parse(provider.FormData["EndDate"]),
                Guid = sessionId,
                IsManual = true,
                MongoObjectId = null,
                SettlementNumber = provider.FormData["SettlementNumber"],
                Status = Common.Enums.Enums.Integration.MovementReviseStatus.Empty
            }).ConfigureAwait(false);

            return Ok(sessionId);
        }

        private async Task InitReconciliationForUserAsync(Guid sessionId, string settlementNumber, DateTime? onDate)
        {
            var settlementAccounts = await settlementAccountClient.GetByNumbersAsync(userContext.FirmId, userContext.UserId, new[] { settlementNumber }).ConfigureAwait(false);
            var settlementAccount = settlementAccounts.FirstOrDefault();
            if (settlementAccount == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("wrong settlement number") });
            }

            var reconciliation = new BalanceReconcilation
            {
                ServiceBalance = 0,
                BankBalance = 0,
                ReconcilationDate = onDate ?? DateTime.Now,
                CreateDate = DateTime.Now,
                SessionId = sessionId,
                Status = ReconciliationStatus.InProgress,
                SettlementAccountId = settlementAccount.Id
            };

            await balanceReconcilationDao.SetReadyToCompleteAsync(userContext.FirmId, settlementAccount.Id).ConfigureAwait(false);
            await balanceReconcilationDao.InsertAsync(userContext.FirmId, reconciliation).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("TestFile")]
        public HttpResponseMessage GetTestFile()
        {
            var report = reconciliationReportService.GetReport(new ReconciliationCompareResult
            {
                ExcessOperations = GetExcessOperations(),
                MissingOperations = GetMissingOperations()
            },
            "Название");

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = HttpResponseMessageUtils.CreateFileContent(report.FileName, report.MimeType, report.Content)
            };
            return response;
        }

        private static List<ReconciliationOperation> GetMissingOperations()
        {
            return new List<ReconciliationOperation>
                {
                    new ReconciliationOperation
                    {
                        IsOutgoing = false,
                        Sum = 1000,
                        Date = new DateTime(2017, 1, 22),
                        Number = "18401111",
                        Description = "тратата НДС платёж пыщпыщ второй лист"
                    }
                };
        }

        private static List<ReconciliationOperation> GetExcessOperations()
        {
            return new List<ReconciliationOperation>
                {
                    new ReconciliationOperation
                    {
                        IsOutgoing = false,
                        Sum = 1000,
                        Date = new DateTime(2016, 1, 22),
                        Number = "4545",
                        Description = "тратата НДС платёж пыщпыщ"
                    },
                    new ReconciliationOperation
                    {
                        IsOutgoing = false,
                        Sum = 1000,
                        Date = new DateTime(2016, 1, 22),
                        Number = "4545",
                        Description = "Оплата по счет-договору 38 от 27.11.17 за лентунихромовую, ленту тефлоновую, самоклейку, комбинированную, шнур силиконовый пористый Сумма 15409-38 В т.ч. НДС  (18%) 2350-58"
                    }
                };
        }
    }
}
