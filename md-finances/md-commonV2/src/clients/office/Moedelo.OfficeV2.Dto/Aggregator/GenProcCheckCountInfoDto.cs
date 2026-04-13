namespace Moedelo.OfficeV2.Dto.Aggregator
{
    public class GenProcCheckCountInfoDto
    {
        /// <summary>
        /// общее количество проверок
        /// </summary>
        public int CheckCount { get; set; }

        /// <summary>
        /// количество проверяющих органов
        /// </summary>
        public int InspectionBodyCount { get; set; }

        /// <summary>
        /// количество проверяемых адресов
        /// </summary>
        public int AddressCount { get; set; } 
    }
}