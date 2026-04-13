using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.LinkedDocuments.BaseDocuments
{
    [InjectAsSingleton(typeof(IBaseDocumentCreator))]
    internal sealed class BaseDocumentCreator : IBaseDocumentCreator
    {
        private readonly IBaseDocumentsClient client;

        public BaseDocumentCreator(
            IBaseDocumentsClient client)
        {
            this.client = client;
        }

        public Task<long> CreateForPaymentOrderAsync(BaseDocumentCreateRequest request)
        {
            var dto = MapToDto(request);
            dto.Type = LinkedDocumentType.PaymentOrder;
            return client.CreateAsync(dto);
        }

        public Task<long> CreateForOutgoingCashOrderAsync(BaseDocumentCreateRequest request)
        {
            var dto = MapToDto(request);
            dto.Type = LinkedDocumentType.OutcomingCashOrder;
            return client.CreateAsync(dto);
        }

        public Task<long> CreateForUnifiedBudgetaryPaymentAsync(BaseDocumentCreateRequest request)
        {
            var dto = MapToDto(request);
            dto.Type = LinkedDocumentType.UnifiedBudgetarySubPayment;
            return client.CreateAsync(dto);
        }

        private static BaseDocumentCreateDto MapToDto(BaseDocumentCreateRequest request)
        {
            return new BaseDocumentCreateDto
            {
                
                Date = request.Date,
                Number = request.Number,
                Sum = request.Sum
            };
        }
    }
}