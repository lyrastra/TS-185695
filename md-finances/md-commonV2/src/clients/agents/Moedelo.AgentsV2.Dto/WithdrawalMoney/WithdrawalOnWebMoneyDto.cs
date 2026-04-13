using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.AgentsV2.Dto.Enums;

namespace Moedelo.AgentsV2.Dto.WithdrawalMoney
{
    /// <summary> Запрос на вывод денег партнёром на WebMoney. </summary>
    public class WithdrawalOnWebMoneyDto
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public int PartnerId { get; set; }

        public decimal Amount { get; set; }

        public string PersonalWalletNumber { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UserId { get; set; }

        public DateTime? ConfirmationDate { get; set; }

        public RequestForWithdrawalType Type { get; set; }

        public string Description { get; set; }

        public PaymentOperation PaymentOperationId { get; set; }
    }
}
