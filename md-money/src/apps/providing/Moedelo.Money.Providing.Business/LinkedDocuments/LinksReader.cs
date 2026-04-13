using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.LinkedDocuments
{
    [InjectAsSingleton(typeof(LinksReader))]
    internal class LinksReader
    {
        private readonly ILinksClient linksClient;

        public LinksReader(ILinksClient linksClient)
        {
            this.linksClient = linksClient;
        }

        public async Task<LinkWithDocument[]> GetByIdAsync(long documentBaseId)
        {
            // крайне не рекомендуется читать с реплики (могут быть некорректные данные в случае перепроведения после сохранения первичного документа)
            var links = await linksClient.GetLinksWithDocumentsAsync(documentBaseId, useReadOnly: false);
            return links?.Select(Map).ToArray() ?? [];
        }

        private static LinkWithDocument Map(LinkWithDocumentDto dto)
        {
            return new LinkWithDocument
            {
                Document = new BaseDocument
                {
                    Id = dto.Document.Id,
                    Type = dto.Document.Type,
                    Sum = dto.Document.Sum,
                    Number = dto.Document.Number,
                    Date = dto.Document.Date
                },
                Date = dto.Date,
                Sum = dto.Sum
            };
        }
    }
}
