using Moedelo.Common.Enums.Enums.Kontragents;

namespace Moedelo.OfficeV2.Dto.KontragentMultiCheck
{
    public class MultiCheckRatingRecommendationInfoDto
    {
        public string Name { get; set; }

        public string Result { get; set; }

        public string Recommendation { get; set; }

        public KontragentRatingType RatingType { get; set; } 
    }
}