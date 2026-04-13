using System.Collections.Generic;
using System.Linq;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.Import.Domain.Interfaces;
using Moedelo.Money.Import.Domain.Models.PurseOperation;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Common;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Common.Commands;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Models;
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Money.Import.Domain.Exceptions;
using Moedelo.PaymentOrderImport.Enums;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.Import.Business.PurseOperation
{
    [InjectAsSingleton(typeof(IPurseOperationImportService))]
    public class PurseOperationImportService : IPurseOperationImportService
    {
        private readonly IPurseOperationCommandWriter createCommandWriter;
        private readonly IPurseOperationsParserFromExcel purseOperationsParserFromExcel;
        private readonly ISettlementAccountApiClient settlementAccountApiClient;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly IKontragentsApiClient kontragentsApiClient;
        private readonly ITaxationSystemApiClient taxationSystemApiClient;
        private readonly IFirmRequisitesApiClient firmRequisitesApiClient;
        private readonly IPatentApiClient patentApiClient;

        public PurseOperationImportService(
            IPurseOperationCommandWriter createCommandWriter, 
            IPurseOperationsParserFromExcel purseOperationsParserFromExcel, 
            ISettlementAccountApiClient settlementAccountApiClient, 
            IExecutionInfoContextAccessor executionInfoContextAccessor, 
            IKontragentsApiClient kontragentsApiClient, 
            ITaxationSystemApiClient taxationSystemApiClient, 
            IFirmRequisitesApiClient firmRequisitesApiClient, 
            IPatentApiClient patentApiClient)
        {
            this.createCommandWriter = createCommandWriter;
            this.purseOperationsParserFromExcel = purseOperationsParserFromExcel;
            this.settlementAccountApiClient = settlementAccountApiClient;
            this.executionInfoContextAccessor = executionInfoContextAccessor;
            this.kontragentsApiClient = kontragentsApiClient;
            this.taxationSystemApiClient = taxationSystemApiClient;
            this.firmRequisitesApiClient = firmRequisitesApiClient;
            this.patentApiClient = patentApiClient;
        }
    
        public async Task<ImportStatus> FromExcelAsync(PurseOperationImportRequest request)
        {
            var executionContext = executionInfoContextAccessor.ExecutionInfoContext;

            try
            {
                var firmRegistrationData = await firmRequisitesApiClient
                    .GetRegistrationDataAsync(executionContext.FirmId, executionContext.UserId);
                
                var taxationSystems = await taxationSystemApiClient
                    .GetAsync(executionContext.FirmId, executionContext.UserId);
                
                var patents = await patentApiClient
                    .GetWithoutAdditionalDataAsync(executionContext.FirmId, executionContext.UserId);

                var (operations, errors) = purseOperationsParserFromExcel
                    .GetOperations(request, taxationSystems, patents, firmRegistrationData.RegistrationDate);
              
                var existingSettlementAccounts =
                    await GetExistingSettlementAccountsAsync(executionContext, operations, errors);

                var purseKontragent = (await kontragentsApiClient
                    .GetByIdsAsync(executionContext.FirmId, executionContext.UserId, new[] { request.KontragentId }))
                    .FirstOrDefault();

                var populationId = await kontragentsApiClient
                    .GetOrCreatePopulationAsync(executionContext.FirmId, executionContext.UserId);

                if (purseKontragent == null)
                {
                    errors.Add($"Не удалось загрузить файл. Указаный KontragentId = {request.KontragentId} не найден");
                }

                if (!operations.Any())
                {
                    errors.Add("В загруженном файле, не найдено ни одной операции.");
                }

                if (errors.Any())
                {
                    return new ImportStatus
                    {
                        Status = PaymentImportResultStatus.WrongFile,
                        ExData = new
                        {
                            Errors = errors
                        }
                    };
                }

                foreach (var operation in operations)
                {
                    var populationKontragentId = operation.PurseOperationType == PurseOperationType.Income ? populationId : (int?)null;
                    
                    await createCommandWriter.WriteCreateAsync(new CreatePurseOperation
                        {
                            PurseId = request.KontragentId, 
                            KontragentId = populationKontragentId,
                            Date = operation.Date,
                            Sum = operation.Sum,
                            Comment = GetComment(operation.PurseOperationType, purseKontragent),
                            SettlementAccountId = GetSettlementAccountId(
                                operation.PurseOperationType, 
                                existingSettlementAccounts,
                                operation.SettlementAccount),
                            PurseOperationType = operation.PurseOperationType,
                            TaxationSystemType = operation.TaxationSystemType,
                            IncludeNds = operation.IncludeNds,
                            NdsSum = operation.NdsSum,
                            NdsType = operation.NdsType,
                        });
                }
            }
            catch (FileValidationException e)
            {
                return new ImportStatus
                {
                    Status = PaymentImportResultStatus.WrongFile,
                    ExData = new
                    {
                        Errors = e.Errors.Cast<object>().ToArray()
                    }
                };
            }

            return new ImportStatus
            {
                Status = PaymentImportResultStatus.InProcess
            };
        }

        private async Task<IList<SettlementAccountDto>> GetExistingSettlementAccountsAsync(
            ExecutionInfoContext executionContext, IList<PurseOperationFromExcel> operations, IList<string> errors)
        {
            var existingSettlementAccounts = await settlementAccountApiClient
                .GetAsync(executionContext.FirmId, executionContext.UserId);

            var settlementAccounts = operations.Where(s => s.PurseOperationType == PurseOperationType.Transfer)
                .Select(x => x.SettlementAccount)
                .Distinct()
                .ToArray();

            var exceptSettlementAccounts  =  settlementAccounts.Except(existingSettlementAccounts.Select(x => x.Number)).ToArray();
            if (settlementAccounts.Any() && exceptSettlementAccounts.Any())
            {
                var strSettlementAccounts = string.Join(",", exceptSettlementAccounts);
                errors.Add($"Пожалуйста, убедитесь, что расчетный счет [{strSettlementAccounts}] указан в реквизитах и активен.");
            }

            return existingSettlementAccounts;
        }

        private static string GetComment(PurseOperationType purseOperationType, KontragentDto kontragent)
        {
            switch (purseOperationType)
            {
                case PurseOperationType.Income:
                    return $"Поступление в {kontragent.ShortName}";
                case PurseOperationType.Transfer:
                    return $"Перевод из платежной системы {kontragent.ShortName} на расчетный счет";
                case PurseOperationType.Comission:
                    return $"Удержание комиссии платежной системы {kontragent.ShortName}";
            }
            return string.Empty;
        }

        private static int GetSettlementAccountId(PurseOperationType purseOperationType,
            IEnumerable<SettlementAccountDto> settlementAccountsApi, string settlementAccount)
        {
            if (purseOperationType != PurseOperationType.Transfer || string.IsNullOrEmpty(settlementAccount))
            {
                return 0;
            }

            var settlementAccountApi = settlementAccountsApi
                .FirstOrDefault(x => x.Number == settlementAccount && x.IsDeleted == false);
            return settlementAccountApi?.Id ?? 0;
        }
    }
}