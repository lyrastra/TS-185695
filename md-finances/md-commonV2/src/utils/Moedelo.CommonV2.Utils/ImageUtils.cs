using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Moedelo.CommonV2.Utils
{
    public static class ImageUtils
    {
        public static byte[] Compress(byte[] bytes, string mimeType, int targetSize, int quality = 80)
        {
            if (bytes == null) throw new ArgumentNullException(nameof(bytes));
            if (bytes.Length == 0) throw new ArgumentException(nameof(bytes));
            if (targetSize < 0) throw new ArgumentOutOfRangeException(nameof(targetSize));
            if (quality < 0) throw new ArgumentOutOfRangeException(nameof(quality));

            var codecInfo = GetEncoderInfo(mimeType);

            var canChangeQuality = ChangeQuality(bytes, codecInfo, quality).Length < targetSize;

            if (canChangeQuality)
            {
                return CompressInternal(bytes, codecInfo, targetSize, ChangeQuality);
            }

            return CompressInternal(bytes, codecInfo, targetSize, Resize);
        }

        private static byte[] CompressInternal(byte[] bytes,
            ImageCodecInfo codecInfo,
            int targetSize,
            Func<byte[], ImageCodecInfo, int, byte[]> transform)
        {
            var left = 0;
            var right = 100;

            while (left <= right)
            {
                var mid = (left + right) / 2;
                var size = transform(bytes, codecInfo, mid).Length;

                if (size >= targetSize)
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return transform(bytes, codecInfo, right);
        }

        private static byte[] ChangeQuality(byte[] bytes, ImageCodecInfo codecInfo, int quality)
        {
            using var imageStream = new MemoryStream(bytes);
            using var image = Image.FromStream(imageStream);
            var encoder = Encoder.Quality;
            var encoderParameters = new EncoderParameters(1);
            var encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;

            using var stream = new MemoryStream();
            image.Save(stream, codecInfo, encoderParameters);

            return stream.ToArray();
        }

        private static byte[] Resize(byte[] bytes, ImageCodecInfo codecInfo, int scale)
        {
            if (scale < 0 || bytes == null) return bytes;

            var imageFormat = SupposedFormats.First(x => x.Guid == codecInfo.FormatID);

            using var imageStream = new MemoryStream(bytes);
            using var image = Image.FromStream(imageStream);
            var scaleFactor = scale / 100.0;
            var width = (int)(image.Width * scaleFactor);
            var height = (int)(image.Height * scaleFactor);

            using var bitmap = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.DrawImage(image, 0, 0, width, height);

            using var stream = new MemoryStream();
            bitmap.Save(stream, imageFormat);

            return stream.ToArray();
        }

        private static ImageFormat[] SupposedFormats = new[] { ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Tiff };

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            var codecInfo = ImageCodecInfo.GetImageDecoders()
                    .FirstOrDefault(x => x.MimeType == mimeType);

            if (codecInfo == null)
                throw new ArgumentException(nameof(mimeType));

            return codecInfo;
        }
    }
}
