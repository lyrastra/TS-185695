
namespace Moedelo.BankIntegrations.Enums.BssSso
{
    //1 — УСН 
    //2 — ОСНО 
    //3 — ЕНВД 
    //4 — УСН + ЕНВД 
    //5 — ОСНО + ЕНВД
    //7 — УСН + ПАТЕНТ
    //8 — УСН + ЕНВД + ПАТЕНТ
    public enum TaxationSystem
    {
        None = 0,
        Usn = 1,
        Osno = 2,
        Envd = 3,
        UsnEnvd = 4,
        OsnoEnvd = 5,
        UsnPatent = 7,
        UsnEnvdPanent=8
    }
}




