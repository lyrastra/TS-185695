using Moedelo.OfficeV2.Dto.Egr.UlModel.Base;

namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о лице, через которое иностранное юридическое лицо осуществляет полномочия управляющей организации
    /// </summary>
    public class EgrUlRepresentativeInfoDto
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
        /// Сведения о контактном телефоне ФЛ
        /// </summary>
        public EgrUlPhoneInfoDto Phone { get; set; }

        /// <summary>
        /// Сведения о рождении ФЛ
        /// </summary>
        public EgrUlFlBirthInfoDto FlBirthInfo { get; set; }

        /// <summary>
        /// Сведения о документе, удостоверяющем личность
        /// </summary>
        public EgrUlIdentityDocumentInfoDto IdentityDoc { get; set; }
    }
}