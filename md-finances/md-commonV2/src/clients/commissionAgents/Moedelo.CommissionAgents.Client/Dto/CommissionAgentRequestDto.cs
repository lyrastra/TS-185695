namespace Moedelo.CommissionAgents.Client.Dto
{
    public class CommissionAgentRequestDto
    {
        public int? KontragentId { get; set; }
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 20;

        public static CommissionAgentRequestDto Unlimited => new CommissionAgentRequestDto
        {
            Offset = 0,
            Limit = int.MaxValue
        };
    }
}
