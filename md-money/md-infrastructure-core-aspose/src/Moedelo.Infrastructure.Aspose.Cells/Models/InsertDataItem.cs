using System.Reflection;
using Moedelo.Infrastructure.Aspose.Cells.Enums;
using Moedelo.Infrastructure.Aspose.Cells.Extensions;

namespace Moedelo.Infrastructure.Aspose.Cells.Models
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