using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Payroll
{
    public enum WorkerForeignerStatus
    {
        [Description("Неиностранец")]
        Default = 0,

        /// <summary>
        /// Временно пребывающие без страхования
        /// </summary>
        [Description("Временно пребывающий")]
        StayingTemporarily = 1,

        /// <summary>
        /// Временно пребывающие со страхованием
        /// </summary>
        [Description("Временно пребывающий")]
        StayingTemporarilyWithInsurance = 2,

        /// <summary>
        /// Временно проживающие
        /// </summary>
        [Description("Временно проживающий")] 
        LivingTemporarily = 3,

        /// <summary>
        /// Постоянно проживающие
        /// </summary>
        [Description("Постоянно проживающий")]
        PermanentResident = 4,

        /// <summary>
        /// Беженец
        /// </summary>
        [Description("Беженец")]
        Refugee = 5,

        /// <summary>
        /// Временное убежище в России
        /// </summary>
        [Description("Получено временное убежище в России")]
        TemporarilyRefugee = 6,
    }
}
