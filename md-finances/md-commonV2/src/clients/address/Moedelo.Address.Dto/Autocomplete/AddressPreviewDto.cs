using System.Collections.Generic;
using Moedelo.Address.Dto.Address;

namespace Moedelo.Address.Dto.Autocomplete
{
    public class AddressPreviewDto
    {
        public List<AddressObjectDto> AddressObjects { get; set; }

        /// <summary> Название дома (дом, здание, владение и т.п.) </summary>
        public string HouseName { get; set; }
        /// <summary> Номер дома </summary>
        public string House { get; set; }
        /// <summary> Название строения (корпус, строение, сооружение и т.п.) </summary>
        public string BuildingName { get; set; }
        /// <summary> Номер строения </summary>
        public string Building { get; set; }
        /// <summary> Название квартиры (квартира, комната, офис и т.п.) </summary>
        public string FlatName { get; set; }
        /// <summary> Номер квартиры </summary>
        public string Flat { get; set; }
        /// <summary> Почтовый индекс </summary>
        public string PostIndex { get; set; }
        /// <summary> Код региона </summary>
        public string RegionCode { get; set; }
        /// <summary> КЛАДР </summary>
        public string Code { get; set; }
        public string Okato { get; set; }
        public string Oktmo { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
