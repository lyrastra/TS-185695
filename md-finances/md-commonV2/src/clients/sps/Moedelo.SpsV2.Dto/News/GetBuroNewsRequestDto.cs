namespace Moedelo.SpsV2.Dto.News
{
    public class GetBuroNewsRequestDto
    {
        public int UserId { get; set; }

        public int Take { get; set; }

        public int Skip { get; set; }
    }
}
