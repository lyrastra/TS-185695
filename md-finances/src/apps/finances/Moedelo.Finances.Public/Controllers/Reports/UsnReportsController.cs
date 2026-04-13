using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Reports;
using Moedelo.Finances.Public.ClientData;
using Moedelo.Finances.Public.Mappers;
using Moedelo.InfrastructureV2.WebApi.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Moedelo.Finances.Public.Controllers.Reports
{
    [RoutePrefix("UsnReports")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class UsnReportsController : BaseApiController
    {
        private readonly IIncomeExpenseUsnReportService incomeExpenseUsnReportService;
        private readonly IUserContext userContext;

        public UsnReportsController(
            IIncomeExpenseUsnReportService incomeExpenseUsnReportService,
            IUserContext userContext)
        {
            this.incomeExpenseUsnReportService = incomeExpenseUsnReportService;
            this.userContext = userContext;
        }

        [HttpPost]
        [Route("GetIncomeExpense")]
        public async Task<IHttpActionResult> GetIncomeExpenseAsync(IReadOnlyCollection<PeriodClientData> requestPeriods)
        {
            var periods = ReportsMapper.MapToDomain(requestPeriods);
            var report = await incomeExpenseUsnReportService.GetReportAsync(userContext, periods).ConfigureAwait(false);
            return this.File(report.Content, report.FileName, report.MimeType);
        }
    }
}