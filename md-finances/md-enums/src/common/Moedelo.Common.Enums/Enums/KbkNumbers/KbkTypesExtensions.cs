using System;
using System.Linq;
using System.Reflection;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Common.Enums.Enums.KbkNumbers
{
    public static class KbkTypesExtensions
    {
        /// <summary>
        /// Получить код, по которому сохраняется данный тип кбк в БД.
        /// Если код не найден, то выбрасывается исключение KbkCodeNotFoundExceptions
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetKey(this KbkType type)
        {
            var attr = (KbkTypeAttribute) Attribute.GetCustomAttribute(ForValue(type), typeof(KbkTypeAttribute));
            if (attr != null && !string.IsNullOrWhiteSpace(attr.Key)) 
                return attr.Key;
            
            throw new KbkCodeNotFoundExceptions(type);
        }
        
        private static MemberInfo ForValue(KbkType type)
        {
            return typeof(KbkType).GetField(Enum.GetName(typeof(KbkType), type));
        }
        
        /// <summary>
        /// Получить тип КБК по коду из БД
        /// Если тип КБК не найден, то выбрасывается исключение KbkTypeNotFoundException
        /// </summary>
        /// <param name="kbkCode">Код из БД</param>
        /// <returns></returns>
        public static KbkType GetKbkType(string kbkCode)
        {
            return Enum.GetValues(typeof (KbkType)).Cast<KbkType>().FirstOrDefault(kbkType => kbkType.GetKey() == kbkCode);
        }
    }
}
