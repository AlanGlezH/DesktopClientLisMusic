using System;
using System.IO;
using System.Drawing;

namespace LisMusic.Utils
{
    public static class Encoder
    {
        public static string EncodeBase64(string path)
        {

            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}
