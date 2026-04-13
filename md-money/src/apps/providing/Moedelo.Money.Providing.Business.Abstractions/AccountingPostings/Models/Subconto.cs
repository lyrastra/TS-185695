using Moedelo.AccPostings.Enums;

namespace Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models
{
    public class Subconto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public SubcontoType Type { get; set; }

        public static Subconto OtherIncomeOrOutgo =>
            new Subconto
            {
                Id = 1,
                Name = "Прочие доходы и расходы",
                Type = SubcontoType.OtherIncomeOrOutgo
            };
    }
}
