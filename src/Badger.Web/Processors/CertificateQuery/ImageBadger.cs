using ImageMagick;
using System.Collections.Generic;

namespace Badger.Web.Processors.CertificateQuery
{
    public class ImageBadger
    {
        public List<LabeleldValue> Text { get; set; } = new List<LabeleldValue>();


        public byte[] Create(List<LabeleldValue> values)
        {
            var lineHeight = 12;
            var height = values.Count * lineHeight + 4;
            using (var image = new MagickImage(new MagickColor("#56667A"), 260, height))
            {
                image.Depth = 64;

                var magic = new Drawables()
                .FontPointSize(12)
                .Font("Courier New")
                .FillColor(new MagickColor("white"))
                .TextAlignment(TextAlignment.Left);

                for (int i = 0; i < values.Count; i++)
                {
                    magic.Text(2, 12 * (i + 1), $"{values[i].Label}: {values[i].Value}");
                }
                magic.Draw(image);


                return image.ToByteArray(MagickFormat.Png64);
            }
        }
    }
}
