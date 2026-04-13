namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения о форме реорганизации (статусе) юридического лица
    /// </summary>
    public class EgrUlReorgStatusInfoDto
    {
        /// <summary>
        /// Код формы реорганизации (статуса) юридического лица по справочнику СЮЛСТ
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Наименование формы реорганизации (статуса) юридического лица по справочнику СЮЛСТ
        /// </summary>
        public string Name { get; set; }
    }
}
