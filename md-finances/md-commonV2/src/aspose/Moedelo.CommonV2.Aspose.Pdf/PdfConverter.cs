using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Devices;
using Moedelo.CommonV2.ASpose.Pdf.Licences;

namespace Moedelo.CommonV2.ASpose.Pdf
{
    public static class PdfConverter
    {
        static PdfConverter()
        {
            AsposePdfLicenseHelper.SetPdfLicense();
        }

        /// <summary>
        /// Постранично конвертирует pdf-отчет в картинки
        /// </summary>
        public static List<byte[]> ToPng(byte[] pdfFileBytes, ImageOptions options)
        {
            using (var pdfStream = new MemoryStream(pdfFileBytes))
            {
                var document = new Document(pdfStream);

                var images = new ContentWrapper[document.Pages.Count];
                var pages = document.Pages.Cast<Page>().ToList();

                var device = new PngDevice(new Resolution(options.HorizontalResolution, options.VerticalResolution));
                Parallel.ForEach(pages, new ParallelOptions { MaxDegreeOfParallelism = 3 }, (page, loopstate, index) =>
                {
                    images[index] = ConvertPdfPageToImage(page, device);
                });

                return images.Select(x => x.Content).ToList();
            }
        }

        public static List<byte[]> ToPng(Stream pdfStream, ImageOptions options)
        {
            var document = new Document(pdfStream);

            var images = new ContentWrapper[document.Pages.Count];
            var pages = document.Pages.Cast<Page>().ToList();

            var device = new PngDevice(new Resolution(options.HorizontalResolution, options.VerticalResolution));
            Parallel.ForEach(pages, new ParallelOptions { MaxDegreeOfParallelism = 3 },
                (page, loopstate, index) => { images[index] = ConvertPdfPageToImage(page, device); });

            return images.Select(x => x.Content).ToList();
        }

        /// <summary>
        /// Постранично конвертирует pdf-отчет в картинки
        /// </summary>
        public static List<byte[]> ToJpeg(byte[] pdfFileBytes, ImageOptionsWithOutputSize options)
        {
            using (var pdfStream = new MemoryStream(pdfFileBytes))
            {
                var document = new Document(pdfStream);

                var images = new ContentWrapper[document.Pages.Count];
                var pages = document.Pages.Cast<Page>().ToList();

                var device = new JpegDevice(new Resolution(options.HorizontalResolution, options.VerticalResolution));
                Parallel.ForEach(pages, new ParallelOptions { MaxDegreeOfParallelism = 3 }, (page, loopstate, index) =>
                {
                    images[index] = ConvertPdfPageToImage(page, device);
                });

                return images.Select(x => x.Content).ToList();
            }
        }

        /// <summary>
        /// Конвертирует указанную страницу pdf-отчета в картинку
        /// </summary>
        public static byte[] ToJpeg(byte[] pdfFileBytes, int pageNumber, ImageOptionsWithOutputSize options)
        {
            using (var pdfStream = new MemoryStream(pdfFileBytes))
            {
                var document = new Document(pdfStream);
                // Фильтруем нужные страницы
                var page = document.Pages.Cast<Page>().FirstOrDefault(x => x.Number == pageNumber);

                var device = new JpegDevice(options.Width, options.Height,
                    new Resolution(options.HorizontalResolution, options.VerticalResolution));

                return ConvertPdfPageToImage(page, device).Content;
            }
        }

        private static ContentWrapper ConvertPdfPageToImage(Page page, ImageDevice device)
        {
            using (var imageStream = new MemoryStream())
            {
                device.Process(page, imageStream);

                return new ContentWrapper {Content = imageStream.ToArray() };
            }
        }

        private class ContentWrapper
        {
            public byte[] Content { get; set; }
        }
    }
}
