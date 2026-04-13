using System;

namespace Moedelo.Common.Enums.Enums.Accounting
{
    [AttributeUsage(AttributeTargets.Field)]
    public class KbkTypeAttribute: Attribute
    {
        public string Key;
        
        public KbkTypeAttribute(string key)
        {
            this.Key = key;
        }
    }
}
