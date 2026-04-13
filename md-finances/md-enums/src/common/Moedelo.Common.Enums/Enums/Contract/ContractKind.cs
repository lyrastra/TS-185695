namespace Moedelo.Common.Enums.Enums.Contract
{
    /// <summary>Вид договора</summary>
    public enum ContractKind
    {
        /// <summary>Обычный (без вида)</summary>
        None = 0,

        /// <summary>Полученный кредит</summary>
        ReceivedCredit = 1,

        /// <summary>Полученный займ</summary>
        ReceivedLoan = 2,

        /// <summary>Посреднический</summary>
        Mediation = 3,

        /// <summary>Договор аренды</summary>
        Rent = 4,

        /// <summary>Выданный займ</summary>
        OutgoingLoan = 5
    }
}