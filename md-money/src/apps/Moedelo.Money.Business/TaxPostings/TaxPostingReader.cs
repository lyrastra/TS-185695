using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments;
using Moedelo.LinkedDocuments.Enums;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.Business.TaxPostings
{
    [InjectAsSingleton(typeof(ITaxPostingReader))]
    internal sealed class TaxPostingReader : ITaxPostingReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IBaseDocumentsClient baseDocumentsClient;
        private readonly ITaxPostingsOsnoClient osnoClient;
        private readonly ITaxPostingsUsnClient usnClient;

        public TaxPostingReader(
            IExecutionInfoContextAccessor contextAccessor,
            IBaseDocumentsClient baseDocumentsClient,
            ITaxPostingsOsnoClient osnoClient,
            ITaxPostingsUsnClient usnClient)
        {
            this.contextAccessor = contextAccessor;
            this.baseDocumentsClient = baseDocumentsClient;
            this.osnoClient = osnoClient;
            this.usnClient = usnClient;
        }

        public async Task<TaxPostingsData> GetByBaseIdAsync(long baseId)
        {
            var baseDocument = await baseDocumentsClient.GetByIdAsync(baseId);

            var context = contextAccessor.ExecutionInfoContext;
            var osnoPosting = await osnoClient.GetByBaseIdAsync(context.FirmId, context.UserId, baseId);
            var usnPosting = await usnClient.GetByDocumentIdAsync(context.FirmId, context.UserId, baseId);

            return new TaxPostingsData
            {
                ProvidePostingType = baseDocument?.TaxStatus == TaxPostingStatus.ByHand
                    ? ProvidePostingType.ByHand
                    : ProvidePostingType.Auto,
                OsnoTaxPostings = osnoPosting.Select(MapOsno).ToArray(),
                UsnTaxPostings = usnPosting.Select(MapUsn).ToArray()
            };
        }

        private OsnoTaxPosting MapOsno(TaxPostingOsnoDto dto)
        {
            return new OsnoTaxPosting
            {
                Direction = dto.Direction,
                Kind = dto.Kind,
                NormalizedCostType = dto.NormalizedCostType,
                Type = dto.Type,
                Sum = dto.Sum
            };
        }

        private UsnTaxPosting MapUsn(TaxPostingUsnDto dto)
        {
            return new UsnTaxPosting
            {
                Description = dto.Destination,
                Direction = dto.Direction,
                DocumentBaseId = dto.DocumentId.Value,
                DocumentDate = dto.DocumentDate,
                DocumentNumber = dto.NumberOfDocument,
                Sum = dto.Sum
            };
        }
    }
}
