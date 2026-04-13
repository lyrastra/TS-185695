using Moedelo.Common.Enums.Enums.Sps;

namespace Moedelo.SpsV2.Dto.Populars
{
    public class GetPopularListRequestDto
    {
        public PopularType Type { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }
    }
}