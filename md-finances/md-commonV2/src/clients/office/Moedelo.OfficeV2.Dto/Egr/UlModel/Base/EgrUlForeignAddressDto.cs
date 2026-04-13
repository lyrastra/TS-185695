namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    /// <summary>
    /// Сведения об адресе в РФ, внесенные в ЕГРЮЛ
    /// </summary>
    public class EgrUlForeignAddressDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Код страны
        /// </summary>
        public string Oksm { get; set; }
        
        /// <summary>
        /// Наименование страны
        /// </summary>
        public string CountryName { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
    }
}
