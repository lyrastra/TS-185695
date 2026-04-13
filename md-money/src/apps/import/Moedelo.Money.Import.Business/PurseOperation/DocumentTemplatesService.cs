using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Moedelo.Accounting.Domain.Shared.NdsRates;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.Import.Business.Helpers;
using Moedelo.Money.Import.Domain.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.Import.Business.PurseOperation
{
    [InjectAsSingleton(typeof(IDocumentTemplateService))]
    public class DocumentTemplatesService : IDocumentTemplateService
    {
        private readonly IFirmRequisitesApiClient firmRequisitesApiClient;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;

        public DocumentTemplatesService(
            IFirmRequisitesApiClient firmRequisitesApiClient, 
            IExecutionInfoContextAccessor executionInfoContextAccessor)
        {
            this.firmRequisitesApiClient = firmRequisitesApiClient;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
        }

        public async Task<HttpFileModel> GetImportByPaymentSystems(int? currentYear)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;
            
            currentYear ??= DateTime.Now.Year;

            var firmRegistrationData = await firmRequisitesApiClient
                .GetRegistrationDataAsync(executionContext.FirmId, executionContext.UserId);

            var nameFile = GetNameFile(currentYear, firmRegistrationData);

            var pathTemplateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameFile);
        
            var bytes = await File.ReadAllBytesAsync(pathTemplateFile);
        
            return new HttpFileModel(
                "Import_by_payment_systems.xls", 
                MediaTypeNames.Application.Octet,
                new MemoryStream(bytes));
        }

        private static string GetNameFile(int? currentYear, RegistrationDataDto firmRegistrationData)
        {
            if (!(currentYear >= NdsRateStartDates.Nds5Or7StartDate.Year)) 
                return "Content/Import_by_payment_systems.xls";
            
            return firmRegistrationData.IsOoo ? 
                "Content/Import_by_payment_systems_2025_ООО.xls" : 
                "Content/Import_by_payment_systems_2025_ИП.xls";
        }
    }
}