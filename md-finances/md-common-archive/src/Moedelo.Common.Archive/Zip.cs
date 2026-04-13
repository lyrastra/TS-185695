using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Archive.Exceptions;

// ReSharper disable once CheckNamespace
namespace Moedelo.Common.Archive
{
    /// <summary>
    /// Класс, предоставляющий методы для работы с ZIP-архивами: упаковка и распаковка файлов.
    /// </summary>
    public static class Zip
    {
        /// <summary>
        /// Упаковывает файлы в ZIP-архив с уникальными именами файлов.
        /// </summary>
        /// <param name="items">Коллекция элементов для упаковки.</param>
        /// <param name="entryNameEncoding">Кодировка имен файлов.</param>
        /// <returns>ZIP-архив в виде байтового массива.</returns>
        public static async Task<byte[]> PackWithUniqueNamesAsync(
            IReadOnlyCollection<ZipItem> items,
            Encoding entryNameEncoding = null)
        {
            items.CheckEmptyFileData();
            items.CheckEmptyFileName();
        
            return await InternalPackAsync(items, entryNameEncoding, true);
        }

        /// <summary>
        /// Упаковывает файлы в ZIP-архив без изменений имен файлов.
        /// </summary>
        /// <param name="items">Коллекция элементов для упаковки.</param>
        /// <param name="entryNameEncoding">Кодировка имен файлов.</param>
        /// <returns>ZIP-архив в виде байтового массива.</returns>
        public static async Task<byte[]> PackAsync(
            IReadOnlyCollection<ZipItem> items,
            Encoding entryNameEncoding = null)
        {
            items.CheckEmptyFileData();
            items.CheckEmptyFileName();
        
            return await InternalPackAsync(items, entryNameEncoding, false);
        }
        
        public static async Task<ZipItem[]> UnpackAsync(byte[] zipData,
            Encoding entryNameEncoding = null)
        {
            using var ms = new MemoryStream(zipData);
            using var archive = new ZipArchive(ms, ZipArchiveMode.Read, false, entryNameEncoding);

            var tasks = archive.Entries.Select(async entry =>
            {
                using var dataStream = new MemoryStream();
                using var entryStream = entry.Open();
                await entryStream.CopyToAsync(dataStream);
                return new ZipItem(entry.Name, dataStream.ToArray());
            });

            return await Task.WhenAll(tasks);
        }
        
        private static async Task<byte[]> InternalPackAsync(
            IReadOnlyCollection<ZipItem> items,
            Encoding entryNameEncoding,
            bool useUniqueFilenames)
        {
            using var ms = new MemoryStream();
            using (var archive = new ZipArchive(ms, ZipArchiveMode.Create, false, entryNameEncoding))
            {
                var filenames = new List<string>();
                foreach (var item in items)
                {
                    var fileName = useUniqueFilenames 
                        ? GetUniqueFilename(filenames, item.Name) 
                        : item.Name;
                    filenames.Add(fileName);

                    var entry = archive.CreateEntry(fileName);
                    using var entryStream = entry.Open();
                    await entryStream.WriteAsync(item.Data, 0, item.Data.Length);
                }
            }

            return ms.ToArray();
        }

        private static void CheckEmptyFileData(this IReadOnlyCollection<ZipItem> entries)
        {
            if (entries == null 
                || !entries.Any() 
                || entries.Any(entry => entry.Data == null || entry.Data.Length == 0))
            {
                throw new EmptyFileArchiveException();
            }
        }
        
        private static void CheckEmptyFileName(this IReadOnlyCollection<ZipItem> entries)
        {
            if(entries.Any(entry => string.IsNullOrEmpty(entry?.Name)))
            {
                throw new EmptyFileNameArchiveException();
            }
        }
        
        private static string GetUniqueFilename(IReadOnlyCollection<string> filenames, string filename)
        {
            if (!filenames.Contains(filename)) return filename;
    
            var index = 2;
            while (true)
            {
                var uniqueName = $"{Path.GetFileNameWithoutExtension(filename)}({index++}){Path.GetExtension(filename)}";
                if (!filenames.Contains(uniqueName)) return uniqueName;
            }
        }
    }
}