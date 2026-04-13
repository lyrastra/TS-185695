using Moedelo.PaymentOrderImport.Enums.Attributes;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Moedelo.PaymentOrderImport.Enums.Extensions
{
    public static class PaymentImportOperationTypeExtension
    {
        public static OperationDirection AsOperationDirection(this PaymentImportOperationType operationType)
        {
            return GetOperationDirection(operationType);
        }

        public static string AsOperationDescription(this PaymentImportOperationType operationType)
        {
            return GetOperationDescription(operationType);
        }

        public static bool IsCurrencyOperation(this PaymentImportOperationType operationType)
        {
            return HasCurrencyOperationAttribute(operationType);
        }

        public static LegalType AsOperationLegal(this PaymentImportOperationType operationType)
        {
            return GetOperationAvailabe(operationType);
        }

        public static bool IsUserRuleOperation(this PaymentImportOperationType operationType)
        {
            return HasUserRuleAttribute(operationType);
        }

        public static bool IsTransferOperation(this PaymentImportOperationType operationType)
        {
            return HasTransferOperationAttribute(operationType);
        }
        
        public static bool IsIpOsnoUnavailable(this PaymentImportOperationType operationType)
        {
            return HasAttribute<IpOsnoUnavailableAttribute>(operationType);
        }

        public static bool IsPurseOperation(this PaymentImportOperationType operationType)
        {
            return HasPurseOperationAttribute(operationType);
        }

        public static bool IsAvailableWithContractor(this PaymentImportOperationType operationType)
        {
            return HasAvailableWithContractorAttribute(operationType);
        }

        public static bool IsAvailableTaxableSumType(this PaymentImportOperationType operationType)
        {
            return operationType 
                is PaymentImportOperationType.PaymentOrderIncomingOther 
                or PaymentImportOperationType.PaymentOrderOutgoingOther;
        }

        public static bool IsAvailableSyntheticAccountCode(this PaymentImportOperationType operationType)
        {
            return operationType 
                is PaymentImportOperationType.PaymentOrderIncomingOther 
                or PaymentImportOperationType.PaymentOrderOutgoingOther;
        }

        public static AccPostingOverrideDirection? GetAccPostingOverrideDirection(this PaymentImportOperationType operation)
        {
            return operation switch
            {
                PaymentImportOperationType.PaymentOrderIncomingOther => AccPostingOverrideDirection.Credit,
                PaymentImportOperationType.PaymentOrderOutgoingOther => AccPostingOverrideDirection.Debit,
                _ => null
            };
        }

        public static PaymentImportAccessRule? GetDependentAccessRule(this PaymentImportOperationType operationType)
        {
            return operationType.GetMemberInfo()
                .GetCustomAttribute<AvailableWithAccessRuleAttribute>()
                ?.AccessRule;
        }

        private static OperationDirection GetOperationDirection(PaymentImportOperationType operationType)
        {
            return operationType.GetMemberInfo()
                .GetCustomAttribute<OperationDirectionAttribute>()
                .Direction;
        }

        private static string GetOperationDescription(PaymentImportOperationType operationType)
        {
            return operationType.GetMemberInfo()
                .GetCustomAttribute<DescriptionAttribute>()
                .Description;
        }

        private static bool HasUserRuleAttribute(PaymentImportOperationType operationType)
        {
            var attribute = operationType.GetMemberInfo()
                .GetCustomAttribute<UserRuleAttribute>();

            return attribute != null;
        }

        private static bool HasCurrencyOperationAttribute(PaymentImportOperationType operationType)
        {
            var attribute = operationType.GetMemberInfo()
                .GetCustomAttribute<CurrencyOperationAttribute>();

            return attribute != null;
        }

        private static LegalType GetOperationAvailabe(PaymentImportOperationType operationType)
        {
            return operationType.GetMemberInfo()
                .GetCustomAttribute<OperationLegalAttribute>()
                .LegalType;
        }

        private static bool HasTransferOperationAttribute(PaymentImportOperationType operationType)
        {
            var attribute = operationType.GetMemberInfo()
                .GetCustomAttribute<TransferOperationAttribute>();

            return attribute != null;
        }

        private static bool HasPurseOperationAttribute(PaymentImportOperationType operationType)
        {
            var attribute = operationType.GetMemberInfo()
                .GetCustomAttribute<PurseOperationAttribute>();

            return attribute != null;
        }

        private static bool HasAvailableWithContractorAttribute(PaymentImportOperationType operationType)
        {
            var attribute = operationType.GetMemberInfo()
                .GetCustomAttribute<AvailableWithContractor>();

            return attribute != null;
        }

        private static MemberInfo GetMemberInfo(this Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault(x => x.DeclaringType == value.GetType());
        }

        private static bool HasAttribute<T>(PaymentImportOperationType operationType) where T: Attribute
        {
            var attribute = operationType.GetMemberInfo()
                .GetCustomAttribute<T>();

            return attribute != null;
        }
    }
}
