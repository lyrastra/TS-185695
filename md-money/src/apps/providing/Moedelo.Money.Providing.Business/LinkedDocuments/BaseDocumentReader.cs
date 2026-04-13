using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.LinkedDocuments
{
    [InjectAsSingleton(typeof(BaseDocumentReader))]
    internal class BaseDocumentReader
    {
        private readonly IBaseDocumentsClient client;

        public BaseDocumentReader(
            IBaseDocumentsClient client)
        {
            this.client = client;
        }

        public async Task<BaseDocument[]> GetByIdsAsync(IReadOnlyCollection<long> ids)
        {
            var response = await client.GetByIdsAsync(ids);
            return response.Select(Map).ToArray();
        }

        private static BaseDocument Map(BaseDocumentDto dto)
        {
            return new BaseDocument
            {
                Id = dto.Id,
                Type = dto.Type,
                Date = dto.Date,
                Number = dto.Number,
                Sum = dto.Sum,
                TaxStatus = dto.TaxStatus
            };
        }
    }
}
