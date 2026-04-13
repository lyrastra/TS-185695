using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Cells;
using Moedelo.Infrastructure.Aspose.Cells.Attributes;
using Moedelo.Infrastructure.Aspose.Cells.Helpers;

namespace Moedelo.Infrastructure.Aspose.Cells
{
    /// <summary>
    /// Десериализор простых excel-таблиц 
    /// </summary>
    public static class WorkbookSerializer
    {
        static WorkbookSerializer()
        {
            AsposeCellsActivator.ApplyLicense();
        }
        
        /// <summary>
        /// Десериализует входящий поток с excel-таблицей
        /// </summary>
        /// <param name="source">Входящий поток</param>
        /// <typeparam name="T">Результирующий тип</typeparam>
        /// <returns>Возвращает коллекцию объектов типа Т</returns>
        /// <exception cref="ArgumentException">Если не сопоставлены колонки в модели</exception>
        public static IEnumerable<T> Deserialize<T>(Stream source)
        {
            var type = typeof(T);
            using var book = new Workbook(source);

            var worksheetAttribute = (ExcelWorksheetAttribute)type.GetCustomAttributes(typeof(ExcelWorksheetAttribute), false).FirstOrDefault();
            var worksheet = book.Worksheets[worksheetAttribute?.Number ?? 0];

            var properties = type.GetProperties();
            var columns = properties
                .Select(property =>
                    new
                    {
                        Property = property,
                        Number = ((ExcelColumnAttribute) property
                            .GetCustomAttributes(typeof(ExcelColumnAttribute), false)
                            .FirstOrDefault())?.Number ?? -1
                    })
                .Where(x => x.Number != -1)
                .ToList();

            if (!columns.Any())
            {
                throw new ArgumentException($"{type.Name} свойствам не сопоставлен атрибут ExcelColumn");
            }

            var row = (ExcelRowAttribute)type.GetCustomAttributes(typeof(ExcelRowAttribute), false).FirstOrDefault();

            for (var rowNumber = row?.Number ?? 0; rowNumber <= worksheet.Cells.MaxDataRow; rowNumber++)
            {
                var instance = Activator.CreateInstance(type);

                foreach (var column in columns)
                {
                    column.Property.SetValue(instance, worksheet.Cells[rowNumber, column.Number].StringValue);
                }

                yield return (T)instance;
            }
        } 
    }
}