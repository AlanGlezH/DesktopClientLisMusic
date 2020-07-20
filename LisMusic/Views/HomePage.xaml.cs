using LisMusic.albums.domain;
using LisMusic.musicgenders.domain;
using LisMusic.personaltracks;
using LisMusic.personaltracks.domain;
using LisMusic.player;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            LoadPersonalTracks();
        }

        public async void LoadPersonalTracks() 
        {
            try
            {
                List<PersonalTrack> personalTracks = await PersonalTrackRepository.GetPersonsalTracksAccount();

                foreach (var personalTrack in personalTracks)
                {
                    personalTrack.indexRow = personalTracks.IndexOf(personalTrack) + 1;
                }
                ListView_personal_tracks.ItemsSource = personalTracks;
            }
            catch ( Exception ex) 
            {
                MessageBox.Show(ex.Message, "Please reload");
            }
        }
        private void Button_history_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HistoryPage());
        }

        private void Button_upload_personal_track_Click(object sender, RoutedEventArgs e)
        {
            FloatingWindow floating = new FloatingWindow(new UploadPersonalTrackPage());
            floating.ShowDialog();
            LoadPersonalTracks();
        }

        private async void ListView_tracks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var personalTrack = (PersonalTrack)ListView_personal_tracks.SelectedItem;

            if (personalTrack != null)
            {
                Track trackAux = new Track(personalTrack.idPersonalTrack,personalTrack.title,0,0,personalTrack.fileTrack, personalTrack.avaialable, new MusicGender(),new Album());
                trackAux.album.cover = "defaultAlbumCover.jpeg";
                trackAux.album.artist.name = personalTrack.album;
                var result = await Player.UploadTrackAsync(trackAux);
                if (result)
                {
                    SingletonMainWindows.GetSingletonWindow().UpdateInfoPlayer(trackAux);
                }
            }
        }

        private void Button_add_queue_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            PersonalTrack personalTrack = button.DataContext as PersonalTrack;

            if (personalTrack != null)
            {
                Track trackAux = new Track(personalTrack.idPersonalTrack, personalTrack.title, 0, 0, personalTrack.fileTrack, personalTrack.avaialable, new MusicGender(), new Album());
                trackAux.album.cover = "defaultAlbumCover.jpeg";
                trackAux.album.artist.name = personalTrack.album;
                Player.AddTrackToQueue(trackAux);
            }
        }
    }
}
