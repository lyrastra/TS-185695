namespace Moedelo.CatalogV2.Dto.MunicipalUnit
{
    /// <summary>
    /// Муниципальные районы: муниципальный округ Беговой, Белинский муниципальный район
    /// </summary>
    public class MunicipalUnitDto
    {
        /// <summary>
        /// ОКТМО
        /// </summary>
        public string Oktmo { get; set; }

        /// <summary>
        /// наименование района/округа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// тип муниципального района: городское поселение, сельское поселение, городской округ, муниципальный район
        /// </summary>
        public string Type { get; set; }
    }
}