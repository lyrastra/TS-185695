namespace Moedelo.BankIntegrationsV2.Dto.Import1C
{
    public class Import1CFileResponseDto
    {
        public Import1CFileResponseDto(string msg)
        {
            Msg = msg;
        }

        public bool IsSuccess => string.IsNullOrWhiteSpace(Msg);
        public string Msg { get; set; }
    }
}
