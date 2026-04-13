namespace Moedelo.Common.Enums.Enums.Billing
{
    // ServiceType для актов получаемых процедурой dbo.GetAccountingActs
    public enum ActServiceType
    {
        Service = 1,
        Consultation = 2,
        OutsourceOrOneTime = 3, // "Аутсорс" или "Разовые услуги без НДС"
        Cash = 4,
        CashService = 5,
        CashActivation = 6,
        Shipping = 7,
        OneTimeWithNds = 8, // "Разовые услуги c НДС"
    }
}
