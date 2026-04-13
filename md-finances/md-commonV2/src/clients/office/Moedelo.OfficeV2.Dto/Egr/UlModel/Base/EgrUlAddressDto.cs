namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Base
{
    public class EgrUlAddressDto : EgrUlGrnDateBaseInfoDto
    {
        /// <summary>
        /// Уникальный идентификатор адресного объекта в ГАР
        /// </summary>
        public string GarId { get; set; }

        public string Index { get; set; }

        public string RegionCode { get; set; }

        public string KladrCode { get; set; }

        public string Home { get; set; }

        public string Housing { get; set; }

        public string Office { get; set; }

        public EgrUlRegionInfoDto RegionInfo { get; set; }

        public EgrUlAreaInfoDto Area { get; set; }

        public EgrUlCityInfoDto City { get; set; }

        public EgrUlLocalityInfoDto Locality { get; set; }

        public EgrUlStreetInfoDto Street { get; set; }
    }
}