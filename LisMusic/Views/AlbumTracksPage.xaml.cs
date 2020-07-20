using LisMusic.albums.domain;
using LisMusic.player;
using LisMusic.tracks;
using LisMusic.tracks.domain;
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
    /// Interaction logic for AlbumTracks.xaml
    /// </summary>
    public partial class AlbumTracksPage : Page
    {
        private Album album;
        private List<Track> tracks;
        public AlbumTracksPage(Album album)
        {
            InitializeComponent();
            this.album = album;
            TextBlock_name_album.Text = album.title;
            TextBlock_artist_name.Text = album.artist.name;
            Image_cover_album.Source = album.coverImage;
            TextBlock_gender.Text = album.musicGender.genderName;
            LoadTracks();
        }

        public async void LoadTracks()
        {
            tracks = await TrackRepository.GetTracksAlbum(album.idAlbum);
            foreach (var track in tracks)
            {
                track.indexRow = tracks.IndexOf(track) + 1;
            }

            ListView_tracks.ItemsSource = tracks;
        }

        private void Button_back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }

        }

        private void Button_add_queue_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Track track = button.DataContext as Track;
            Player.AddTrackToQueue(track);
        }

        private void Button_generate_radio_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Track track = button.DataContext as Track;
            GenerateRadio(track);
        }

        private void Button_add_playlist_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Track track = button.DataContext as Track;
            FloatingWindow floating = new FloatingWindow(new AddToPlaylist(track));
            floating.ShowDialog();
        }
        private async void GenerateRadio(Track track)
        {
            try
            {
                var tracks = await TrackRepository.GetRadioTrack(track);
                Player.AddListTracksToQueue(tracks);
                MessageBox.Show("Gadio station generated: " + track.album.musicGender.genderName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async void ListView_tracks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Track track = (Track)ListView_tracks.SelectedValue;
            if (track != null)
            {
                var result = await Player.UploadTrackAsync(track);
                if (result)
                {
                    SingletonMainWindows.GetSingletonWindow().UpdateInfoPlayer(track);
                }
            }
        }

        private void Button_play_tracks_Click(object sender, RoutedEventArgs e)
        {
            Track.PlayListTracks(this.tracks);
        }
    }
}
