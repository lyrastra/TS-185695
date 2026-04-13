using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о регистрации юридического лица в качестве страхователя в исполнительном органе Фонда социального страхования Российской Федерации
    /// </summary>
    public class EgrUlFssRegistrationInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Сведения об исполнительном органе Фонда социального страхования Российской Федерации
        /// </summary>
        public EgrUlFssInfoDto Fss { get; set; }

        /// <summary>
        /// Регистрационный номер в исполнительном органе Фонда социального страхования Российской Федерации
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// Дата регистрации юридического лица в качестве страхователя
        /// </summary>
        public DateTime RegDate { get; set; }
    }
}