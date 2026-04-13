using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.EdsRequest;

namespace Moedelo.ErptV2.Dto.EdsRequest
{
    public class EdsRequestChangesDto
    {
        public List<EdsRequestField> ChangedFields { get; set; }
        public List<AdditionalFnsChangeDto> AdditionalFnsChanges { get; set; }
    }
}