using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments.Models;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.LinkedDocuments.BaseDocuments
{
    [InjectAsSingleton(typeof(IBaseDocumentReader))]
    internal sealed class BaseDocumentReader : IBaseDocumentReader
    {
        private readonly IBaseDocumentsClient client;

        public BaseDocumentReader(
            IBaseDocumentsClient client)
        {
            this.client = client;
        }

        public async Task<BaseDocument[]> GetByIdsAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            var dto = await client.GetByIdsAsync(documentBaseIds);
            return dto.Select(Map).ToArray();
        }

        private static BaseDocument Map(BaseDocumentDto dto)
        {
            return new BaseDocument
            {
                Id = dto.Id,
                Type = (LinkedDocumentType)dto.Type,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
            };
        }
    }
}