using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare
{
    public enum ChildCarePaymentsTerminationOrderType
    {
        [Description("Приказ о преждевременном выходе на работу")]
        PrematureExitToWorkOrder = 1,
        [Description("Приказ о прекращении трудовых отношений")]
        LaborTerminationOrder = 2,
        [Description("Свидетельство о смерти ребенка")]
        DeathCertificateOfChild = 3,
        [Description("Документ о прекращении оснований для выплаты пособия")]
        DocumentTerminationPayment = 4,
        [Description("Иное")]
        Other = 5
    }
}