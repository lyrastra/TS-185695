using System.Collections.Generic;

namespace Moedelo.Eds.Dto.RequestArchive
{
    public class UpdateRequestDto
    {
        public List<EdsRequestDto> RowsForUpdate { get; set; }
        public RequestCriteria Request { get; set; }
        public string PerformBy { get; set; }
    }
}