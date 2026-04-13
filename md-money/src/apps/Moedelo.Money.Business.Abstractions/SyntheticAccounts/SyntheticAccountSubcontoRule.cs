using Moedelo.AccPostings.Enums;

namespace Moedelo.Money.Business.Abstractions.SyntheticAccounts;

public class SyntheticAccountSubcontoRule
{
    public SubcontoType SubcontoType { get; set; }
    public int Level { get; set; }
}