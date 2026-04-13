using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moedelo.Common.Enums.Enums.Integration
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
        [BudgetaryPaymentBase("ТП", "тп")]
        CurrentPayment = 1,

        /// <summary>
        /// ЗД - Добровольное погашение задолженности по истекшим налоговым периодам (ЗД)
        /// </summary>
        [BudgetaryPaymentBase("ЗД")]
        FreeDebtRepayment = 2,

        /// <summary>
        /// ТР - погашение задолженности по требованию налогового органа об уплате налогов (сборов);
        /// </summary>
        [BudgetaryPaymentBase("ТР")]
        Tr = 3,

        /// <summary>
        /// РС - погашение рассроченной задолженности;
        /// </summary>
        [BudgetaryPaymentBase("РС")]
        Rs = 4,

        /// <summary>
        /// ОТ - погашение отсроченной задолженности;
        /// </summary>
        [BudgetaryPaymentBase("ОТ")]
        Ot = 5,

        /// <summary>
        /// РТ - погашение реструктурируемой задолженности;
        /// </summary>
        [BudgetaryPaymentBase("РТ")]
        Rt = 6,

        /// <summary>
        /// ВУ - погашение отсроченной задолженности в связи с введением внешнего управления;
        /// </summary>
        [BudgetaryPaymentBase("ВУ")]
        Vu = 7,

        /// <summary>
        /// ПР - погашение задолженности, приостановленной к взысканию;
        /// </summary>
        [BudgetaryPaymentBase("ПР")]
        Pr = 8,

        /// <summary>
        /// АП - погашение задолженности по акту проверки;
        /// </summary>
        [BudgetaryPaymentBase("АП")]
        Ap = 9,

        /// <summary>
        /// АР - погашение задолженности по исполнительному документу.
        /// </summary>
        [BudgetaryPaymentBase("АР")]
        Ar = 10,

        /// <summary>
        /// БФ -  текущие платежи физических лиц – клиентов банков (владельцев счета), уплачиваемые со своего банковского счета.
        /// </summary>
        [BudgetaryPaymentBase("БФ")]
        Bf = 11,

        /// <summary>
        /// ИН - Погашение инвестиционного налогового кредита
        /// </summary>
        [BudgetaryPaymentBase("ИН")]
        IN = 12,

        /// <summary>
        /// ТЛ - номер определения арбитражного суда об удовлетворении заявления о намерении погасить требования к должнику;
        /// </summary>
        [BudgetaryPaymentBase("ТЛ")]
        TL = 13,

        /// <summary>
        /// РК - номер реестра дела о банкротстве
        /// </summary>
        [BudgetaryPaymentBase("РК")]
        RK = 14,

        /// <summary>
        /// ЗТ - погашение текущей задолженности в ходе процедур, применяемых в деле о банкротстве
        /// </summary>
        [BudgetaryPaymentBase("ЗТ")]
        ZT = 15,

        /// <summary>
        /// 0 - остальные виды платежей
        /// </summary>
        [BudgetaryPaymentBase("0")]
        Other = 16,

        /// <summary>
        /// ПБ - погашение задолженности в ходе процедур, применяемых в деле о банкротстве
        /// </summary>
        [BudgetaryPaymentBase("ПБ")]
        PB = 17,
    }

    public static class BudgetaryPaymentBaseExtensions
    {
        private static readonly List<BudgetaryPaymentBase> PaymentBaseName = new List<BudgetaryPaymentBase>
            {
                BudgetaryPaymentBase.CurrentPayment,
                BudgetaryPaymentBase.CurrentPayment,
                BudgetaryPaymentBase.FreeDebtRepayment,
                BudgetaryPaymentBase.Tr,
                BudgetaryPaymentBase.Rs,
                BudgetaryPaymentBase.Ot,
                BudgetaryPaymentBase.Rt,
                BudgetaryPaymentBase.Vu,
                BudgetaryPaymentBase.Pr,
                BudgetaryPaymentBase.Ap,
                BudgetaryPaymentBase.Ar,
                BudgetaryPaymentBase.Bf,
                BudgetaryPaymentBase.IN,
                BudgetaryPaymentBase.TL,
                BudgetaryPaymentBase.RK,
                BudgetaryPaymentBase.ZT,
                BudgetaryPaymentBase.Other,
                BudgetaryPaymentBase.PB,
            };

        public static string ToText(this BudgetaryPaymentBase type)
        {
            try
            {
                var attr = (BudgetaryPaymentBaseAttribute)Attribute.GetCustomAttribute(ForValue(type), typeof(BudgetaryPaymentBaseAttribute));
                return attr == null ? string.Empty : attr.DetectStrings.FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

        public static BudgetaryPaymentBase ToEnum(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return BudgetaryPaymentBase.None;
            }

            return PaymentBaseName.FirstOrDefault(paymentBase => paymentBase.HasParseValue(data));
        }

        public static BudgetaryPaymentFoundation AsBudgetaryPaymentFoundations(this BudgetaryPaymentBase type)
        {
            switch (type)
            {
                case BudgetaryPaymentBase.Ap: return BudgetaryPaymentFoundation.Ap;
                case BudgetaryPaymentBase.Ar: return BudgetaryPaymentFoundation.Ar;
                case BudgetaryPaymentBase.Bf: return BudgetaryPaymentFoundation.Bf;
                case BudgetaryPaymentBase.ZT: return BudgetaryPaymentFoundation.Zt;
                case BudgetaryPaymentBase.Vu: return BudgetaryPaymentFoundation.Vu;
                case BudgetaryPaymentBase.Tr: return BudgetaryPaymentFoundation.Tr;
                case BudgetaryPaymentBase.TL: return BudgetaryPaymentFoundation.Tl;
                case BudgetaryPaymentBase.Rt: return BudgetaryPaymentFoundation.Rt;
                case BudgetaryPaymentBase.Rs: return BudgetaryPaymentFoundation.Rs;
                case BudgetaryPaymentBase.RK: return BudgetaryPaymentFoundation.Rk;
                case BudgetaryPaymentBase.Pr: return BudgetaryPaymentFoundation.Pr;
                case BudgetaryPaymentBase.PB: return BudgetaryPaymentFoundation.Zt;
                case BudgetaryPaymentBase.Other: return BudgetaryPaymentFoundation.Default;
                case BudgetaryPaymentBase.Ot: return BudgetaryPaymentFoundation.Ot;
                case BudgetaryPaymentBase.None: return BudgetaryPaymentFoundation.Default;
                case BudgetaryPaymentBase.IN: return BudgetaryPaymentFoundation.In;
                case BudgetaryPaymentBase.FreeDebtRepayment: return BudgetaryPaymentFoundation.Zd;
                case BudgetaryPaymentBase.CurrentPayment: return BudgetaryPaymentFoundation.Tp;
                default: return BudgetaryPaymentFoundation.Default;
            }
        }

        public static bool HasParseValue(this BudgetaryPaymentBase menuType, string item)
        {
            try
            {
                var attr = (BudgetaryPaymentBaseAttribute)Attribute.GetCustomAttribute(ForValue(menuType), typeof(BudgetaryPaymentBaseAttribute));

                if (attr != null && attr.DetectStrings.Contains(item))
                {
                    if (attr.DetectStrings.Contains(item))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        private static MemberInfo ForValue(BudgetaryPaymentBase menuType)
        {
            return typeof(BudgetaryPaymentBase).GetField(Enum.GetName(typeof(BudgetaryPaymentBase), menuType));
        }

        public static IList<string> GetValue(this BudgetaryPaymentBase budgetaryPayment)
        {
            // Get instance of the attribute.
            BudgetaryPaymentBaseAttribute attribute = (BudgetaryPaymentBaseAttribute)Attribute.GetCustomAttribute(typeof(BudgetaryPaymentBase), typeof(BudgetaryPaymentBaseAttribute));

            if (attribute == null)
            {
                return new List<string>();
            }

            return attribute.DetectStrings.ToList();
        }
    }
}