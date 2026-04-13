using System;
using Moedelo.AgentsV2.Dto.Enums;

namespace Moedelo.AgentsV2.Dto.WithdrawalMoney
{
    public class WithdrawalOnSettlementAccountDto
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public int PartnerId { get; set; }

        public decimal Amount { get; set; }

        public int? BankId { get; set; }

        public string BankName { get; set; }

        public string BankBik { get; set; }

        public string SettlementAccount { get; set; }

        public string CorrespondentAccount { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UserId { get; set; }

        public DateTime? ConfirmationDate { get; set; }

        public RequestForWithdrawalType Type { get; set; }

        public string Description { get; set; }

        public PaymentOperation PaymentOperationId { get; set; }
    }
}
