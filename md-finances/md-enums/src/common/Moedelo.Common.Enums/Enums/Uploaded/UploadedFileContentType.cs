namespace Moedelo.Common.Enums.Enums.Uploaded
{
    /// <summary> Какую иконку приставить к файлу? </summary>
    public enum UploadedFileContentType
    {
        /// <summary> Просто файл </summary>
        AnyFile = 0,

        /// <summary> Изображение </summary>
        Image = 1,

        /// <summary> Документ - doc, docx, rtf и т.д. </summary>
        Document = 2,

        /// <summary> Документ в формате PDF </summary>
        Pdf = 3,

        /// <summary> Электронная таблица - xls и т.д. </summary>
        Spreadsheet = 4,

        /// <summary> XML-файл </summary>
        Xml = 5,

        /// <summary> HTM-файл </summary>
        Htm = 6
    }
}