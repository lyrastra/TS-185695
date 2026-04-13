namespace Moedelo.Finances.Domain.Models
{
    public class AccessRuleFlags
    {
        public bool HasAccessToEditCurrencyOperations { get; set; }

        public bool HasAccessToPostings { get; set; }

        public bool HasAccessToMoneyEdit { get; set; }

        public bool HasAccessToViewNoAutoDeleteOperation { get; set; }
    }
}
