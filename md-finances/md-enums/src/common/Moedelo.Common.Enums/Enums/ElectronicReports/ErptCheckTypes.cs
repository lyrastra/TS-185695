using System;

namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum ErptCheckTypes
    {
        None = 0,
        
        /// <summary>Акт сверки. ErptNeformalType.IonActCheck</summary>
        Act = 1,
        
        /// <summary>Список Деклараций. ErptNeformalType.IonDeclarationsList</summary>
        CheckList = 2,
        
        /// <summary>Выписка по расчетам за год (Выписка). ErptNeformalType.IonExtract</summary>
        [Obsolete]
        Billing = 3,
        
        /// <summary>Задолженность по налогам. ErptNeformalType.IonTaxDebt</summary>
        [Obsolete]
        Reference = 4,
        
        /// <summary>Справка о налогах. ErptNeformalType.IonTaxNote</summary>
        ReferenceTax = 5,
        
        /// <summary>Расчеты по ЕНП. ErptNeformalType.IonEnpCalculationNote</summary>
        EnpCalculationNote = 6,
        
        /// <summary>Задолженность по ЕНП. ErptNeformalType.IonEnpDebt</summary>
        EnpDebt = 7,
        
        /// <summary> Справка расчетов по ЕНП. ErptNeformalType.IonEnpCalculationStatement </summary>
        EnpCalculationStatement = 8,
        
        /// <summary> Справка о задолженности. ErptNeformalType.IonDebtStatement </summary>
        DebtStatement = 9
        
    }
}
