using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy;

namespace Moedelo.Money.Business.TaxPostings
{
    [InjectAsSingleton(typeof(ITaxPostingsRemover))]
    public class TaxPostingsRemover : ITaxPostingsRemover
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ITaxPostingsPsnClient taxPostingsPsnClient;

        public TaxPostingsRemover(IExecutionInfoContextAccessor contextAccessor,
            ITaxPostingsPsnClient taxPostingsPsnClient)
        {
            this.contextAccessor = contextAccessor;
            this.taxPostingsPsnClient = taxPostingsPsnClient;
        }

        public Task DeletePatentPostingsAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return taxPostingsPsnClient.DeleteByRelatedDocumentAsync(context.FirmId, context.UserId, documentBaseId);
        }
    }
}