using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Integration
{
    public enum MovementTransferTypesForImport1C
    {
        /// <summary> Не определено</summary>
        [Description("Не определено")]
        NotDefined = -1,

        /// <summary> Перевод с р/с на р/сч </summary>
        [Description("Перевод с р/с на р/сч")]
        MovementFromSettlementToSettlement,

        /// <summary> Перевод с р/с в кассу </summary>
        [Description("Перевод с р/с в кассу")]
        MovementFromSettlementToCash,

        /// <summary> Перевод с кассы на р/сч </summary>
        [Description("Перевод с кассы на р/сч")]
        MovementFromCashToSettlement
    }
}
