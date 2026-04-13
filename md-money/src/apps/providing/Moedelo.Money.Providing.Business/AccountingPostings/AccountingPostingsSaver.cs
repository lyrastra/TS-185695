using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings.Dto;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.AccountingPostings
{
    [InjectAsSingleton(typeof(AccountingPostingsSaver))]
    class AccountingPostingsSaver
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IAccountingPostingsClient client;

        public AccountingPostingsSaver(
            IExecutionInfoContextAccessor contextAccessor,
            IAccountingPostingsClient client)
        {
            this.contextAccessor = contextAccessor;
            this.client = client;
        }

        /// <inheritdoc />
        public Task OverwriteAsync(long documentBaseId, IReadOnlyCollection<AccountingPosting> postings)
        {
            // todo: переделать на внутренние вызовы, когда будет полная реализация домена БУ-проводок
            var context = contextAccessor.ExecutionInfoContext;

            // удаляем проводки, если список пустой
            if (postings.Count == 0)
            {
                return client.DeleteByDocumentAsync(context.FirmId, context.UserId, documentBaseId);
            }

            var dto = postings.Select(x => Map(documentBaseId, x)).ToArray();
            return client.SaveAsync(context.FirmId, context.UserId, dto);
        }

        private static AccountingPostingDto Map(long documentBaseId, AccountingPosting posting)
        {
            return new AccountingPostingDto
            {
                DocumentBaseId = documentBaseId,
                Date = posting.Date,
                Sum = posting.Sum,
                DebitCode = posting.DebitCode,
                DebitSubcontos = posting.DebitSubcontos.Select(x => x.Id).ToList(),
                CreditCode = posting.CreditCode,
                CreditSubcontos = posting.CreditSubcontos.Select(x => x.Id).ToList(),
                Description = posting.Description ?? string.Empty,
                OperationType = posting.OperationType,
                IsManual = false
            };
        }
    }
}