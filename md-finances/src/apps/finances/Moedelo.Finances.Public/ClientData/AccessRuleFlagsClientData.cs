namespace Moedelo.Finances.Public.ClientData.Setup
{
    public class AccessRuleFlagsClientData
    {
        public bool HasAccessToEditCurrencyOperations { get; set; }

        public bool HasAccessToPostings { get; set; }

        public bool HasAccessToMoneyEdit { get; set; }

        public bool HasAccessToViewNoAutoDeleteOperation { get; set; }
    }
}
