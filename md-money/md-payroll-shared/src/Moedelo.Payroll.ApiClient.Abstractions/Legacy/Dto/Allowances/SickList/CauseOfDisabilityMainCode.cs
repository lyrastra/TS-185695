using System.ComponentModel;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList
{
    /// <summary>
    /// Причина нетрудоспособности. Основной код
    /// </summary>
    public enum CauseOfDisabilityMainCode : byte
    {
        [Description("01 - заболевание")] 
        Code01 = 1,
        
        [Description("02 - травма")] 
        Code02 = 2,
        
        [Description("03 - карантин")] 
        Code03 = 3,

        [Description("04 - несчастный случай на производстве или его последствия")]
        Code04 = 4,

        [Description("06 - протезирование в стационаре")]
        Code06 = 6,

        [Description("07 - профессиональное заболевание или его обострение")]
        Code07 = 7,

        [Description("08 - долечивание в санатории")]
        Code08 = 8,

        [Description("09 - уход за больным членом семьи")]
        Code09 = 9,

        [Description("10 - иное состояние (отравление, проведение манипуляций и др.)")]
        Code10 = 10,

        [Description("11 - заболевание туберкулезом")]
        Code11 = 11,

        [Description(
            "12 - в случае заболевания ребенка, включенного в перечень заболеваний определяемых Минздравсоцразвития России")]
        Code12 = 12,
        
        [Description("13 - ребенок-инвалид")] 
        Code13 = 13,

        [Description("14 - поствакцинальное осложнение или злокачественное новообразование у ребенка")]
        Code14 = 14,

        [Description("15 - ВИЧ-инфицированный ребенок")]
        Code15 = 15
    }
}
