using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.Kafka.Abstractions.BaseDocuments.Commands;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.TaxPostings
{
    [InjectAsSingleton(typeof(TaxPostingsRemover))]
    internal class TaxPostingsRemover
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ITaxPostingsUsnClient usnTaxPostingsClient;
        private readonly ITaxPostingsOsnoClient osnoTaxPostingsClient;
        private readonly ITaxPostingsPsnClient psnTaxPostingsClient;
        private readonly IBaseDocumentsCommandWriter baseDocumentsCommandWriter;

        public TaxPostingsRemover(
            IExecutionInfoContextAccessor contextAccessor,
            ITaxPostingsUsnClient usnTaxPostingsClient,
            ITaxPostingsOsnoClient osnoTaxPostingsClient,
            ITaxPostingsPsnClient psnTaxPostingsClient,
            IBaseDocumentsCommandWriter baseDocumentsCommandWriter)
        {
            this.contextAccessor = contextAccessor;
            this.usnTaxPostingsClient = usnTaxPostingsClient;
            this.osnoTaxPostingsClient = osnoTaxPostingsClient;
            this.psnTaxPostingsClient = psnTaxPostingsClient;
            this.baseDocumentsCommandWriter = baseDocumentsCommandWriter;
        }

        public Task DeleteAndUnsetTaxStatusAsync(long documentBaseId)
        {
            return Task.WhenAll(
                DeleteAsync(documentBaseId),
                baseDocumentsCommandWriter.WriteAsync(new SetTaxStatusCommand { Id = documentBaseId }));
        }

        public Task DeleteAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return Task.WhenAll(
                usnTaxPostingsClient.DeleteByRelatedDocumentIdAsync(context.FirmId, context.UserId, documentBaseId),
                osnoTaxPostingsClient.DeleteAsync(context.FirmId, context.UserId, documentBaseId),
                psnTaxPostingsClient.DeleteByRelatedDocumentAsync(context.FirmId, context.UserId, documentBaseId)
            );
        }
    }
}
