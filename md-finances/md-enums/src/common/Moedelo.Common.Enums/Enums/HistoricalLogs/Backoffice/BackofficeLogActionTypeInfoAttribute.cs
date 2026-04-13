using System;

namespace Moedelo.Common.Enums.Enums.HistoricalLogs.Backoffice
{
    public class BackofficeLogActionTypeInfoAttribute : Attribute
    {
        public string DisplayName { get; set; }

        public BackOfficeLogObjectType ObjectType { get; set; }

        public BackofficeLogActionTypeInfoAttribute(
            string displayName,
            BackOfficeLogObjectType objectType = BackOfficeLogObjectType.Empty)
        {
            DisplayName = displayName;
            ObjectType = objectType;
        }
    }
}