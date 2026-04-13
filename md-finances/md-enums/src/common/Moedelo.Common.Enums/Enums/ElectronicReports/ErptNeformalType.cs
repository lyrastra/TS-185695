namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    /// <summary>
    /// Типы отчетов неформализованной отчетности и иона
    /// </summary>
    public enum ErptNeformalType
    {
        /// <summary>Задолженность по налогам (IonType.Debt=1)</summary>
        // IonTaxDebt = 2,

        /// <summary>Выписка по расчетам за год</summary>
        // IonExtract = 3,

        /// <summary>Акт сверки (IonType.Declarations=4)</summary>
        IonActCheck = 4,

        /// <summary>Список деклараций (IonType.Declarations=3)</summary>
        IonDeclarationsList = 5,

        /// <summary>Справка о налогах (IonType.Declarations=5)</summary>
        IonTaxNote = 6,
        
        /// <summary>Расчеты по ЕНП (IonType.Declarations=6)</summary>
        IonEnpCalculationNote = 7,
        
        /// <summary>Задолженность по ЕНП (IonType.Declarations=7)</summary>
        IonEnpDebt = 8,
        
        /// <summary> Справка расчетов по ЕНП. IonType.EnpCalculationStatement </summary>
        IonEnpCalculationStatement = 9,
        
        /// <summary> Справка о задолженности. IonType.DebtStatement </summary>
        IonDebtStatement = 10
    }
}
