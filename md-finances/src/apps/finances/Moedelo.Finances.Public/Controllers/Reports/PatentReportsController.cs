using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Interfaces.Business.Reports;
using Moedelo.InfrastructureV2.WebApi.Extensions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Moedelo.Finances.Public.Controllers.Reports
{
    [RoutePrefix("PatentReports")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PatentReportsController : BaseApiController
    {
        private readonly IPatentIncomeReportService incomePatentReportService;
        private readonly IUserContext userContext;

        public PatentReportsController(
            IUserContext userContext,
            IPatentIncomeReportService incomePatentReportService)
        {
            this.userContext = userContext;
            this.incomePatentReportService = incomePatentReportService;
        }

        [HttpGet]
        [Route("GetIncome")]
        public async Task<IHttpActionResult> GetIncomeAsync(long patentId)
        {
            var report = await incomePatentReportService.GetReportAsync(userContext, patentId).ConfigureAwait(false);
            return this.File(report.Content, report.FileName, report.MimeType);
        }
    }
}
