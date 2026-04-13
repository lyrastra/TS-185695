using System.Reflection;
using Moedelo.CommonV2.Cells.Enums;
using Moedelo.CommonV2.Cells.Extensions;

namespace Moedelo.CommonV2.Cells.Models
{
    /// <summary>
    /// Вставляемые данные
    /// </summary>
    public class InsertDataItem
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса вставляемых данных
        /// </summary>
        /// <param name="propertyInfo">Информация об аттрибуте</param>
        /// <param name="model">Модель</param>
        public InsertDataItem(PropertyInfo propertyInfo, object model)
        {
            Name = propertyInfo.Name;
            Value = propertyInfo.GetValue(model);
            DataType = propertyInfo.GetInsertDataType();
        }

        /// <summary>
        /// Имя вставляемых данных
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Значение вставляемых данных
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Тип вставляемых данных
        /// </summary>
        public InsertDataType DataType { get; set; }
    }
}