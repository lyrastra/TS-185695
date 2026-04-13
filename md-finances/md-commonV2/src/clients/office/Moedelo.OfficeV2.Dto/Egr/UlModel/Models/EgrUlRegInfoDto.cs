using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    public class EgrUlRegInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Способ образования юридического лица
        /// </summary>
        public EgrUlMethodFormingInfoDto MethodForming { get; set; }

        /// <summary>
        /// Основной государственный регистрационный номер юридического лица
        /// </summary>
        public string Ogrn { get; set; }
        
        /// <summary>
        /// Дата присвоения ОГРН
        /// </summary>
        public DateTime OgrnDate { get; set; }
        
        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegNumber { get; set; }
        
        /// <summary>
        /// Дата регистрации юридического лица
        /// </summary>
        public DateTime RegDate { get; set; }

        public bool RegDateSpecified { get; set; }
        
        /// <summary>
        /// Наименование органа, зарегистрировавшего юридическое лицо
        /// </summary>
        public string RegAuthorityName { get; set; }
    }
}