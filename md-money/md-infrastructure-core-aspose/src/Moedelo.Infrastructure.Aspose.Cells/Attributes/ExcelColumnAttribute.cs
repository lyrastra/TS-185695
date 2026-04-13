using System;

namespace Moedelo.Infrastructure.Aspose.Cells.Attributes
{
    /// <summary>
    /// Аттрибут-сопоставление свойства модели и колонки excel
    /// </summary>
    public class ExcelColumnAttribute : Attribute
    {
        public int Number{ get; set; }

        public ExcelColumnAttribute(int number)
        {
            Number = number;
        }
    }
}