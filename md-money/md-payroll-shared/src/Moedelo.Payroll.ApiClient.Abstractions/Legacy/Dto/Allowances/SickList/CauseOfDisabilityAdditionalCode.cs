using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    /// <summary>
    /// Причина нетрудоспособности. Дополнительный код
    /// </summary>
    public enum CauseOfDisabilityAdditionalCode : byte
    {
        [Description("017 - лечение туберкулеза, когда санаторно-курортное лечение заменяет оказание медицинской помощи в стационарных условиях")]
        Code017 = 17,

        [Description("018 - медицинская реабилитации в связи с несчастным случаем на производстве в период временной нетрудоспособности (до направления на МСЭ)")]
        Code018 = 18,

        [Description("019 - направление на долечивание больных туберкулезом в санаторнокурортную организацию")]
        Code019 = 19,

        [Description("020 - дополнительный отпуск по беременности и родам")]
        Code020 = 20,

        [Description("021 - заболевание или травма, наступившие вследствие алкогольного, наркотического, токсического опьянения или действий, связанных с таким опьянением")]
        Code021 = 21
    }
    
    public static class CauseOfDisabilityAdditionalCodeExtensions
    {
        public static bool IsTuberculosis(this CauseOfDisabilityAdditionalCode? code)
        {
            return code.HasValue && (code.Value == CauseOfDisabilityAdditionalCode.Code017 ||
                                     code.Value == CauseOfDisabilityAdditionalCode.Code018);
        }
    }
    
    
}