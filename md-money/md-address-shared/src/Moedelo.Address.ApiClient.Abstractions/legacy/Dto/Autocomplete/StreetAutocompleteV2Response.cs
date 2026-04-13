using System;

namespace Moedelo.Address.ApiClient.Abstractions.legacy.Dto.Autocomplete
{
    public class StreetAutocompleteV2Response
    {
        /// <summary>
        /// Полное название улицы, например ул Лениногорская
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Guid улицы
        /// </summary>
        public Guid Guid { get; set; }
    }
}
