using System.Linq;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Reports;
using Moedelo.Finances.Domain.Models.Reports;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.RequisitesV2.Client.Patent;

namespace Moedelo.Finances.Business.Services.Reports.PatentIncome
{
    [InjectAsSingleton]
    public class PatentIncomeReportService : IPatentIncomeReportService
    {
        private readonly IMoneyOperationReader moneyOperationReader;
        private readonly IPatentApiClient patentApiClient;

        public PatentIncomeReportService(
            IMoneyOperationReader moneyOperationReader,
            IPatentApiClient patentApiClient)
        {
            this.moneyOperationReader = moneyOperationReader;
            this.patentApiClient = patentApiClient;
        }


        public async Task<Report> GetReportAsync(IUserContext userContext, long patentId)
        {
            var operations =  await moneyOperationReader.GetByPatentAsync(userContext.FirmId, patentId).ConfigureAwait(false);
            var patent = await patentApiClient.GetAsync(userContext.FirmId, userContext.UserId, patentId).ConfigureAwait(false);
            var contextExtraData = await userContext.GetContextExtraDataAsync().ConfigureAwait(false);

            return PatentIncomeReportMaker.CreateReportFile(
                contextExtraData.OrganizationName,
                patent.ShortName,
                patent.StartDate.Year,
                operations.Where(operation => operation.Direction == MoneyDirection.Incoming).ToArray());
        }
    }
}
