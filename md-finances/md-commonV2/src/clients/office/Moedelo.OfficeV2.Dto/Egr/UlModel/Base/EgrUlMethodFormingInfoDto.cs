namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Способ образования юридического лица
    /// </summary>
    public class EgrUlMethodFormingInfoDto
    {
        /// <summary>
        /// Код способа образования по справочнику СЮЛНД
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Наименование способа образования юридического лица
        /// </summary>
        public string Name { get; set; }
    }
}
