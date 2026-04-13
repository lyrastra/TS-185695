using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об управляющей организации
    /// </summary>
    public class EgrUlManagementCompanyInfoDto
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ сведений о данном лице
        /// </summary>
        public EgrUlGrnDateInfoDto Grn { get; set; }

        /// <summary>
        /// Сведения о наименовании и (при наличии) ОГРН и ИНН ЮЛ - управляющей организации
        /// </summary>
        public EgrUlBaseInfoDto Name { get; set; }

        /// <summary>
        /// Сведения о регистрации в стране происхождения
        /// </summary>
        public EgrUlForeignRegUlInfoDto ForeignRegUlInfo { get; set; }

        /// <summary>
        /// Сведения о наименовании представительства или филиала в Российской Федерации, через которое иностранное ЮЛ осуществляет полномочия управляющей организации
        /// </summary>
        public EgrUlForeignRepresentationInfoDto ForeignRepresentation { get; set; }

        /// <summary>
        /// Сведения об адресе управляющей организации в Российской Федерации
        /// </summary>
        public EgrUlAddressDto RfAddress { get; set; }

        /// <summary>
        /// Сведения о контактном телефоне
        /// </summary>
        public EgrUlPhoneInfoDto Phone { get; set; }

        /// <summary>
        /// Сведения о лице, через которое иностранное юридическое лицо осуществляет полномочия управляющей организации
        /// </summary>
        public EgrUlRepresentativeInfoDto Representative { get; set; }
    }
}