namespace Moedelo.OfficeV2.Dto.Egr.IpModel.Base
{
    /// <summary>
    /// Сведения об адресе в РФ, внесенные в ЕГРЮЛ
    /// </summary>
    public class EgrIpAddressDto
    {
        /// <summary>
        /// Индекс
        /// </summary>
        public string Index { get; set; }
        
        /// <summary>
        /// Код субъекта Российской Федерации
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// Код адреса по КЛАДР
        /// </summary>
        public string KladrCode { get; set; }

        /// <summary>
        /// Дом (владение и т.п.)
        /// </summary>
        public string Home { get; set; }

        /// <summary>
        /// Корпус (строение и т.п.)
        /// </summary>
        public string Housing { get; set; }

        /// <summary>
        /// Квартира (офис и т.п.)
        /// </summary>
        public string Office { get; set; }

        /// <summary>
        /// Субъект Российской Федерации
        /// </summary>
        public EgrIpRegionInfoDto RegionInfo { get; set; }

        /// <summary>
        /// Район (улус и т.п.)
        /// </summary>
        public EgrIpAreaInfoDto Area { get; set; }

        /// <summary>
        /// Город (волость и т.п.)
        /// </summary>
        public EgrIpCityInfoDto City { get; set; }

        /// <summary>
        /// Населенный пункт (село и т.п.)
        /// </summary>
        public EgrIpLocalityInfoDto Locality { get; set; }

        /// <summary>
        /// Улица (проспект, переулок и т.д.)
        /// </summary>
        public EgrIpStreetInfoDto Street { get; set; }
    }
}
