using LisMusic.albums;
using LisMusic.albums.domain;
using LisMusic.Media;
using LisMusic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LisMusic.Views
{
    /// <summary>
    /// Interaction logic for ContentCreatorPage.xaml
    /// </summary>
    public partial class ContentCreatorPage : Page
    {
        public ContentCreatorPage()
        {
            InitializeComponent();
            TextBlock_name_artist.Text = SingletonArtist.GetSingletonArtist().name;
            TextBlock_description.Text = SingletonArtist.GetSingletonArtist().description;
            Image_cover_artist.Source = SingletonArtist.GetSingletonArtist().coverImage;
            LoadAlbums();

        }



        public async void LoadAlbums()
        {
            try
            {
                var albums = await AlbumRepository.GetArtistAlbums(SingletonArtist.GetSingletonArtist().idArtist);
                string typeImage = "albums";
                foreach (var album in albums)
                {
                    album.coverImage = await MediaRepository.GetImage(album.cover, typeImage);
                }
                ListView_albums.ItemsSource = albums;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ScrollViewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var album = (Album)ListView_albums.SelectedItem;
            if (album != null)
            {
                NavigationService.Navigate(new AlbumTracksPage(album));
            }
        }

        private void Button_upload_album_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UploadAlbumPage());
        }
    }

}
