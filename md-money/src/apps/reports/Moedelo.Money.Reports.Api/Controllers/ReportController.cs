using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moedelo.Infrastructure.AspNetCore.Abstractions.BackgroundTasks;
using Moedelo.Money.Reports.Api.Models;
using Moedelo.Money.Reports.Business.Abstractions.BankAndServiceBalanceReport;

namespace Moedelo.Money.Reports.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("private/api/v{version:apiVersion}/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IBackgroundTaskQueue backgroundTaskQueue;
        private readonly IBankAndServiceBalanceReportSender reportSender;
        private readonly IBankAndServiceBalanceReportReader reportReader;
        private readonly IBankAndServiceBalanceReportPrintService printService;

        public ReportController(
            IBackgroundTaskQueue backgroundTaskQueue,
            IBankAndServiceBalanceReportSender reportSender,
            IBankAndServiceBalanceReportReader reportReader,
            IBankAndServiceBalanceReportPrintService printService)
        {
            this.backgroundTaskQueue = backgroundTaskQueue;
            this.reportSender = reportSender;
            this.reportReader = reportReader;
            this.printService = printService;
        }

        [HttpGet("DownloadGetBankAndServiceReport")]
        public async Task<IActionResult> DownloadGetBankAndServiceReportAsync(DateTime date)
        {
            var reportRows = await reportReader.ReadAsync(date);
            var reportFile = printService.GetReport(reportRows, date);

            return File(reportFile.Content, reportFile.ContentType, reportFile.FileName);
        }

        [HttpPost("QueryGetBankAndServiceReport")]
        public IActionResult QueryGetBankAndServiceReport(DownloadGetBankAndServiceReportQueryDto request)
        {
            Task CreateWorkItem(CancellationToken cancellationToken)
            {
                return reportSender.QueryReportAsync(request.OnDate, request.Email);
            }

            backgroundTaskQueue.QueueBackgroundWorkItem(CreateWorkItem);

            return Ok();
        }
    }

    
}
