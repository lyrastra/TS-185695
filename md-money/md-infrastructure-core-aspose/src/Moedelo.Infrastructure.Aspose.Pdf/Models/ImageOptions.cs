namespace Moedelo.Infrastructure.Aspose.Pdf.Models
{
    public class ImageOptions
    {
        public ImageOptions(ImageFormat format)
        {
            Format = format;
        }
        
        public ImageFormat Format { get; set; }
        
        public (int Width, int Heght)? Size { get; set; }
        
        public (int Horizontal, int Vertical)? Resolution { get; set; }
    }
}