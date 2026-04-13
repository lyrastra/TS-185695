using System;
using Moedelo.OurPartners.Dto.Enums;

namespace Moedelo.OurPartners.Dto.Promotions
{
    public class PromotionDto
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Признак, опубликована ли акция
        /// </summary>
        public bool IsPublic { get; set; }
        
        /// <summary>
        /// Название акции
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Признак, что акция не имеет сроков
        /// </summary>
        public bool IsUnlimited { get; set; }

        /// <summary>
        /// Дата начала акции
        /// </summary>
        public DateTime? StartDate { get; set; }
        
        /// <summary>
        /// Дата окончания акции
        /// </summary>
        public DateTime? EndDate { get; set; }
        
        /// <summary>
        /// Название организации
        /// </summary>
        public string CompanyName { get; set; }
        
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Место проведения
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Тип промо контента
        /// </summary>
        public PromotionLinkType PromotionLinkType { get; set; }
        
        /// <summary>
        /// Ссылка на акцию
        /// </summary>
        public string PromotionUrl { get; set; }
        
        /// <summary>
        /// Промокод
        /// </summary>
        public string PromotionCode { get; set; }

        /// <summary>
        /// Признак, что организатором является партнер
        /// </summary>
        public bool IsPartner { get; set; }
        
        /// <summary>
        /// Числовой идентификатор фирмы инициатора акции
        /// </summary>
        public int? FirmId { get; set; }

        /// <summary>
        /// Признак, что контактная информация об организаторе в виде ссылки
        /// </summary>
        public bool IsLink { get; set; }
        
        /// <summary>
        /// Контактная информация об организаторе
        /// </summary>
        public string ContactInformation { get; set; }
        
        /// <summary>
        /// Ссылка на страницу орагнизатора
        /// </summary>
        public string CompanyUrl { get; set; }
        
        /// <summary>
        /// Текст, отображаемый как ссылка на страницу орагнизатора
        /// </summary>
        public string CompanyShowUrl { get; set; }
        
        /// <summary>
        /// Ссылка на баннер
        /// </summary>
        public string BannerUrl { get; set; }
        
        /// <summary>
        /// Ссылка на логотип
        /// </summary>
        public string LogoUrl { get; set; }
    }
}