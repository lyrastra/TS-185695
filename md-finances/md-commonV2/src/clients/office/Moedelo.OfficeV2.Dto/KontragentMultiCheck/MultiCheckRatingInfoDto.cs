using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Kontragents;

namespace Moedelo.OfficeV2.Dto.KontragentMultiCheck
{
    public class MultiCheckRatingInfoDto
    {
        public int Rating { get; set; }

        public KontragentRatingType RatingType { get; set; }

        public List<MultiCheckRatingRecommendationInfoDto> RecommendationList { get; set; }

        public bool SomeChecksSkipped { get; set; }
    }
}