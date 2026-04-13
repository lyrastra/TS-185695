using System;

namespace Moedelo.Infrastructure.Aspose.Cells.Attributes
{
    /// <summary>
    /// Номер строки, откуда начнет чтение десериализатор
    /// </summary>
    public class ExcelRowAttribute : Attribute
    {
        public int Number{ get; set; }

        public ExcelRowAttribute(int number)
        {
            Number = number;
        }
    }
}