using System;

namespace Moedelo.Common.Enums.Enums.RegistrationService.LeadGroup
{
    public class LeadGroupAttribute : Attribute
    {
        public string DisplayName { get; set; }

        public string StringValue { get; set; }

        public LeadGroupAttribute(string displayName, string stringValue)
        {
            DisplayName = displayName;
            StringValue = stringValue;
        }
    }
}