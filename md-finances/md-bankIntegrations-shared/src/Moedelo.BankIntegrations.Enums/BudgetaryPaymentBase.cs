using System;
using System.ComponentModel;
using System.Reflection;

namespace Moedelo.BankIntegrations.Enums
{
    /// <summary>
    /// Показатель основания платежа
    /// </summary>
    public enum BudgetaryPaymentBase
    {
        /// <summary>  Нет основания  </summary>
        None = 0,

        /// <summary>
        /// ТП - Текущий платеж без погашения задолженности (ТП)
        /// </summary>
        [Description("ТП")]
        CurrentPayment = 1,

        /// <summary>
        /// ЗД - Добровольное погашение задолженности по истекшим налоговым периодам (ЗД)
        /// </summary>
        [Description("ЗД")]
        FreeDebtRepayment = 2,

        /// <summary>
        /// ТР - погашение задолженности по требованию налогового органа об уплате налогов (сборов);
        /// </summary>
        [Description("ТР")]
        Tr = 3,

        /// <summary>
        /// РС - погашение рассроченной задолженности;
        /// </summary>
        [Description("РС")]
        Rs = 4,

        /// <summary>
        /// ОТ - погашение отсроченной задолженности;
        /// </summary>
        [Description("ОТ")]
        Ot = 5,

        /// <summary>
        /// РТ - погашение реструктурируемой задолженности;
        /// </summary>
        [Description("РТ")]
        Rt = 6,

        /// <summary>
        /// ВУ - погашение отсроченной задолженности в связи с введением внешнего управления;
        /// </summary>
        [Description("ВУ")]
        Vu = 7,

        /// <summary>
        /// ПР - погашение задолженности, приостановленной к взысканию;
        /// </summary>
        [Description("ПР")]
        Pr = 8,

        /// <summary>
        /// АП - погашение задолженности по акту проверки;
        /// </summary>
        [Description("АП")]
        Ap = 9,

        /// <summary>
        /// АР - погашение задолженности по исполнительному документу.
        /// </summary>
        [Description("АР")]
        Ar = 10,

        /// <summary>
        /// БФ -  текущие платежи физических лиц – клиентов банков (владельцев счета), уплачиваемые со своего банковского счета.
        /// </summary>
        [Description("БФ")]
        Bf = 11,

        /// <summary>
        /// ИН - Погашение инвестиционного налогового кредита
        /// </summary>
        [Description("ИН")]
        IN = 12,

        /// <summary>
        /// ТЛ - номер определения арбитражного суда об удовлетворении заявления о намерении погасить требования к должнику;
        /// </summary>
        [Description("ТЛ")]
        TL = 13,

        /// <summary>
        /// РК - номер реестра дела о банкротстве
        /// </summary>
        [Description("РК")]
        RK = 14,

        /// <summary>
        /// ЗТ - погашение текущей задолженности в ходе процедур, применяемых в деле о банкротстве
        /// </summary>
        [Description("ЗТ")]
        ZT = 15,

        /// <summary>
        /// 0 - остальные виды платежей
        /// </summary>
        [Description("0")]
        Other = 16,

        /// <summary>
        /// ПБ - погашение задолженности в ходе процедур, применяемых в деле о банкротстве
        /// </summary>
        [Description("ПБ")]
        PB = 17,
    }

    public static class BudgetaryPaymentBaseExtensions
    {
        public static string ToText(this BudgetaryPaymentBase type)
        {
            try
            {
                var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(ForValue(type), typeof(DescriptionAttribute));
                return attr == null ? string.Empty : attr.Description;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

        private static MemberInfo ForValue(BudgetaryPaymentBase menuType)
        {
            return typeof(BudgetaryPaymentBase).GetField(Enum.GetName(typeof(BudgetaryPaymentBase), menuType));
        }
    }
}
