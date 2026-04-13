using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об учете в налоговом органе
    /// </summary>
    public class EgrUlTaxRegistrationInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Сведения о налоговом органе, в котором юридическое лицо состоит (для ЮЛ, прекративших деятельность - состояло) на учете
        /// </summary>
        public EgrUlTaxAuthorityInfoDto TaxAuthority { get; set; }

        /// <summary>
        /// ИНН юридического лица
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// КПП юридического лица
        /// </summary>
        public string Kpp { get; set; }

        /// <summary>
        /// Дата постановки на учет в налоговом органе
        /// </summary>
        public DateTime RegistrationDate { get; set; }
    }
}