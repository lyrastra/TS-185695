namespace Moedelo.OutSystemsIntegrationV2.Dto.FnsExcerpt.UlModel.Base
{
    public class EgrUlGrnDateBaseInfo
    {
        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ записи, содержащей указанные сведения
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("ГРНДата")]
        public EgrUlGrnDateInfo EgrUlGrnDate { get; set; }

        /// <summary>
        /// ГРН и дата внесения в ЕГРЮЛ записи об исправлении технической ошибки в указанных сведениях
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("ГРНДатаИспр")]
        public EgrUlGrnDateInfo EgrUlGrnDateCorrection { get; set; }
    }
}
