using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Finances.Money;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Extensions.Finances.Money;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.Money.Operations;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Helpers;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.Finances.DataAccess.Money.Operations.PaymentOrders
{
    [InjectAsSingleton]
    public class PaymentOrdersDuplicatesDao : IPaymentOrdersDuplicatesDao
    {
        private readonly IMoedeloDbExecutor dbExecutor;

        public PaymentOrdersDuplicatesDao(IMoedeloDbExecutor dbExecutor)
        {
            this.dbExecutor = dbExecutor;
        }

        public Task<int?> GetPaymentOrderIdAsync(DuplicateOperationRequest request)
        {
            var param = new
            {
                request.FirmId,
                request.PaymentOrderNumber,
                request.Sum,
                request.Date,
                request.Direction,
                request.StartDate,
                request.EndDate,
                RegularOperationState = OperationState.Default,
                BadOperationStates = OperationStateExtensions.BadOperationStates.Cast<int>().ToIntListTVP()
            };
            var queryObject = new QueryObject(PaymentOrdersDuplicatesQueries.GetPaymentOrderId, param);
            return dbExecutor.FirstOrDefaultAsync<int?>(queryObject);
        }

        public Task<List<OperationDuplicate>> GetAllPaymentOrdersAsync(DuplicateOperationRequest request)
        {
            var param = new
            {
                request.FirmId,
                request.Sum,
                request.Date,
                request.Direction,
                StartDate = (request.Direction == (int)MoneyDirection.Outgoing ? request.StartDate : request.Date),
                EndDate = (request.Direction == (int)MoneyDirection.Outgoing ? request.EndDate : request.Date),
                RegularOperationState = OperationState.Default,
                BadOperationStates = new[]
                {
                        OperationState.ImportProcessing,
                        OperationState.DuplicateProcessing,
                        OperationState.MissingKontragentProcessing,
                        OperationState.MissingWorkerProcessing,
                        OperationState.Invalid
                }.Cast<int>().ToIntListTVP(),
                PaymentOrderOutgoingForTransferSalaryOperationType = OperationType.PaymentOrderOutgoingForTransferSalary
            };
            var queryObject = new QueryObject(PaymentOrdersDuplicatesQueries.GetAllPaymentOrders, param);
            return dbExecutor.QueryAsync<OperationDuplicate>(queryObject);
        }

        public Task<List<OperationDuplicateForBatchCheck>> GetForBatchDetectonAsync(int firmId, DetermineOutgoingOperationsDuplicatesDbRequest request)
        {
            var param = new
            {
                firmId,
                request.StartDate,
                request.EndDate,
                request.SettlementAccountId,
                RegularOperationState = OperationState.Default,
                OutgoingDirection = MoneyDirection.Outgoing,
                IncomingDirection = MoneyDirection.Incoming,
                BadOperationStates = new[]
                {
                    OperationState.ImportProcessing,
                    OperationState.DuplicateProcessing,
                    OperationState.MissingKontragentProcessing,
                    OperationState.MissingWorkerProcessing,
                    OperationState.Invalid
                }.Cast<int>().ToIntListTVP(),
                PaymentOrderOutgoingForTransferSalaryOperationType = OperationType.PaymentOrderOutgoingForTransferSalary,
                PaymentOrderOutgoingProfitWithdrawingOperationType = OperationType.PaymentOrderOutgoingProfitWithdrawing
            };
            var queryObject = new QueryObject(PaymentOrdersDuplicatesQueries.GetForBatchDetection, param);
            return dbExecutor.QueryAsync<OperationDuplicateForBatchCheck>(queryObject);
        }
    }
}