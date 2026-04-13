using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.SpsV2.Dto.Rubrics;

namespace Moedelo.SpsV2.Client.Rubrics
{
    public interface IRubricApiClient
    {
        Task<RubricInfoDto> GetRubricsInfo(RubricInfoRequestDto request);

        Task<List<RubricPathDto>> GetParentRubrics(RubricInfoRequestDto request);
    }
}
