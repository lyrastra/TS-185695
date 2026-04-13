namespace Moedelo.CommonV2.Auth.Mobile
{
    public class OAuthTicket
    {
        public string access_token { get; set; }
        public long expires_in { get; set; }
        public string refresh_token { get; set; }
    }
}