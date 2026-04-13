namespace Moedelo.Accounts.ApiClient.Enums
{
    public enum LeadMarkType
    {
        Any = -1,

        None = 0,

        Operator = 1,

        Partner = 2,

        // ГлавУчёт
        OutsourcingMdPartner = 3,
        
        // Франчайзи
        Franchisee = 4,

        // ПА, не привязанный к партнёру (профбух и т.п.)
        Account = 5
    }
}