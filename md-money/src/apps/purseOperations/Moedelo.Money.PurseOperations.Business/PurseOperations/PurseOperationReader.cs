using System.Collections.Generic;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.PurseOperations.Business.Abstractions;
using Moedelo.Money.PurseOperations.Business.Abstractions.Exceptions;
using Moedelo.Money.PurseOperations.DataAccess.Abstractions;
using Moedelo.Money.Enums;
using System.Threading.Tasks;
using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PurseOperations.Business.Abstractions.Models;
using Moedelo.Money.PurseOperations.Domain.Models;

namespace Moedelo.Money.PurseOperations.Business.PurseOperations
{
    [InjectAsSingleton(typeof(IPurseOperationReader))]
    internal class PurseOperationReader : IPurseOperationReader
    {
        private readonly IExecutionInfoContextAccessor executionInfoContext;
        private readonly IPurseOperationDao dao;
        private readonly IPurseOperationReadOnlyDao readOnlyDao;

        public PurseOperationReader(
            IExecutionInfoContextAccessor executionInfoContext,
            IPurseOperationDao dao, 
            IPurseOperationReadOnlyDao readOnlyDao)
        {
            this.executionInfoContext = executionInfoContext;
            this.dao = dao;
            this.readOnlyDao = readOnlyDao;
        }

        public async Task<PurseOperationResponse> GetByBaseIdAsync(long documentBaseId, OperationType operationType)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var purseOperation = await dao.GetAsync((int)context.FirmId, documentBaseId).ConfigureAwait(false);
            CheckPurseOperation(documentBaseId, operationType, purseOperation);
            return new PurseOperationResponse
            {
                PurseOperation = purseOperation
            };
        }

        public async Task<OperationType> GetOperationTypeAsync(long documentBaseId)
        {
            var context = executionInfoContext.ExecutionInfoContext;
            var purseOperation = await dao.GetAsync((int)context.FirmId, documentBaseId).ConfigureAwait(false);
            if (purseOperation == null)
            {
                throw new PurseOperationNotFoundExcepton
                {
                    DocumentBaseId = documentBaseId
                };
            }
            return purseOperation.OperationType;
        }

        public Task<IReadOnlyList<DocumentStatus>> GetDocumentsStatusByBaseIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            return readOnlyDao.GetDocumentsStatusByBaseIdsAsync(documentBaseIds);
        }

        private static void CheckPurseOperation(long documentBaseId, OperationType operationType, PurseOperation purseOperation)
        {
            if (purseOperation == null)
            {
                throw new PurseOperationNotFoundExcepton
                {
                    DocumentBaseId = documentBaseId
                };
            }
            if (purseOperation.OperationType != operationType)
            {
                throw new PurseOperationMismatchTypeExcepton
                {
                    DocumentBaseId = documentBaseId,
                    Expected = operationType,
                    Actual = purseOperation.OperationType
                };
            }
        }
    }
}