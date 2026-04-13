using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о регистрации учредителя (участника) до 01.07.2002 г
    /// </summary>
    public class EgrUlOldRegInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Регистрационный номер, присвоенный юридическому лицу до 1 июля 2002 года
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// Дата регистрации юридического лица до 1 июля 2002 года
        /// </summary>
        public DateTime RegDate { get; set; }

        public bool RegDateSpecified { get; set; }

        /// <summary>
        /// Наименование органа, зарегистрировавшего юридическое лицо до 1 июля 2002 года
        /// </summary>
        public string RegAuthorityName { get; set; }
    }
}