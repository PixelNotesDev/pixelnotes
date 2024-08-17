using ImageMagick;
using ImageMagick.Formats;

namespace WebPGenerator.Converters
{
    public class PngToWebPConverter : IImageConverterStrategy
    {
        public string FileExtension => "png";

        public async Task ConvertAsync(string inputFilePath)
        {
            Console.WriteLine($"\nConvert {FileExtension} to webP:{Path.GetFileNameWithoutExtension(inputFilePath)}");

            using (var image = new MagickImage(inputFilePath))
            {
                image.Format = MagickFormat.WebP;
                image.Quality = 80;
                image.RemoveProfile("!*");

                var defines = new WebPWriteDefines()
                {
                    Lossless = false,
                };

                string outputFilePath = Path.ChangeExtension(inputFilePath, ".webp");
                await image.WriteAsync(outputFilePath, defines);
            }
        }
    }

}
