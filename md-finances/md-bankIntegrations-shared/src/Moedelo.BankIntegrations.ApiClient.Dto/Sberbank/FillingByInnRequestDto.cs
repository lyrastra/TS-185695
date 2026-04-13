namespace Moedelo.BankIntegrations.ApiClient.Dto.Sberbank
{
    public class FillingByInnRequestDto
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public string Inn { get; set; }
    }
}