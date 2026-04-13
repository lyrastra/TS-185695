namespace Moedelo.BankIntegrations.ApiClient.Dto.RepeatHandler
{
    public class RepeatEventDto
    {
        public int Id { get; set; }

        public string Data { get; set; }

        public int RetryCount { get; set; }
    }
}
