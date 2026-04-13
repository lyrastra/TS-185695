namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Сведения о наименовании и (при наличии) ИНН и ОГРН ЮЛ - учредителя (участника), управляющей организации, залогодержателя, управляющего долей участника, внесенные в ЕГРЮЛ
    /// </summary>
    public class EgrUlBaseInfoDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// ИНН юридического лица
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// Основной государственный регистрационный номер юридического лица
        /// </summary>
        public string Ogrn { get; set; }

        /// <summary>
        /// Полное наименование юридического лица
        /// </summary>
        public string FullName { get; set; }
    }
}