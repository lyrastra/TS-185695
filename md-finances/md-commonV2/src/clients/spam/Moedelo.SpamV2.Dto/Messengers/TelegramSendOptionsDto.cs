namespace Moedelo.SpamV2.Dto.Messengers
{
    public class TelegramSendOptionsDto
    {
        public string ChatId { get; set; } = TelegramChats.MdTest;
        public string Message { get; set; }
    }
}
