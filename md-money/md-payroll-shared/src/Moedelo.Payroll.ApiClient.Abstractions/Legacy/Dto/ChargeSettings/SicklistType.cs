using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeSettings
{
    public enum SicklistType
    {
        /// <summary>Больничный лист</summary>
        Illness = 0200,

        /// <summary>  Б/Л при направлении на протезирование в стационаре</summary>
        Prosthetics = 0225,

        /// <summary> Б/Л Бытовая травма </summary>
        Trauma = 0205,

        /// <summary>Б/Л Долечивание в сан.-кур. учреждении  </summary>
        SpaTreatment = 0210,

        /// <summary> Б/Л Долечивание в сан.-кур. Учреждении (туберкулез)</summary>
        SpaTreatmentTuberculosis = 0211,

        /// <summary>Больничный при карантине </summary>
        Quarantine = 0286,

        /// <summary>Б/Л по уходу за членом семьи (амбулаторно)  </summary>
        NursingCareAmbulatory = 0235,

        /// <summary>Профзаболевание  </summary>
        ProfIlness = 11,

        /// <summary>Б/Л производственная травма   </summary>
        WorkTrauma = 0290,

        /// <summary>Б/Л произв травма с нарушением режима    </summary>
        WorkTraumaWithBreachRegime = 0296,

        /// <summary>По беременности и родам  (0310	Отпуск по берем.и родам Стар) </summary>
        Pregnancy = 0300,

        /// <summary>Болезнь сотрудника - участника ликвидации аварии на Чернобыльской АЭС  </summary>
        ChernobylAES = 0230,

        /// <summary> Отпуск по уходу за 1 реб. (0504 Уход за 1 реб.)</summary>
        CareForFirstChild = 0500,

        /// <summary> Отпуск по уходу за 2 детьми (0506 Уход за 2 детьми)</summary>
        CareForTwoChild = 0502,

        /// <summary>Отпуск по уходу за 3 детьми до 1,5 л.  (0507	Уход за 3 детьми)</summary>
        CareForThreeChild = 0503,

        /// <summary> Б/Л инвалиду с огранич.спос. к труд. Деят. При туберкулёзе </summary>
        InvalidTuberculosis = 0215,

        /// <summary>Б/Л инвалиду с огранич.спос. к труд. Деятельности  </summary>
        Invalid = 0220,

        /// <summary> Б/Л нарушение режима</summary>
        BreachRegime = 0298,

        /// <summary> Б/Л по уходу за реб.-инв. До 15 лет (амбулаторно) </summary>
        ChildInvalidAmbulatory = 0240,

        /// <summary> Б/Л по уходу за реб.-инв. До 15 лет (стационар)</summary>
        ChildInvalidStationary = 0245,

        /// <summary> Б/Л по уходу за реб. До 15 лет ВИЧ (стационар)</summary>
        ChildHIVStationary = 0250,

        /// <summary> Б/Л по уходу за реб до 15 лет ВИЧ (амбулаторно) </summary>
        ChildHIVAmbulatory = 0251,

        /// <summary> Б/Л по уходу за реб. До 15 лет поствакц. Осл. (амбулаторно) </summary>
        ChildAfterVaccinationAmbulatory = 0255,

        /// <summary> Б/Л по уходу за реб. До 15 лет поствакц. Осл. (стационар) </summary>
        ChildAfterVaccinationStationary = 0260,

        /// <summary> Б/Л по уходу за реб. До 7 лет (амбулаторно)</summary>
        CareChild7Ambulatory = 0265,

        /// <summary> Б/Л по уходу за реб. До 7 лет (стационар) </summary>
        CareChild7Stationary = 0266,

        /// <summary> Б/Л по уходу за реб. До 7 лет вкл. В перечень  (амбулаторно) </summary>
        CareChild7YearAmbulatoryIn = 0270,

        /// <summary> Б/Л по уходу за реб до 7 лет вкл в перечень (стационар)</summary>
        CareChild7YearStationaryIn = 0271,

        /// <summary>Б/Л по уходу за реб. с 7 до 15 лет (амбулаторно) </summary>
        CareChild15YearAmbulatory = 0275,

        /// <summary> Б/Л по уходу за реб. с 7 до 15 лет (стационар)</summary>
        CareChild15YearStationary = 0280,

        /// <summary> Б/Л производственная травма по дороге на работу </summary>
        TraumaByWayOnWork = 0295,

        /// <summary> Больничный лист после увольнения </summary>
        AfterDismissal = 0299,

        OtherState = 203
    }

    public static class SickListTypesExtensions
    {
        private static List<SicklistType> SicklistStationaryTypes = new List<SicklistType>
        {
            SicklistType.CareChild7Stationary,
            SicklistType.CareChild7YearStationaryIn,
            SicklistType.CareChild15YearStationary,
            SicklistType.ChildAfterVaccinationStationary,
            SicklistType.ChildHIVStationary,
            SicklistType.ChildInvalidStationary
        };
        
        public static bool IsStationaryType(this SicklistType sicklistType)
        {
            return SicklistStationaryTypes.Any(x => x == sicklistType);
        }
    }
}