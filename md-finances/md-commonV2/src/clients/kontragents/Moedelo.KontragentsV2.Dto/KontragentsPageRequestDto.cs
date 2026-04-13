namespace Moedelo.KontragentsV2.Dto
{
    public class KontragentsPageRequestDto
    {
        public uint PageNumber { get; set; } = 1;
        public uint PageSize { get; set; } = 50;
        public string Inn { get; set; }
        /// <summary>
        /// при запросе будет заменено на %Name%
        /// </summary>
        public string Name { get; set; }
        public bool IsKppRequired { get; set; } = false;
    }
}