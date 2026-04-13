using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о регистрации юридического лица в качестве страхователя в территориальном органе Пенсионного фонда Российской Федерации
    /// </summary>
    public class EgrUlPFRegistrationInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Сведения о территориальном органе Пенсионного фонда Российской Федерации
        /// </summary>
        public EgrUlPfInfoDto PfInfo { get; set; }

        /// <summary>
        /// Регистрационный номер в территориальном органе Пенсионного фонда Российской Федерации
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// Дата регистрации юридического лица в качестве страхователя
        /// </summary>
        public DateTime RegDate { get; set; }
    }
}