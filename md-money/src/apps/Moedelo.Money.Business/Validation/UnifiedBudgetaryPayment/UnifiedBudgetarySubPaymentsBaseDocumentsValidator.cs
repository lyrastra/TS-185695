using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(UnifiedBudgetarySubPaymentsBaseDocumentsValidator))]
    internal class UnifiedBudgetarySubPaymentsBaseDocumentsValidator
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IBaseDocumentReader baseDocumentReader;

        public UnifiedBudgetarySubPaymentsBaseDocumentsValidator(
            IExecutionInfoContextAccessor contextAccessor,
            IBaseDocumentReader baseDocumentReader)
        {
            this.contextAccessor = contextAccessor;
            this.baseDocumentReader = baseDocumentReader;
        }

        public async Task<BaseDocument[]> ValidateAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            UnifiedBudgetarySubPaymentsDuplicateValidator.Validate(documentBaseIds);

            var baseDocuments = (await baseDocumentReader.GetByIdsAsync(documentBaseIds))
                .Where(x => x.Type == LinkedDocumentType.UnifiedBudgetarySubPayment)
                .ToArray();

            var baseDocumentsById = baseDocuments.ToDictionary(x => x.Id);

            var i = 0;
            foreach (var documentBaseId in documentBaseIds)
            {
                if (documentBaseId > 0 &&
                    baseDocumentsById.TryGetValue(documentBaseId, out var baseDocument) == false)
                {
                    throw new BusinessValidationException($"SubPayments[{i}].DocumentBaseId", $"Не найден дочерний бюджетный платеж с базовым ид {documentBaseId}");
                }
                i++;
            }

            return baseDocuments;
        }
    }
}
