using Moedelo.Address.ApiClient.Abstractions.Address.Dto;

namespace Moedelo.Address.ApiClient.Abstractions.Suggestion.Dto
{
    public class SuggestionAddressDto
    {
        /// <summary>
        /// Адресные объекты
        /// </summary>
        public AddressPartDto[] Parts { get; set; }

        /// <summary>
        /// Здание/сооружение
        /// </summary>
        public BuildingDto Building { get; set; }

        /// <summary>
        /// ОКТМО
        /// </summary>
        public string Oktmo { get; set; }

        /// <summary>
        /// Почтовый индекс
        /// </summary>
        public string PostIndex { get; set; }

        /// <summary>
        /// ИФНС для юр. лица
        /// </summary>
        public string FnsCodeForOoo { get; set; }

        /// <summary>
        /// ИФНС для физ. лица
        /// </summary>
        public string FnsCodeForIp { get; set; }

        /// <summary>
        /// Полный адрес
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Код региона
        /// </summary>
        public string RegionCode { get; set; }
    }
}
