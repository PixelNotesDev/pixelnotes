using ImageMagick;
using ImageMagick.Formats;

namespace WebPGenerator.Converters
{
    public class GifToWebPConverter : IImageConverterStrategy
    {
        public string FileExtension => "gif";

        public async Task ConvertAsync(string inputFilePath)
        {
            Console.WriteLine($"\nConvert {FileExtension} to webP: {Path.GetFileNameWithoutExtension(inputFilePath)}");

            using (var image = new MagickImageCollection(inputFilePath))
            {
                var defines = new WebPWriteDefines()
                {
                    Lossless = false,
                };

                using (var webpCollection = new MagickImageCollection())
                {
                    foreach (MagickImage frame in image)
                    {
                        frame.Quality = 80;
                        frame.Format = MagickFormat.WebP;
                        frame.RemoveProfile("!*");

                        webpCollection.Add(frame.Clone());
                    }

                    foreach (var im in webpCollection)
                    {
                        int delay = image[0].AnimationDelay;
                        im.AnimationDelay = delay;
                    }

                    string outputFilePath = Path.ChangeExtension(inputFilePath, ".webp");
                    await webpCollection.WriteAsync(outputFilePath, defines);
                }
            }
        }
    }
}
