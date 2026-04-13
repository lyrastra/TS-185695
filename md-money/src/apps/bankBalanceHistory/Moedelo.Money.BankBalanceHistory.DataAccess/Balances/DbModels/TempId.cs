using System.ComponentModel.DataAnnotations.Schema;

namespace Moedelo.Money.BankBalanceHistory.DataAccess.Balances.DbModels
{
    internal class TempId
    {
        [Column("id")]
        public int Id { get; set; }
    }
}
