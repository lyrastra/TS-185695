namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Порядок распределения прямых расходов в случае оказания организацией услуг.
    /// </summary>
    public enum AllocationMethodForCostsInCaseOfProvisionOfService
    {
        /// <summary>
        /// В полном объеме относить на уменьшение доходов от производства и реализации без распределения на остатки незавершенки.
        /// </summary>
        AttributedToDecreaseInRevenuesFromProduction = 1,

        /// <summary>
        /// Относятся к расходам текущего периода по мере реализации услуг (распределять на остатки незавершенки).
        /// </summary>
        AsSalesOfServices = 2
    }
}