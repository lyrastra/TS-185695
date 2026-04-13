using Moedelo.Address.Enums;

namespace Moedelo.Address.ApiClient.Abstractions.Address.Dto
{
    public class AddressSaveDto
    {
        /// <summary> Тип территориального деления </summary>
        public DivisionType Division { get; set; }

        /// <summary> Адресные объекты (части) </summary>
        public AddressPartSaveDto[] Parts { get; set; }

        /// <summary> Здание/сооружение </summary>
        public BuildingDto Building { get; set; }

        /// <summary> Название квартиры (квартира, комната, офис и т.п.) </summary>
        public string FlatName { get; set; }
        /// <summary> Номер квартиры </summary>
        public string Flat { get; set; }

        /// <summary> Почтовый индекс </summary>
        public string PostIndex { get; set; }

        /// <summary> ОКТМО </summary>
        public string Oktmo { get; set; }

        /// <summary> Адрес указан вручную  </summary>
        public bool IsManual { get; set; }

        /// <summary> Формализованная строка </summary>
        public string RawAddress { get; set; }

        /// <summary>  Поле "Дополнительно" </summary>
        public string AdditionalInfo { get; set; }
    }
}
