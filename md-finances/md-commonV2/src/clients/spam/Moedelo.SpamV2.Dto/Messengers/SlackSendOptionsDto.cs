namespace Moedelo.SpamV2.Dto.Messengers
{
    public class SlackSendOptionsDto
    {
        public string WebHookUrl { get; set; } = SlackWebHooks.MdTest;
        public string Message { get; set; }
    }
}
