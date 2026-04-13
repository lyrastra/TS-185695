using System;
using System.ComponentModel;
using System.Reflection;

namespace Moedelo.Common.Enums.Enums.Documents
{
    public static class AdvanceStatementItemDataTypeExtension
    {
        public static string GetName(this AdvanceStatementItemDataType dateType)
        {
            try
            {
                var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(ForValue(dateType), typeof(DescriptionAttribute));

                if (attr != null && !string.IsNullOrEmpty(attr.Description))
                {
                    return attr.Description;
                }

                return string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
        }

        private static MemberInfo ForValue(AdvanceStatementItemDataType type)
        {
            return
                typeof(AdvanceStatementItemDataType).GetField(Enum.GetName(typeof(AdvanceStatementItemDataType), type));
        }
    }
}
