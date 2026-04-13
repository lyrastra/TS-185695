using System;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Сведения о регистрации иностранного ЮЛ в стране происхождения, внесенные в ЕГРЮЛ
    /// </summary>
    public class EgrUlForeignRegUlInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Код страны происхождения
        /// </summary>
        public string Oksm { get; set; }

        /// <summary>
        /// Наименование страны происхождения
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime RegDate { get; set; }

        /// <summary>
        /// Регистрационный номер
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        /// Наименование регистрирующего органа
        /// </summary>
        public string RegOrgName { get; set; }

        /// <summary>
        /// Адрес (место нахождения) в стране происхождения
        /// </summary>
        public string Address { get; set; }
    }
}
