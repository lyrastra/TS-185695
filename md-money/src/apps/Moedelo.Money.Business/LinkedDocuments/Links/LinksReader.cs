using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.LinkedDocuments.Links
{
    [InjectAsSingleton(typeof(ILinksReader))]
    internal sealed class LinksReader : ILinksReader
    {
        private readonly ILinksClient client;

        public LinksReader(ILinksClient client)
        {
            this.client = client;
        }

        public async Task<LinkWithDocument[]> GetLinksWithDocumentsAsync(long documentBaseId)
        {
            var dto = await client.GetLinksWithDocumentsAsync(documentBaseId);
            return dto.Select(Map).ToArray();
        }

        public async Task<Dictionary<long, LinkWithDocument[]>> GetLinksWithDocumentsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var dto = await client.GetLinksWithDocumentsAsync(documentBaseIds);
            return dto.ToDictionary(x => x.Key, x => x.Value.Select(Map).ToArray());
        }

        private static LinkWithDocument Map(LinkWithDocumentDto dto)
        {
            return new LinkWithDocument
            {
                Document = new BaseDocument
                {
                    Id = dto.Document.Id,
                    Type = (LinkedDocumentType)dto.Document.Type,
                    Date = dto.Document.Date,
                    Number = dto.Document.Number,
                    Sum = dto.Document.Sum,
                },
                Sum = dto.Sum,
                Date = dto.Date,
            };
        }
    }
}