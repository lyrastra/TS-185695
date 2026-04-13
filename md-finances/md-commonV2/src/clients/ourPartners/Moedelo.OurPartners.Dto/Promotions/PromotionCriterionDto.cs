namespace Moedelo.OurPartners.Dto.Promotions
{
    public class PromotionCriterionDto
    {
        public int Offset { get; set; } = 0;

        public int Limit { get; set; } = 20;
        
        /// <summary>
        /// Фильтр - только опубликованные
        /// </summary>
        public bool? IsPublic { get; set; }
        
        /// <summary>
        /// Фильтр - только от партнеров
        /// </summary>
        public bool? IsPartner { get; set; }
    }
}