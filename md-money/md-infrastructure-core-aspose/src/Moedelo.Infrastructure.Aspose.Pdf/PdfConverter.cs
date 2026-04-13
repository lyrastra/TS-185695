using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Moedelo.Infrastructure.Aspose.Pdf.Helpers;
using Moedelo.Infrastructure.Aspose.Pdf.Models;

namespace Moedelo.Infrastructure.Aspose.Pdf
{
    public static class PdfConverter
    {
        static PdfConverter()
        {
            AsposePdfActivator.ApplyLicense();
        }

        /// <summary>
        /// Постранично конвертирует pdf-отчет в картинки
        /// </summary>
        public static List<byte[]> ToImage(byte[] pdfFileBytes, ImageOptions options)
        {
            using (var pdfStream = new MemoryStream(pdfFileBytes))
            {
                var document = new Document(pdfStream);
                var images = new byte[document.Pages.Count][];
                var pages = document.Pages.ToList();
                var device = GetImageDevice(options);

                Parallel.ForEach(pages, new ParallelOptions { MaxDegreeOfParallelism = 3 }, (page, loopstate, index) =>
                {
                    images[index] = ConvertPdfPageToImage(page, device);
                });

                return images.ToList();
            }
        }
        
        /// <summary>
        /// Конвертирует указанную страницу pdf-отчета в картинку
        /// </summary>
        public static byte[] ToImage(byte[] pdfFileBytes, int pageNumber, ImageOptions options)
        {
            using (var pdfStream = new MemoryStream(pdfFileBytes))
            {
                using (var document = new Document(pdfStream))
                {
                    var page = document.Pages.FirstOrDefault(x => x.Number == pageNumber);
                    var device = GetImageDevice(options);
                
                    return ConvertPdfPageToImage(page, device);
                }
            }
        }

        /// <summary>
        /// Конвертирует из html в pdf
        /// </summary>
        public static byte[] FromHtml(string html, HtmlConvertationOptions options = null)
        {
            options = options ?? new HtmlConvertationOptions();
            using (var htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(html ?? string.Empty)))
            {
                var loadOptions = new HtmlLoadOptions {IsRenderToSinglePage = options.IsRenderToSinglePage};
                if (options.IsResetPageMargins)
                {
                    loadOptions.PageInfo.Margin = new MarginInfo(0, 0, 0, 0);
                }

                using (var doc = new Document(htmlStream, loadOptions))
                {
                    if (options.IsOptimizeOutput)
                    {
                        doc.OptimizeResources();
                    }

                    using (var outputStream = new MemoryStream())
                    {
                        doc.Save(outputStream, SaveFormat.Pdf);

                        return outputStream.ToArray();
                    }
                }
            }
        }
        
        private static ImageDevice GetImageDevice(ImageOptions options)
        {
            const int defaultResolution = 150;
            
            if (options == null)
                return null;

            var resolution = options.Resolution.HasValue
                ? new Resolution(options.Resolution.Value.Horizontal, options.Resolution.Value.Vertical)
                : new Resolution(defaultResolution); 
            
            switch (options.Format)
            {
                case ImageFormat.Jpeg: 
                    return options.Size.HasValue 
                        ? new JpegDevice(options.Size.Value.Width, options.Size.Value.Heght, resolution)
                        : new JpegDevice(resolution);
                case ImageFormat.Png:
                    return options.Size.HasValue 
                        ? new PngDevice(options.Size.Value.Width, options.Size.Value.Heght, resolution)
                        : new PngDevice(resolution);
                
                default:
                    throw new InvalidEnumArgumentException($"Not supported device to {options.Format.ToString()} format");
            }
        }
        
        private static byte[] ConvertPdfPageToImage(Page page, PageDevice device)
        {
            if (page == null)
                return null;
            
            using (var imageStream = new MemoryStream())
            {
                device.Process(page, imageStream);
                
                return imageStream.ToArray();
            }
        }
    }
}
