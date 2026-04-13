using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Enums.Extensions;

namespace Moedelo.Common.Enums.Enums.RegistrationService.LeadGroup
{
    public static class LeadGroupExtension
    {
        public static List<LeadGroup> GetAll()
        {
            return Enum.GetValues(typeof(LeadGroup)).OfType<LeadGroup>().ToList();
        }

        public static Dictionary<string, LeadGroup> GetNamesDictionary()
        {
            return GetAll().ToDictionary(k => k.GetName(), v => v);
        }

        public static Dictionary<string, LeadGroup> GetStringValuesDictionary()
        {
            return GetAll().ToDictionary(k => k.GetStringValue(), v => v);
        }

        public static string GetName(this LeadGroup group)
        {
            return group.GetEnumAttribute<LeadGroupAttribute, LeadGroup>()?.DisplayName;
        }

        public static string GetStringValue(this LeadGroup group)
        {
            return group.GetEnumAttribute<LeadGroupAttribute, LeadGroup>()?.StringValue;
        }
    }
}