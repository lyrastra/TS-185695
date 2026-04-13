using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Finances.Domain.Enums.Money;
using Moedelo.Finances.Domain.Interfaces.Business.Money;
using Moedelo.Finances.Domain.Interfaces.DataAccess.FinancialOperations;
using Moedelo.Finances.Domain.Models.Money.Duplicates;
using Moedelo.Finances.Domain.Models.Money.Operations.Duplicates;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.Finances.Business.Services.Money
{
    [InjectAsSingleton]
    public class FinancialOperationsDuplicatesReader : IFinancialOperationsDuplicatesReader
    {
        private readonly IFinancialOperationsDuplicatesDao dao;

        public FinancialOperationsDuplicatesReader(
            IFinancialOperationsDuplicatesDao dao)
        {
            this.dao = dao;
        }

        // TODO: Выпилить!!
        public Task<int?> GetIncomingOperationIdAsync(DuplicateOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Incoming;
            return dao.GetOperationIdAsync(request);
        }

        // TODO: Выпилить!!
        public Task<int?> GetOutgoingOperationIdAsync(DuplicateOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Outgoing;
            return dao.GetOperationIdAsync(request);
        }

        public Task<List<OperationDuplicate>> GetAllOperationsDuplicateAsync(DuplicateOperationRequest request, 
            FinancialOperationDirection direction)
        {
            request.Direction =  (int)direction;
            return dao.GetAllOperationsAsync(request);
        }


        public Task<int?> GetMaterialAidOperationIdAsync(DuplicateOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Incoming;
            return dao.GetMaterialAidOperationIdAsync(request);
        }

        public Task<int?> GetUkInpamentOperationIdAsync(DuplicateOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Incoming;
            return dao.GetUkInpamentOperationIdAsync(request);
        }
        
        public Task<int?> GetBudgetaryPaymentOperationIdAsync(DuplicateBudgetaryPaymentOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Outgoing;
            /*request.BudgetaryPaymentType = (BudgetaryPaymentType)paymentType,
            request.BudgetaryPaymentSubType = paymentSubType.HasValue
                    ? (BudgetaryPaymentSubtype?)paymentSubType
                    : null;*/
            return dao.GetBudgetaryPaymentOperationIdAsync(request);
        }

        public Task<int?> GetDividendPaymentOperationIdAsync(DuplicateDividendPaymentOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Outgoing;
            return dao.GetDividendPaymentOperationIdAsync(request);
        }

        public async Task<int?> GetMovementOperationIdAsync(DuplicateMovementOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Movement;
            return await dao.GetMovementOperationIdAsync(request).ConfigureAwait(false);
        }

        public Task<int?> GetRoboAndSapeIncomingOperationIdAsync(DuplicateOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Incoming;
            return dao.GetRoboAndSapeIncomingOperationIdAsync(request);
        }

        public Task<int?> GetRoboAndSapeOutgoingOperationIdAsync(DuplicateOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Outgoing;
            return dao.GetRoboAndSapeOutgoingOperationIdAsync(request);
        }

        public Task<int?> GetYandexIncomingOperationIdAsync(DuplicateOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Incoming;
            return dao.GetYandexOperationIdAsync(request);
        }

        public Task<int?> GetYandexOutgoingOperationIdAsync(DuplicateOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Outgoing;
            return dao.GetYandexOperationIdAsync(request);
        }

        public Task<int?> GetYandexMovementOperationIdAsync(DuplicateOperationRequest request)
        {
            request.Direction = (int)FinancialOperationDirection.Movement;
            return dao.GetYandexMovementOperationIdAsync(request);
        }
    }
}