using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Providing.Business.Abstractions.TaxPostings.Models;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.Providing.Business.TaxPostings
{
    [InjectAsSingleton(typeof(PatentPostingsSaver))]
    internal class PatentPostingsSaver
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ITaxPostingsPsnClient psnTaxPostingsClient;

        public PatentPostingsSaver(
            IExecutionInfoContextAccessor contextAccessor,
            ITaxPostingsPsnClient psnTaxPostingsClient)
        {
            this.contextAccessor = contextAccessor;
            this.psnTaxPostingsClient = psnTaxPostingsClient;
        }

        public async Task OverwriteAsync(long documentBaseId, IReadOnlyCollection<PatentTaxPosting> postings)
        {
            var context = contextAccessor.ExecutionInfoContext;

            await psnTaxPostingsClient.DeleteByRelatedDocumentAsync(context.FirmId, context.UserId, documentBaseId);

            if (postings.Count <= 0)
            {
                return;
            }

            var postingsDto = postings.Select(Map).ToArray();
            await psnTaxPostingsClient.SaveAsync(context.FirmId, context.UserId, postingsDto);
        }

        private static TaxPostingPsnDto Map(PatentTaxPosting posting)
        {
            return new TaxPostingPsnDto
            {
                DocumentId = posting.DocumentId,
                PostingDate = posting.Date,
                Sum = posting.Sum,
                Destination = posting.Description,
                RelatedDocumentBaseIds = posting.RelatedDocumentBaseIds?.ToList()
            };
        }
    }
}
