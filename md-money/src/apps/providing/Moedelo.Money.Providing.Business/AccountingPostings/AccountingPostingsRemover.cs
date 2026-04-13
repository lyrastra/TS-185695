using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.AccountingPostings
{
    [InjectAsSingleton(typeof(AccountingPostingsRemover))]
    class AccountingPostingsRemover
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IAccountingPostingsClient client;

        public AccountingPostingsRemover(
            IExecutionInfoContextAccessor contextAccessor,
            IAccountingPostingsClient client)
        {
            this.contextAccessor = contextAccessor;
            this.client = client;
        }

        public Task DeleteAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return client.DeleteByDocumentAsync(context.FirmId, context.UserId, documentBaseId);
        }
    }
}
