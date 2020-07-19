using LisMusic.albums;
using LisMusic.albums.domain;
using LisMusic.artists;
using LisMusic.artists.domain;
using LisMusic.Media;
using LisMusic.playlists;
using LisMusic.playlists.domain;
using LisMusic.tracks;
using LisMusic.tracks.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LisMusic.Views
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {

        public SearchPage()
        {
            InitializeComponent();
    
           
        }

        private void HiddenLists()
        {
            ListView_tracks.Visibility = Visibility.Hidden;
            ListView_artists.Visibility = Visibility.Hidden;
            ListView_albums.Visibility = Visibility.Hidden;
            ListView_playlists.Visibility = Visibility.Hidden;
            ScrollViewer_albums.Visibility = Visibility.Hidden;
            ScrollViewer_artists.Visibility = Visibility.Hidden;
            ScrollViewer_playlists.Visibility = Visibility.Hidden;
        }

        public async void SearchArtists()
        {
            try
            {
                ListView_artists.Visibility = Visibility.Visible;
                ScrollViewer_artists.Visibility = Visibility.Visible;
                string typeImage = "artists";
                List<Artist> artists  = await ArtistRepository.SearchArtist(TextBox_search.Text);
                foreach (var artist in artists)
                {
                    artist.coverImage = await MediaRepository.GetImage(artist.cover, typeImage);
                }
                ListView_artists.ItemsSource = artists;

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        public async void SearchAlbums()
        {
            try
            {
                ListView_albums.Visibility = Visibility.Visible;
                ScrollViewer_albums.Visibility = Visibility.Visible;
                string typeImage = "albums";
                List<Album> albums = await AlbumRepository.SearchAlbums(TextBox_search.Text);
                foreach (var album in albums)
                {
                    album.coverImage = await MediaRepository.GetImage(album.cover, typeImage);
                }
                ListView_albums.ItemsSource = albums;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        public async void SearchTracks()
        {
            try
            {
                ListView_tracks.Visibility = Visibility.Visible;
                List<Track> tracks = await TrackRepository.SearchTracks(TextBox_search.Text);
                ListView_tracks.ItemsSource = tracks;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please try again");
            }
        }

        public async void SearchPlaylists()
        {
            try
            {
                ListView_playlists.Visibility = Visibility.Visible;
                ScrollViewer_playlists.Visibility = Visibility.Visible;
                string typeimage = "playlists";
                List<Playlist> playlists = await PlaylistRepository.SearchPlaylists(TextBox_search.Text);
                foreach (var playlist in playlists)
                {
                    playlist.coverImage = await MediaRepository.GetImage(playlist.cover, typeimage);
                }
                ListView_playlists.ItemsSource = playlists;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "please try again");
            }
        }

        private bool ValidateEmptyField()
        {
            bool isValid = true;
            if (String.IsNullOrEmpty(TextBox_search.Text))
            {
                MessageBox.Show("Empty field");
                isValid = false;
            }
            return isValid;
        }

        private void Button_search_Click(object sender, RoutedEventArgs e)
        {
            string opcion = ComboBox_filter.Text;
            Console.WriteLine(opcion);

            if (ValidateEmptyField())
            {
                switch (opcion)
                {
                    case "Tracks":
                        HiddenLists();
                        SearchTracks();
                        break;
                    case "Artists":
                        HiddenLists();
                        SearchArtists();
                        break;
                    case "Albums":
                        HiddenLists();
                        SearchAlbums();
                        break;
                    case "Playlists":
                        HiddenLists();
                        SearchPlaylists();
                        break;
                }
            } 
        }

        private void ScrollViewer_artists_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Artist artist = (Artist)ListView_artists.SelectedItem;
            if (artist != null)
            {
                NavigationService.Navigate(new ArtistAlbumsPage(artist));
            }
        }

        private void ScrollViewer_albums_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var album = (Album)ListView_albums.SelectedItem;
            if (ListView_albums.SelectedValue != null)
            {
                NavigationService.Navigate(new AlbumTracksPage(album));
            }
        }

        private void ScrollViewer_playlists_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var playlist = (Playlist)ListView_playlists.SelectedValue;
            if (playlist != null)
            {
                NavigationService.Navigate(new PlaylistTracksPage(playlist));
            }
        }
    }
}
