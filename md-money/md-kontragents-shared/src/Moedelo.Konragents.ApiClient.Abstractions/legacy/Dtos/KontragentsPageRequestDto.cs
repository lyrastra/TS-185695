namespace Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos
{
    public class KontragentsPageRequestDto
    {
        public uint PageNumber { get; set; }
        public uint PageSize { get; set; }
        public string Inn { get; set; }
        /// <summary>
        /// при запросе будет заменено на %Name%
        /// </summary>
        public string Name { get; set; }
        public bool IsKppRequired { get; set; }
        
        public KontragentsPageRequestDto(string inn, string name, bool isKppRequired = false, uint pageNumber = 1, uint pageSize = 50)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Inn = inn;
            Name = name;
            IsKppRequired = isKppRequired;
        }
    }
}