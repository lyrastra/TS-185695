using System.Collections.Generic;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto.Common
{
    public class ApiPageResponseDto<T>
    {
        public ApiPageResponseDto(IReadOnlyCollection<T> data)
        {
            Data = data;
        }

        public IReadOnlyCollection<T> Data { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public int TotalCount { get; set; }
    }
}