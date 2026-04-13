using System;
using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о лице, осуществляющем управление долей, переходящей в порядке наследования
    /// </summary>
    public class EgrUlHeirShareInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// Сведения о ФИО и (при наличии) ИНН ФЛ
        /// </summary>
        public EgrUlFlInfoDto FlInfo { get; set; }

        /// <summary>
        /// Сведения о рождении ФЛ
        /// </summary>
        public EgrUlFlBirthInfoDto BirthInfo { get; set; }

        /// <summary>
        /// Сведения о документе, удостоверяющем личность
        /// </summary>
        public EgrUlIdentityDocumentInfoDto IdentityDocument { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства в Российской Федерации
        /// </summary>
        public EgrUlAddressDto HomeAddressRf { get; set; }

        /// <summary>
        /// Сведения об адресе места жительства за пределами территории Российской Федерации
        /// </summary>
        public EgrUlForeignAddressDto HomeAddressForeign { get; set; }

        /// <summary>
        /// Дата открытия наследства (дата смерти участника)
        /// </summary>
        public DateTime OpeningInheritanceDate { get; set; }
    }
}