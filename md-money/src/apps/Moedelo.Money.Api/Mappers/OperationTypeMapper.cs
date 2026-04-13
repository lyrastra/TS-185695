using Moedelo.Money.Api.Models;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Attributes;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Moedelo.Money.Api.Mappers
{
    public static class OperationTypeMapper
    {
        public static OperationTypeDescriptionDto[] Get()
        {
            var values = (OperationType[])Enum.GetValues(typeof(OperationType));
            return values.Select(x =>
                new OperationTypeDescriptionDto
                {
                    OperationType = x,
                    SourceType = GetSource(x) ?? 0,
                    Description = GetDescription(x)
                }).ToArray();
        }

        private static MoneySourceType? GetSource(this Enum value)
        {
            return (MoneySourceType?)value.GetMemberInfo()
                ?.GetCustomAttribute<OperationKindAttribute>()
                ?.Kind;
        }

        private static string GetDescription(this Enum value)
        {
            return value.GetMemberInfo()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description;
        }

        private static MemberInfo GetMemberInfo(this Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault();
        }
    }
}
