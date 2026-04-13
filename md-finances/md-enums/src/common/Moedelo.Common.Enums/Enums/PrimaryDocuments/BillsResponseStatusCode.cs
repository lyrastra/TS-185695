namespace Moedelo.Common.Enums.Enums.PrimaryDocuments
{
    public enum BillsResponseStatusCode
    {
        Ok = 0,
        BillNotFound = 1,
        BillIsRelatedWithDocuments = 2,
        BillMustHaveKontragent = 3,
        KontragentNotFound = 4,
        BillContainsNoItems = 5,
        BillItemWrongType = 6,
        BillWrongType = 7,
        BillMustHaveSettlementAccount = 8,
        SettlementAccountNotFound = 9,
        ProjectNotFound = 10
    }
}
