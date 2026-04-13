using Moedelo.Konragents.Enums.Adress;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos.Address
{
    public class KontragentAddressDto
    {
        /// <summary> Тип адреса (юридический, фактический и т.д.) </summary>
        public AddressType AddressType { get; set; }

        /// <summary> Тип территориального деления </summary>
        public DivisionType Division { get; set; }

        /// <summary> Адресные объекты (части) </summary>
        public KontragentAddressAddressPartDto[] Parts { get; set; }

        /// <summary> Здание/сооружение </summary>
        public KontragentAddressBuildingDto Building { get; set; }

        /// <summary> Название квартиры (квартира, комната, офис и т.п.) </summary>
        public string FlatName { get; set; }

        /// <summary> Номер квартиры </summary>
        public string Flat { get; set; }

        /// <summary> Почтовый индекс </summary>
        public string PostIndex { get; set; }

        /// <summary> Код региона </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// Адрес указан вручную
        /// </summary>
        public bool IsManual { get; set; }
    }
}
