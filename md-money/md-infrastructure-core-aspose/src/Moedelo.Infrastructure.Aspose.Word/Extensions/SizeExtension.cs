using System.Drawing;

namespace Moedelo.Infrastructure.Aspose.Word.Extensions
{
    public static class SizeExtension
    {
        /// <summary>
        /// Коэффициент для перевода пикселов в точки, 1em = 12pt = 16px = 100% 
        /// </summary>
        const double pixelToPoint = 0.75;

        public static Size PixelsToPoints(this Size size)
        {
            return new Size((int) (size.Width * pixelToPoint), (int) (size.Height * pixelToPoint));
        }
    }
}