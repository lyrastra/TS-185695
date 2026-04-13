using System.Reflection;
using Moedelo.CommonV2.Cells.Enums;

namespace Moedelo.CommonV2.Cells.Extensions
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
            return property.IsSimple()
                ? InsertDataType.Simple
                : property.IsSimpleList()
                    ? InsertDataType.SimpleList
                    : property.IsObjectList()
                        ? InsertDataType.ObjectList
                        : property.IsCompoundObject() 
                            ? InsertDataType.CompoundObject 
                            : InsertDataType.None;
        }
    }
}