namespace Moedelo.BankIntegrations.Enums.VostochniybankSso
{
    //1 — УСН 
    //2 — ОСНО 
    //3 — ЕНВД 
    //4 — УСН + ЕНВД 
    //5 — ОСНО + ЕНВД
    public enum TaxationSystem
    {
        None = 0,
        Usn,
        Osno,
        Envd,
        UsnEnvd,
        OsnoEnvd
    }
}




