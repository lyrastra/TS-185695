using System;

namespace Moedelo.Common.Enums.Enums.Access.Attributes
{
    /// <summary> Атрибут для перечисления прав </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class AccessRuleAttribute : Attribute
    {
        public AccessRuleGroups[] AccessGroups { get; set; }
        public string Description { get; set; }
        public AccessRuleSite AccessRuleSite { get; set; }

        public AccessRuleAttribute()
        {
            AccessGroups = new AccessRuleGroups[0];
            Description = "";
            AccessRuleSite = AccessRuleSite.Default;
        }

        public AccessRuleAttribute(string description, AccessRuleSite site, AccessRuleGroups[] groups)
        {
            Description = description;
            AccessGroups = groups;
            AccessRuleSite = site;
        }
        public AccessRuleAttribute(string description)
        {
            AccessGroups = new AccessRuleGroups[0];
            Description = description;
            AccessRuleSite = AccessRuleSite.Default;
        }
    }
}