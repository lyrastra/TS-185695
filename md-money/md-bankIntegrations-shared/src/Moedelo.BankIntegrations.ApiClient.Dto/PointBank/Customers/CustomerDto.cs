namespace Moedelo.BankIntegrations.ApiClient.Dto.PointBank.Customers
{
    public class CustomerDto
    {
        /// <summary> Уникальный код клиента </summary>
        public string CustomerCode { get; set; }

        /// <summary> Тип клиент (физическое или юридическое лицо) </summary>
        public CustomerType Type { get; set; }

        /// <summary> Признак резидента </summary>
        public bool IsResident { get; set; }

        /// <summary> ИНН </summary>
        public string Inn { get; set; }

        /// <summary> Краткое наименование </summary>
        public string ShortName { get; set; }

        /// <summary> Полное наименование </summary>
        public string FullName { get; set; }

        /// <summary> КПП </summary>
        public string Kpp { get; set; }
        
        /// <summary> ОГРН или ОГРНИП </summary>
        public string Ogrn { get; set; }
    }
}
