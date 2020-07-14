using LisMusic.accounts;
using LisMusic.ApiServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LisMusic.Media
{
    class MediaRepository
    {
        public static async Task<BitmapImage> GetImage()
        {
            string path = "media/playlists/defaultPlaylistCover.jpeg";

            BitmapImage bitmapImage = new BitmapImage();
            using (var response = await ApiServiceReader.ApiClient.GetAsync(path))
            {
                Console.WriteLine();
                if (response.IsSuccessStatusCode)
                {  
                    var byteArray = await response.Content.ReadAsByteArrayAsync();
                    MemoryStream memoryStream = new MemoryStream(byteArray);
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                }
            }
      
            return bitmapImage;
        }

       
    }
}
