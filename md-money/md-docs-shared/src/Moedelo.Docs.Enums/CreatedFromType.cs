namespace Moedelo.Docs.Enums
{
    /// <summary>
    /// sync with: https://github.com/moedelo/md-bookkeeping/blob/master/src/old/Moedelo.Domain/Models/PrimaryDocuments/Enums/CreationPlace.cs#L3 
    /// </summary>
    public enum CreatedFromType
    {
        Default = 0,

        /// <summary>
        /// Из остатков
        /// </summary>
        Balances = 1,

        /// <summary>
        /// Из имущества (пока не используется)
        /// </summary>
        Estate = 2
    }
}