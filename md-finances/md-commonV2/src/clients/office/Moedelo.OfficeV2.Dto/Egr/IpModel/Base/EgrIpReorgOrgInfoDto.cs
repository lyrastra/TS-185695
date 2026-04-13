namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Base
{
    /// <summary>
    /// Сведения о регистрирующем органе
    /// </summary>
    public class EgrIpReorgOrgInfoDto
    {
        /// <summary>
        /// Код органа по справочнику СОНО
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Наименование регистрирующего (налогового) органа
        /// </summary>
        public string Name { get; set; }
    }
}
