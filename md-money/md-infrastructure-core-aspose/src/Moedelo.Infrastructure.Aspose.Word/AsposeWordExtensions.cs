using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Words;
using Aspose.Words.MailMerging;
using Moedelo.Infrastructure.Aspose.Word.Internals;

namespace Moedelo.Infrastructure.Aspose.Word
{
    public static class AsposeWordExtensions
    {
        /// <summary>
        /// Сохраняет документ в масив байтов
        /// </summary>
        /// <param name="document">aspose-word документ</param>
        /// <param name="format">формат, в котором нужно сохранить</param>
        public static byte[] ToFile(this Document document, SaveFormat format)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            using (var outputStream = new MemoryStream())
            {
                document.Save(outputStream, format);
                return outputStream.ToArray();
            }
        }
        
        /// <summary>
        /// Заполняет aspose-документ данными из переданной модели.
        /// </summary>
        /// <param name="document">Aspose-word документ</param>
        /// <param name="data">Данные, которыми нужно заполнить шаблон</param>
        public static void ApplyMergeFields<T>(this Document document, T data)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            document.MailMerge.CleanupOptions |= MailMergeCleanupOptions.RemoveEmptyParagraphs;
            var result = GetMailMergeData(data);

            if (result.Any())
            {
                document.MailMerge.Execute(result.Keys.ToArray(), result.Values.ToArray());
                var dataTables = result.Where(x => x.Value is MailMergeDataSource).ToList();

                foreach (var pair in dataTables)
                {
                    document.MailMerge.ExecuteWithRegions(pair.Value as MailMergeDataSource);
                }

                document.MailMerge.CleanupOptions |= MailMergeCleanupOptions.RemoveEmptyParagraphs;
                document.MailMerge.DeleteFields();
            }
        }

        private static Dictionary<string, object> GetMailMergeData<T>(T data, string parentName = null)
        {
            var result = new Dictionary<string, object>();
            var properties = data.GetType().GetProperties();

            foreach (var property in properties)
            {
                var name = string.IsNullOrEmpty(parentName) ? property.Name : $"{parentName}.{property.Name}";
                var value = data.GetType().GetProperty(property.Name).GetValue(data, null);

                if (property.PropertyType.IsImage())
                {
                    result.Add(name, value);
                }
                else if (property.PropertyType.IsTable())
                {
                    var ordersDataSource = new MailMergeDataSource((IEnumerable<dynamic>)value, name);
                    result.Add(name, ordersDataSource);
                }
                else if (property.PropertyType.IsDictionary())
                {
                    foreach (DictionaryEntry item in (IDictionary)value)
                    {
                        var dictKey = item.Key.ToString();
                        var dictValue = item.Value;

                        var innerDict = GetMailMergeData(dictValue, $"{name}.{dictKey}").ToDictionary(d => $"{d.Key}", d => d.Value);
                        result = result.Union(innerDict).ToDictionary(d => d.Key, d => d.Value);
                    }
                }
                else if (value != null && property.PropertyType.IsComplexObject())
                {
                    var innerDict = GetMailMergeData(value, name).ToDictionary(d => $"{d.Key}", d => d.Value);
                    result = result.Union(innerDict).ToDictionary(d => d.Key, d => d.Value);
                }
                else
                {
                    result.Add(name, value);
                }
            }

            return result;
        }

        /// <summary>
        /// Вставляет документ srcDoc целиком после установленного места
        /// реализация взята из источника https://docs.aspose.com/pages/viewpage.action?pageId=2589132#HowtoInsertaDocumentintoanotherDocument(zArchived)-bookmark
        /// </summary>
        public static void InsertDocument(this Node insertAfterNode, Document srcDoc)
        {
            // Make sure that the node is either a paragraph or table.
            if ((!insertAfterNode.NodeType.Equals(NodeType.Paragraph)) &
              (!insertAfterNode.NodeType.Equals(NodeType.Table)))
                throw new ArgumentException("The destination node should be either a paragraph or table.");

            // We will be inserting into the parent of the destination paragraph.
            CompositeNode dstStory = insertAfterNode.ParentNode;

            // This object will be translating styles and lists during the import.
            NodeImporter importer = new NodeImporter(srcDoc, insertAfterNode.Document, ImportFormatMode.KeepSourceFormatting);

            // Loop through all sections in the source document.
            foreach (Section srcSection in srcDoc.Sections)
            {
                // Loop through all block level nodes (paragraphs and tables) in the body of the section.
                foreach (Node srcNode in srcSection.Body)
                {
                    // Let's skip the node if it is a last empty paragraph in a section.
                    if (srcNode.NodeType.Equals(NodeType.Paragraph))
                    {
                        Paragraph para = (Paragraph)srcNode;
                        if (para.IsEndOfSection && !para.HasChildNodes)
                            continue;
                    }

                    // This creates a clone of the node, suitable for insertion into the destination document.
                    Node newNode = importer.ImportNode(srcNode, true);

                    // Insert new node after the reference node.
                    dstStory.InsertAfter(newNode, insertAfterNode);
                    insertAfterNode = newNode;
                }
            }
        }
    }
}