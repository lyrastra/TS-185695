using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.Business.Abstractions.Operations;
using Moedelo.Money.Business.CashOrders.ApiClient;
using Moedelo.Money.Business.PaymentOrders.ApiClient;
using Moedelo.Money.Business.PurseOperations.ApiClient;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Domain.Operations;

namespace Moedelo.Money.Business.Operations
{
    [InjectAsSingleton(typeof(IOperationsValidationService))]
    internal sealed class OperationsValidationService : IOperationsValidationService
    {
        private static readonly HttpQuerySetting HttpRequestTimeout = new(TimeSpan.FromSeconds(100));
        
        private readonly IPaymentOrderApiClient paymentOrderApiClient;
        private readonly IPurseOperationApiClient purseOperationApiClient;
        private readonly ICashOrderApiClient cashOrderApiClient;

        public OperationsValidationService(
            IPaymentOrderApiClient paymentOrderApiClient, 
            IPurseOperationApiClient purseOperationApiClient, 
            ICashOrderApiClient cashOrderApiClient)
        {
            this.paymentOrderApiClient = paymentOrderApiClient;
            this.purseOperationApiClient = purseOperationApiClient;
            this.cashOrderApiClient = cashOrderApiClient;
        }

        public async Task<IReadOnlyList<DocumentValidationStatus>> GetDocumentsStatusByBaseIdsAsync(DocumentsStatusRequest request)
        {
            if (request.DocBaseIds.Length == 0)
            {
                return [];
            }

            var validationTasks = CreateValidationTasks(request);
            var validationResponses = await Task.WhenAll(validationTasks);

            var allStatuses = validationResponses.SelectMany(x => x);

            return MapToValidationStatuses(allStatuses);
        }

        private IEnumerable<Task<IReadOnlyList<DocumentStatus>>> CreateValidationTasks(DocumentsStatusRequest request)
        {
            yield return paymentOrderApiClient.GetDocumentsStatusAsync(request, HttpRequestTimeout);
            yield return cashOrderApiClient.GetDocumentsStatusByBaseIdsAsync(request.DocBaseIds, HttpRequestTimeout);
            yield return purseOperationApiClient.GetDocumentsStatusByBaseIdsAsync(request.DocBaseIds, HttpRequestTimeout);
        }
        
        private static List<DocumentValidationStatus> MapToValidationStatuses(IEnumerable<DocumentStatus> statuses)
        {
            return statuses
                .Select(p => new DocumentValidationStatus
                {
                    DocumentBaseId = p.DocumentBaseId,
                    IsValid = p.IsValid
                })
                .ToList();
        }
    }
}