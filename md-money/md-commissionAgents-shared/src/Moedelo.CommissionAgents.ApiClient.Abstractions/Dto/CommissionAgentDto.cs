namespace Moedelo.CommissionAgents.ApiClient.Abstractions.Dto
{
    public class CommissionAgentDto
    {
        public int Id { get; set; }
        public string Inn { get; set; }
        public string Name { get; set; }
        public int KontragentId { get; set; }
        public long StockId { get; set; }
    }
}
