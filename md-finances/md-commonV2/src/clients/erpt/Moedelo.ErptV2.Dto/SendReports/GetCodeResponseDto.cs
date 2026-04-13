using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto.SendReports
{
    public class GetCodeResponseDto
    {
        public IReadOnlyCollection<int> Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
