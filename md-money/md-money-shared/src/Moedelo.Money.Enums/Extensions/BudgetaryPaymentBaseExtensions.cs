using Moedelo.Money.Enums.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moedelo.Money.Enums.Extensions
{
    public static class BudgetaryPaymentBaseExtensions
    {
        public static string ToText(this BudgetaryPaymentBase type)
        {
            try
            {
                var enumValue = typeof(BudgetaryPaymentBase).GetField(Enum.GetName(typeof(BudgetaryPaymentBase), type));
                var attr = (BudgetaryPaymentBaseAttribute)Attribute.GetCustomAttribute(enumValue, typeof(BudgetaryPaymentBaseAttribute));
                return attr == null ? string.Empty : attr.DetectStrings.FirstOrDefault();
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }
        
        private static readonly IReadOnlyCollection<BudgetaryPaymentBase> PaymentBaseName = new []
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
        
        public static BudgetaryPaymentBase ToEnum(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return BudgetaryPaymentBase.None;
            }

            return PaymentBaseName.FirstOrDefault(paymentBase => paymentBase.HasParseValue(data));
        }
        
        private static bool HasParseValue(this BudgetaryPaymentBase menuType, string item)
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
    }
}
