using System.ComponentModel;

namespace Moedelo.BankIntegrations.Enums
{
    public enum OperationType
    {
        [Description("Неизвестная операция")]
        UnknownOperation = 0,
        [Description("Поступление средств")]
        IncomeOperation = 1,
        [Description("Списание средств")]
        OutcomeOperation = 2,
        [Description("Движение средств")]
        MoveOperation = 3
    }
}
