using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.TaxPostings.Models;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Models;

namespace Moedelo.Money.Providing.Business.TaxPostings
{
    [InjectAsSingleton(typeof(UsnPostingsSaver))]
    class UsnPostingsSaver
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ITaxPostingsUsnClient usnTaxPostingsClient;

        public UsnPostingsSaver(
            IExecutionInfoContextAccessor contextAccessor,
            ITaxPostingsUsnClient usnTaxPostingsClient)
        {
            this.contextAccessor = contextAccessor;
            this.usnTaxPostingsClient = usnTaxPostingsClient;
        }

        public async Task OverwriteAsync(long documentBaseId, IReadOnlyCollection<UsnTaxPosting> postings)
        {
            await DeleteRelatedAsync(documentBaseId);
            await CreateAsync(postings);
        }

        public async Task CreateAsync(IReadOnlyCollection<UsnTaxPosting> postings)
        {
            if (postings.Count <= 0)
            {
                return;
            }

            var context = contextAccessor.ExecutionInfoContext;
            var postingsDto = postings.Select(Map).ToArray();
            await usnTaxPostingsClient.SaveAsync(context.FirmId, context.UserId, postingsDto);
        }

        public Task DeleteRelatedAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return usnTaxPostingsClient.DeleteByRelatedDocumentIdAsync(context.FirmId, context.UserId, documentBaseId);
        }

        public Task DeleteRelatedExcludingOwnAsync(long documentBaseId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return usnTaxPostingsClient.DeleteByRelatedDocumentIdNotInDocumentIdAsync(
                context.FirmId,
                context.UserId,
                documentBaseId);
        }

        private static TaxPostingUsnDto Map(UsnTaxPosting posting)
        {
            return new TaxPostingUsnDto
            {
                DocumentId = posting.DocumentId,
                DocumentDate = posting.DocumentDate,
                NumberOfDocument = posting.DocumentNumber,
                PostingDate = posting.Date,
                Sum = posting.Sum,
                Direction = posting.Direction,
                Destination = posting.Description,
                RelatedDocumentBaseIds = posting.RelatedDocumentBaseIds?.ToList()
            };
        }
    }
}
