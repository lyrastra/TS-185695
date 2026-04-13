namespace Moedelo.Finances.ApiClient.Abstractions.Legacy.Integration.Dto
{
    public class SendPaymentOrdersResponseDto
    {
        public int StatusCode { get; set; }
        public int? ErrorCode { get; set; }
        public string PhoneMask { get; set; }
        public string Message { get; set; }

        public SendPaymentOrderResponseDto[] List { get; set; }
    }
}
