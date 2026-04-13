using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using Aspose.Cells;
using Moedelo.Infrastructure.Aspose.Cells.Helpers;

namespace Moedelo.Infrastructure.Aspose.Cells
{
    /// <summary>
    /// Фабрика по созданию xls-подобных документов для работы через Aspose.Cells 
    /// </summary>
    public static class WorkbookFactory
    {
        private static readonly ConcurrentDictionary<TemplateKey, byte[]> TemplatesMap = new ConcurrentDictionary<TemplateKey, byte[]>();
        
        static WorkbookFactory()
        {
            AsposeCellsActivator.ApplyLicense();
        }

        /// <summary>
        /// Создает Workbook из шаблона (шаблон добавляется как embedded resource)
        /// </summary>
        /// <param name="assembly">Сборка, в которой расположен шаблон</param>
        /// <param name="templatePath">Путь внутри сборки</param>
        public static Workbook Create(Assembly assembly, string templatePath)
        {
            var templateKey = new TemplateKey(assembly, templatePath);
            var templateBytes = TemplatesMap.GetOrAdd(templateKey, key => ReadAsBytesFromAssembly(key.Assembly, key.TemplatePath));

            return CreateFromBytesArray(templateBytes);
        }

        private static byte[] ReadAsBytesFromAssembly(Assembly assembly, string templatePath)
        {
            using (var templateStream = assembly.GetManifestResourceStream(templatePath))
            {
                if (templateStream == null)
                {
                    throw new ArgumentException($"Error loading template '{templatePath}' in '{assembly.GetName().Name}'");
                }
                
                using (var ms = new MemoryStream())
                {
                    templateStream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        private static Workbook CreateFromBytesArray(byte[] templateBytes)
        {
            using (var stream = new MemoryStream(templateBytes))
            {
                return new Workbook(stream);
            }
        }
        
        private struct TemplateKey
        {
            public readonly Assembly Assembly;
            public readonly string TemplatePath;

            public TemplateKey(Assembly assembly, string templatePath)
            {
                Assembly = assembly;
                TemplatePath = templatePath;
            }
            
            public override bool Equals( object obj )
            {
                if( obj is TemplateKey value ) {
                    return TemplatePath == value.TemplatePath && Assembly == value.Assembly;
                }

                return false;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Assembly != null ? Assembly.GetHashCode() : 0) * 397) ^ (TemplatePath != null ? TemplatePath.GetHashCode() : 0);
                }
            }
        }
    }
}