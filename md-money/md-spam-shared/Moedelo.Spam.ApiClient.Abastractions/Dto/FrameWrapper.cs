using System.Collections.Generic;

namespace Moedelo.Spam.ApiClient.Abastractions.Dto
{
    public class FrameWrapper<T>
    {
        public IReadOnlyCollection<T> Data { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }

        public int TotalCount { get; set; }
    }
}