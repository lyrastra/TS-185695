using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Aspose.Words.Fields;
using Aspose.Words.MailMerging;
using Moedelo.Infrastructure.Aspose.Word.Extensions;

namespace Moedelo.Infrastructure.Aspose.Word
{
    public class AsposeDocsImageFieldCallback : IFieldMergingCallback
    {
        private readonly IReadOnlyDictionary<string, Size> imageConstrains;

        public AsposeDocsImageFieldCallback(IReadOnlyDictionary<string, Size> imageConstrains)
        {
            this.imageConstrains = imageConstrains;
        }

        public void FieldMerging(FieldMergingArgs args)
        {
        }

        public void ImageFieldMerging(ImageFieldMergingArgs args)
        {
            if (!imageConstrains.ContainsKey(args.FieldName) || args.FieldValue == null)
            {
                return;
            }

            var maxWidth = imageConstrains[args.FieldName].Width;
            var maxHeight = imageConstrains[args.FieldName].Height;

            using (var ms = new MemoryStream((byte[]) args.FieldValue))
            {
                if (ms.Length == 0)
                {
                    return;
                }

                var image = new Bitmap(ms);

                var ptSize = image.Size.PixelsToPoints();
                var width = ptSize.Width;
                var height = ptSize.Height;

                var hRatio = (float) height / maxHeight;
                var wRatio = (float) width / maxWidth;

                var ratio = Math.Max(1.0, Math.Max(hRatio, wRatio));

                args.ImageHeight = new MergeFieldImageDimension(
                    Math.Ceiling(height / ratio),
                    MergeFieldImageDimensionUnit.Point);
                args.ImageWidth = new MergeFieldImageDimension(
                    Math.Ceiling(width / ratio),
                    MergeFieldImageDimensionUnit.Point);
            }
        }
    }
}