using System.Reflection;
using Moedelo.Infrastructure.Aspose.Cells.Enums;
using Moedelo.Infrastructure.System.Extensions.PropertyInfos;

namespace Moedelo.Infrastructure.Aspose.Cells.Extensions
{
    /// <summary>
    /// Класс-расширение для получения типа вставляемых данных
    /// </summary>
    public static class InsertDataTypeExtension
    {
        /// <summary>
        /// Получить тип вставляемых данных по информации об аттрибуте
        /// </summary>
        /// <param name="property">Информация об аттрибуте</param>
        /// <returns>Тип вставляемых данных</returns>
        public static InsertDataType GetInsertDataType(this PropertyInfo property)
        {
            if (property.IsSimple())
            {
                return InsertDataType.Simple;
            }

            if (property.IsSimpleList())
            {
                return InsertDataType.SimpleList;
            }

            if (property.IsObjectList())
            {
                return InsertDataType.ObjectList;
            }

            return property.IsCompoundObject() ? InsertDataType.CompoundObject : InsertDataType.None;
        }
    }
}