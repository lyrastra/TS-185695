namespace Moedelo.Address.ApiClient.Abstractions.legacy.Dto.Autocomplete
{
    public class AddressParamsResponse
    {
        /// <summary> Почтовый индекс </summary>
        public string PostIndex { get; set; }

        /// <summary> ОКТМО </summary>
        public string Oktmo { get; set; }

        /// <summary> ОКАТО </summary>
        public string Okato { get; set; }

        /// <summary> Код ФНС </summary>
        public string FnsCode { get; set; }
    }
}
