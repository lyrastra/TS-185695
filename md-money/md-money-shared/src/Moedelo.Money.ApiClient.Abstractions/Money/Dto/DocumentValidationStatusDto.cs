namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto
{
    public class DocumentValidationStatusDto
    {
        public long DocumentBaseId { get; set; }
        /// <summary>
        /// Операция есть в деньгах и в НУ
        /// </summary>
        public bool IsValid { get; set; }
    }
}