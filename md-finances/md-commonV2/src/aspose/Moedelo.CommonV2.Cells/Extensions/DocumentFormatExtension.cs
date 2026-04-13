using System;
using Aspose.Cells;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.CommonV2.Cells.Extensions
{
    public static class DocumentFormatExtension
    {
        /// <summary>
        /// Преобразует значение типа DocumentFormat в значение SaveFormat из библиотеки Aspose.Cells
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static SaveFormat ToSaveFormat(this DocumentFormat format)
        {
            switch (format)
            {
                case DocumentFormat.Xlsx:
                    return SaveFormat.Xlsx;
                case DocumentFormat.Xls:
                    return SaveFormat.Excel97To2003;
                case DocumentFormat.Pdf:
                    return SaveFormat.Pdf;
                default:
                    throw new ArgumentException($"Unsupported format '{format}'");
            }
        }
    }
}