using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.SpsV2.Dto.Documents;

namespace Moedelo.SpsV2.Client.Documents
{
    public interface IDocumentApiClient
    {
        Task<LandingQaInfoDto> GetLandingQaInfo(QaInfoRequestDto request);

        Task<LandingFormInfoDto> GetLandingFormInfo(DocIdDto request);

        Task<List<CommonPropertyCustomValueDto>> GetCustomValues(CustomValuesRequestDto request);

        Task<List<CommonPropertyDto>> GetCommonProperties();

        Task<string> GetXmlContent(DocIdDto request);

        Task<string> GetFormHtml(DocIdDto request);

        Task<FormTemplateDto> GetFormTemplate(DocIdDto request);
    }
}
