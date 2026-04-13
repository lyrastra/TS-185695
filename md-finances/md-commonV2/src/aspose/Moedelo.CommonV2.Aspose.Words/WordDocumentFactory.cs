using System;
using System.IO;
using Aspose.Words;
using Aspose.Words.MailMerging;

namespace Moedelo.CommonV2.Reports.Aspose.Words
{
    /// <summary>
    /// Класс для создания word-документов на основе механизма обновляемых полей
    /// https://confluence.mdtest.org/pages/viewpage.action?pageId=24125476
    /// </summary>
    public static class WordDocumentFactory
    {
        static WordDocumentFactory()
        {
            AsposeWordsLicenseInitializer.Initalize();
        }

        /// <summary>
        /// Создает aspose-модель word-документа по шаблону.
        /// Заполняет документ данными из переданной модели.
        /// </summary>
        /// <param name="data">Данные, которыми нужно заполнить шаблон</param>
        /// <param name="reportTemplate">шаблон word-документа </param>
        /// <param name="callback">Обработчик событий слияния полей.</param>
        /// <returns>aspose-модель документа</returns>
        public static Document Create<T>(T data, byte[] reportTemplate, IFieldMergingCallback callback = null) where T : class
        {
            return Create(data, reportTemplate, callback, _ => _);
        }

        /// <summary>
        /// Создает aspose-модель word-документа по шаблону.
        /// Заполняет документ данными из переданной модели.
        /// Преимущественно метод модифицирует исходный объект документа, но для гибкости
        /// допускается создание нового документа на основе существующего.
        /// </summary>
        /// <param name="data">Данные, которыми нужно заполнить шаблон</param>
        /// <param name="reportTemplate">шаблон word-документа </param>
        /// <param name="callback">Обработчик событий слияния полей.</param>
        /// <param name="preprocessor">
        /// Функция предобработки, позволяющая модифицировать переданный документ
        /// или создать новый на его основе.
        /// </param>
        /// <returns>aspose-модель документа</returns>
        public static Document Create<T>(T data, byte[] reportTemplate, IFieldMergingCallback callback,
            Func<Document, Document> preprocessor) where T : class
        {
            _ = data ?? throw new ArgumentNullException(nameof(data));
            _ = reportTemplate ?? throw new ArgumentNullException(nameof(reportTemplate));
            _ = preprocessor ?? throw new ArgumentNullException(nameof(preprocessor));
            
            using (var docStream = new MemoryStream(reportTemplate))
            {
                var document = new Document(docStream);
                if (callback != null)
                {
                    document.MailMerge.FieldMergingCallback = callback;
                }
                document = preprocessor(document);
                document.ApplyMergeFields(data);
                return document;
            }
        }

        /// <summary>
        /// Создает aspose-модель word-документа по шаблону. 
        /// Очищает все обновляемые поля.
        /// </summary>
        /// <param name="reportTemplate">шаблон word-документа </param>
        /// <returns>aspose-модель документа</returns>
        public static Document Create(byte[] reportTemplate)
        {
            if (reportTemplate == null)
            {
                throw new ArgumentNullException(nameof(reportTemplate));
            }

            using (var docStream = new MemoryStream(reportTemplate))
            {
                var document = new Document(docStream);

                document.ApplyMergeFields(new {});

                return document;
            }
        }
    }
}