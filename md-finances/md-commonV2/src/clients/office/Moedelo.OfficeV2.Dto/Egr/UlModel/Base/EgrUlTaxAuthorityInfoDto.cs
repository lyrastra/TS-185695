namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Сведения о налоговом органе, в котором юридическое лицо или обособленное подразделение состоит (состояло) на учете
    /// </summary>
    public class EgrUlTaxAuthorityInfoDto
    {
        /// <summary>
        /// Код органа по справочнику СОНО
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Наименование налогового органа
        /// </summary>
        public string Name { get; set; }
    }
}