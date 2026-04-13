using System;

namespace Moedelo.Infrastructure.Aspose.Cells.Attributes
{
    /// <summary>
    /// Номер рабочего листа (от 0), где будет работать десериализтор 
    /// </summary>
    public class ExcelWorksheetAttribute : Attribute
    {
        public int Number{ get; set; }

        public ExcelWorksheetAttribute(int number)
        {
            Number = number;
        }
    }
}