using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.IntegrationPartnersInfo.Attributes
{
    internal class IntegrationPartnerInfoAttribute : Attribute
    {
        public string PartnerName { get; }
        public string PartnerNameInGenitive { get; }
        public string RegexpNameInDb { get; }
        public string InaccessibleText { get; }
        public string JsFunctionForTurn { get; }
        public bool IsCanBeIntegrated { get; set; }
        /// <summary> Выводится в интерфейс для интеграции с платёжными системами </summary>
        public bool UsePaymentSystemIntegrationUI { get; set; }
        /// <summary> One Time Password </summary>
        public bool IsOtp { get; set; }
        public bool IsBank { get; set; } = true;
        public bool IsIntegrationMonitor { get; set; }
        /// <summary> В разработке </summary>
        public bool InDevelopment { get; set; }
        /// <summary>
        /// На примере с Точкой: банк был до 2023 в Открытии а потом отделился и сменил рег номер. Если рег номер известен заранее то его можно добавить в массив. Старые же записи из таблицы Bank когда ЦБ отдаст станут с IsActive = false
        /// </summary>
        public int[] BankLicenseNumbers { get; set; }
        /// Партнер используется в WL.
        /// </summary>
        public TypeIntegration TypePartner { get; set; }

        public IntegrationPartnerInfoAttribute(
            string partnerName,
            string partnerNameInGenitive,
            string regexpNameInDb,
            string inaccessibleText,
            string jsFunctionForTurn)
        {
            PartnerName = partnerName;
            PartnerNameInGenitive = partnerNameInGenitive;
            RegexpNameInDb = regexpNameInDb;
            InaccessibleText = inaccessibleText;
            JsFunctionForTurn = jsFunctionForTurn;
        }
    }

    /// <summary>
    /// Нужен для сортировки определения партнера по расчетному счету в случае нескольких партнеров для одного банка, например Совкомбанк/СовкомбанкWL
    /// </summary>
    public enum TypeIntegration
    {
        /// <summary>
        /// Интеграция от нас
        /// </summary>
        IsUsedMD = 0,
        /// <summary>
        /// WL Интеграция
        /// </summary>
        IsUsedWL = 1,
        /// <summary>
        /// Партнер используется для WL и для интеграции от нас
        /// </summary>
        IsBoth = 2
    }
}
