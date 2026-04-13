namespace Moedelo.Accounting.Enums.Documents
{
    public enum BillStatus
    {
        // Legacy значения
        Default = 0,
        OnSigning = 1,
        Signed = 2,
        Client = 3,

        // Реально возможные значения
        NotPayed = 4,
        PartialPayed = 5,
        Payed = 6
    }
}