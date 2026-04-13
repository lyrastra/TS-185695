namespace Moedelo.Contracts.Enums
{
    // изначально повторяет значения PrimaryDocumentsTransferDirection
    public enum ContractDirection
    {
        Default = 0,
        /// <summary> Исходящие документы </summary>
        Outgoing = 1,
        /// <summary> Входящие документы </summary>
        Incoming = 2
    }
}