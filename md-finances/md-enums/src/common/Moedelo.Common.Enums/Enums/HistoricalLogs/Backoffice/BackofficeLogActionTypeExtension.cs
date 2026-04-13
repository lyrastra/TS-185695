using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Moedelo.Common.Enums.Extensions;

namespace Moedelo.Common.Enums.Enums.HistoricalLogs.Backoffice
{
    public static class BackofficeLogActionTypeExtension
    {
        private const byte MaxReportTypeValue = 64;
        private const byte MaxViewTypeValue = 95;

        public static List<BackofficeLogActionType> GetAllActionTypes()
        {
            return Enum.GetValues(typeof(BackofficeLogActionType))
                .OfType<BackofficeLogActionType>()
                .ToList();
        }

        public static Dictionary<string, BackofficeLogActionType> GetAllActionTypesWithNames()
        {
            return GetAllActionTypes().ToDictionary(k => k.GetName(), v => v);
        }

        public static bool IsReportActionType(this BackofficeLogActionType type)
        {
            return (byte) type <= MaxReportTypeValue;
        }

        public static bool IsViewType(this BackofficeLogActionType type)
        {
            return (byte) type > MaxReportTypeValue && (byte) type <= MaxViewTypeValue;
        }

        public static bool IsActionType(this BackofficeLogActionType type)
        {
            return (byte) type > MaxViewTypeValue;
        }

        public static string GetName(this BackofficeLogActionType type)
        {
            var attribute = type.GetEnumAttribute<BackofficeLogActionTypeInfoAttribute, BackofficeLogActionType>();
            return attribute?.DisplayName;
        }

        public static BackOfficeLogObjectType GetObjectType(this BackofficeLogActionType type)
        {
            var attribute = type.GetEnumAttribute<BackofficeLogActionTypeInfoAttribute, BackofficeLogActionType>();
            return attribute?.ObjectType ?? BackOfficeLogObjectType.Empty;
        }
        
        public static string GetObjectTypeName(this BackofficeLogActionType type)
        {
            var objectType = type.GetObjectType();
            return objectType.GetEnumAttribute<DisplayAttribute, BackOfficeLogObjectType>()?.Name ??
                   string.Empty;
        }
    }
}