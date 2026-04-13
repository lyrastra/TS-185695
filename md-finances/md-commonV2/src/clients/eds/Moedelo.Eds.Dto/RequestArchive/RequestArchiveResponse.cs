using System.Collections.Generic;

namespace Moedelo.Eds.Dto.RequestArchive
{
    public class RequestArchiveResponse
    {
        public IReadOnlyCollection<EdsRequestDto> Data { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int TotalCount { get; set; }

        public RequestArchiveResponse()
        {
            Data = new List<EdsRequestDto>();
        }
    }
}