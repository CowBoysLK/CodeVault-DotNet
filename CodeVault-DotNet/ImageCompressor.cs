using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace CodeVault_DotNet
{
    class ImageCompressor
    {
        const int sizeConst = 750; // width in pixels 




        const int quality = 80;

        const string inputPath = @"D:\output\imageData\large.jpg";

        const string outputPath = @"D:\output\imageData\compressed.jpg";


        // compress image and save it 
        public void CompressImage()
        {

            using (Image image = Image.Load(inputPath))
            {
                // passing 0 to the heigth will preserve the original aspect ratio
                image.Mutate(i => i.Resize(sizeConst, 0));
                image.Save(outputPath);
            }

        }

        // compress a image an return it 
        private MemoryStream CompressImage(MemoryStream imageStream)
        {
            const string extension = "jpg";
            using (Image image = Image.Load(imageStream))
            {
                try
                {

                    IImageEncoder imageEncoder;
                    switch (extension)
                    {
                        case "png":
                            imageEncoder = new PngEncoder();
                            break;
                        case "jpg":
                            imageEncoder = new JpegEncoder();
                            break;
                        case "jpeg":
                            imageEncoder = new JpegEncoder();
                            break;

                        default:
                            imageEncoder = new JpegEncoder();
                            break;
                    }

                    // passing 0 to the heigth will preserve the original aspect ratio
                    image.Mutate(i => i.Resize(sizeConst, 0));

                    var ms = new MemoryStream();


                    image.Save(ms, imageEncoder);

                    return ms;




                }

                catch (ImageProcessingException ex)
                {
                    Console.WriteLine("Error in processing the image");
                    return null;
                }

                catch (Exception ex)
                {
                    Console.WriteLine("erro in compressing image");
                    return null;
                }

            }
        }
    }
}
