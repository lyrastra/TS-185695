using Moedelo.Common.Enums.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money.Duplicates;
using Moedelo.Finances.Domain.Interfaces.DataAccess.FinancialOperations;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Finances.Business.Services.Money.Operations.Duplicates
{
    [InjectAsSingleton]
    public class DuplicateDetector : IDuplicateDetector
    {
        private const string Tag = nameof(DuplicateDetector);

        private readonly ILogger logger;
        private readonly IPaymentOrdersDuplicatesDao paymentOrdersDuplicatesDao;
        private readonly IFinancialOperationsDuplicatesDao financialOperationsDuplicatesDao;

        public DuplicateDetector(
            ILogger logger,
            IPaymentOrdersDuplicatesDao paymentOrdersDuplicatesDao,
            IFinancialOperationsDuplicatesDao financialOperationsDuplicatesDao)
        {
            this.logger = logger;
            this.paymentOrdersDuplicatesDao = paymentOrdersDuplicatesDao;
            this.financialOperationsDuplicatesDao = financialOperationsDuplicatesDao;
        }

        public async Task<DuplicateDetectionResult[]> DetectAsync(DuplicateDetectionRequest request)
        {
            var sourceOperations = await GetSourceOperationsAsync(request).ConfigureAwait(false);
            if (sourceOperations.Count == 0)
            {
                return request.Operations
                    .Select(x => DuplicateDetectionResult.NotFound(x.Guid))
                    .ToArray();
            }

            logger.Info(Tag, $"Operations for duplicate detection for firm {request.FirmId} like: {JsonConvert.SerializeObject(sourceOperations, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");

            return request.Operations
                .Select(x => x.Direction == MoneyDirection.Outgoing
                    ? OutgoingOperationsDuplicateDetector.Detect(sourceOperations, x)
                    : IncomingOperationsDuplicateDetector.Detect(sourceOperations, x))
                .ToArray();
        }

        private Task<List<OperationDuplicateForBatchCheck>> GetSourceOperationsAsync(DuplicateDetectionRequest request)
        {
            var dbRequest = MapToDbRequest(request);
            return request.IsAccounting
                ? paymentOrdersDuplicatesDao.GetForBatchDetectonAsync(request.FirmId, dbRequest)
                : financialOperationsDuplicatesDao.GetForBatchDetectionAsync(request.FirmId, dbRequest);
        }

        private static DetermineOutgoingOperationsDuplicatesDbRequest MapToDbRequest(DuplicateDetectionRequest request)
        {
            return new DetermineOutgoingOperationsDuplicatesDbRequest
            {
                StartDate = request.Operations.Min(x => x.Date).AddDays(-15),
                EndDate = request.Operations.Max(x => x.Date).AddDays(15),
                SettlementAccountId = request.SettlementAccountId
            };
        }
    }
}