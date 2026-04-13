using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto.EdsRequestTasks
{
    public class EdsRequestTaskResponseDto
    {
        public int TotalCount { get; set; }
        
        public List<EdsRequestTaskInfoDto> List { get; set; }
    }
}