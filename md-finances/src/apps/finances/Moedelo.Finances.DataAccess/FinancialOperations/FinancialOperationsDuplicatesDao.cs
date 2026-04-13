using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.FinancialOperations;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.FinancialOperations;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DataAccess;
using Moedelo.InfrastructureV2.Domain.Models.DataAccess;

namespace Moedelo.Finances.DataAccess.FinancialOperations
{
    [InjectAsSingleton]
    public class FinancialOperationsDuplicatesDao : IFinancialOperationsDuplicatesDao
    {
        private readonly IMoedeloDbExecutor dbExecutor;

        public FinancialOperationsDuplicatesDao(IMoedeloDbExecutor dbExecutor)
        {
            this.dbExecutor = dbExecutor;
        }

        public Task<int?> GetOperationIdAsync(DuplicateOperationRequest request)
        {
            var queryObject = new QueryObject(Sql.GetOperationDuplicateId, request);
            return dbExecutor.FirstOrDefaultAsync<int?>(queryObject);
        }

        public Task<int?> GetMaterialAidOperationIdAsync(DuplicateOperationRequest request)
        {
            var queryObject = new QueryObject(Sql.GetMaterialAidOperationDuplicateId, request);
            return dbExecutor.FirstOrDefaultAsync<int?>(queryObject);
        }

        public Task<int?> GetUkInpamentOperationIdAsync(DuplicateOperationRequest request)
        {
            var queryObject = new QueryObject(Sql.GetUkInpamentOperationDuplicateId, request);
            return dbExecutor.FirstOrDefaultAsync<int?>(queryObject);
        }

        public Task<int?> GetBudgetaryPaymentOperationIdAsync(DuplicateBudgetaryPaymentOperationRequest request)
        {
            var queryObject = new QueryObject(Sql.GetBudgetaryPaymentOperationDuplicateId, request);
            return dbExecutor.FirstOrDefaultAsync<int?>(queryObject);
        }

        public Task<int?> GetDividendPaymentOperationIdAsync(DuplicateDividendPaymentOperationRequest request)
        {
            var queryObject = new QueryObject(Sql.GetDividendPaymentOperationDuplicateId, request);
            return dbExecutor.FirstOrDefaultAsync<int?>(queryObject);
        }

        public Task<int?> GetMovementOperationIdAsync(DuplicateMovementOperationRequest request)
        {
            var queryObject = new QueryObject(Sql.GetMovementOperationDuplicateId, request);
            return dbExecutor.FirstOrDefaultAsync<int?>(queryObject);
        }

        public Task<int?> GetRoboAndSapeIncomingOperationIdAsync(DuplicateOperationRequest request)
        {
            var queryObject = new QueryObject(Sql.GetRoboAndSapeIncomingOperationDuplicateId, request);
            return dbExecutor.FirstOrDefaultAsync<int?>(queryObject);
        }

        public Task<int?> GetRoboAndSapeOutgoingOperationIdAsync(DuplicateOperationRequest request)
        {
            var queryObject = new QueryObject(Sql.GetRoboAndSapeOutgoingOperationDuplicateId, request);
            return dbExecutor.FirstOrDefaultAsync<int?>(queryObject);
        }

        public Task<int?> GetYandexOperationIdAsync(DuplicateOperationRequest request)
        {
            var queryObject = new QueryObject(Sql.GetYandexOperationDuplicateId, request);
            return dbExecutor.FirstOrDefaultAsync<int?>(queryObject);
        }

        public Task<int?> GetYandexMovementOperationIdAsync(DuplicateOperationRequest request)
        {
            var queryObject = new QueryObject(Sql.GetYandexMovementOperationDuplicateId, request);
            return dbExecutor.FirstOrDefaultAsync<int?>(queryObject);
        }

        public Task<List<OperationDuplicate>> GetAllOperationsAsync(DuplicateOperationRequest request, bool isMovement = false)
        {
            var param = new
            {
                request.FirmId,
                request.Sum,
                request.Date,
                request.Direction,
                StartDate = request.Direction == (int)FinancialOperationDirection.Outgoing 
                    ? request.StartDate 
                    : request.Date,
                EndDate = request.Direction == (int)FinancialOperationDirection.Outgoing 
                    ? request.EndDate 
                    : request.Date,
                request.SettlementAccountId,
                PayDaysOutgoingOperationType = "PayDaysOperation",
                SalaryProjectMoneyBayType = MoneyBayType.SalaryProject
            };

            var sql = request.Direction == (int)FinancialOperationDirection.Outgoing
                ? Sql.GetAllOutgoingOperations
                : Sql.GetAllIncomingOperations;
            var queryObject = new QueryObject(sql, param);
            return dbExecutor.QueryAsync<OperationDuplicate>(queryObject);
        }

        public Task<List<OperationDuplicateForBatchCheck>> GetForBatchDetectionAsync(int firmId, DetermineOutgoingOperationsDuplicatesDbRequest request)
        {
            var param = new
            {
                firmId,
                request.StartDate,
                request.EndDate,
                request.SettlementAccountId,
                OutgoingDirection = FinancialOperationDirection.Outgoing,
                IncomingDirection = FinancialOperationDirection.Incoming,
                PayDaysOutgoingOperationType = "PayDaysOperation",
                SalaryProjectMoneyBayType = MoneyBayType.SalaryProject
            };
            var queryObject = new QueryObject(Sql.GetForBatchDetection, param);
            return dbExecutor.QueryAsync<OperationDuplicateForBatchCheck>(queryObject);
        }
    }
}