using LisMusic.albums;
using LisMusic.albums.domain;
using LisMusic.artists.domain;
using LisMusic.Media;
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
    /// Interaction logic for ArtistAlbumsPage.xaml
    /// </summary>
    public partial class ArtistAlbumsPage : Page
    {
        private Artist artist;
        private List<Album> albums;
        public ArtistAlbumsPage(Artist artist)
        {
            InitializeComponent();
            this.artist = artist;
            TextBlock_name_artist.Text = artist.name;
            TextBlock_description.Text = artist.description;
            Image_cover_artist.Source = artist.coverImage;
            LoadAlbums();
        }

        public async void LoadAlbums()
        {
            try
            {
                albums = await AlbumRepository.GetArtistAlbums(artist.idArtist);
                string typeImage = "albums";
                foreach (var album in albums)
                {
                    album.coverImage = await MediaRepository.GetImage(album.cover, typeImage);
                }
                ListView_albums.ItemsSource = albums;
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
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
    }
}
