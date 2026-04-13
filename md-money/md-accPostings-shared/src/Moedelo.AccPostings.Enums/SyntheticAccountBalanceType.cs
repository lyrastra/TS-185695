namespace Moedelo.AccPostings.Enums
{
    public enum SyntheticAccountBalanceType
    {
        Empty = -1,

        /// <summary>
        /// Балансовый счет
        /// </summary>
        InBalance = 0,

        /// <summary>
        /// Забалансовый
        /// </summary>
        OffBalance = 1,
    }
}