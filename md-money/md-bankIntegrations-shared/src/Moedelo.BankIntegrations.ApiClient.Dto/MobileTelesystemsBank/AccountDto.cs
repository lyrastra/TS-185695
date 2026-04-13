using Moedelo.BankIntegrations.Dto;

namespace Moedelo.BankIntegrations.ApiClient.Dto.MobileTelesystemsBank
{
    public class AccountDto: BaseResponseDto
    {
        public string AccountNumber { get; set; }

        public string RestAmount { get; set; }

        public string Status { get; set; }

        public string Currency { get; set; }

        public string CustomerName { get; set; }

        public string CustomerINN { get; set; }

        public string CustomerKPP { get; set; }

        public string CustomerBankBIC { get; set; }

        public string CustomerBankName { get; set; }

        public string CustomerBankAccount { get; set; }
    }
}
