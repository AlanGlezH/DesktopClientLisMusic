using LisMusic.Media;
using LisMusic.playlists;
using LisMusic.playlists.domain;
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
    /// Interaction logic for AddToPlaylist.xaml
    /// </summary>
    public partial class AddToPlaylist : Page
    {
        public AddToPlaylist()
        {
            InitializeComponent();
            LoadPlaylists();
        }

        private async void LoadPlaylists()
        {
            try
            {
                string typeImage = "playlists";
                List<Playlist> playlists = await PlaylistRepository.GetPlaylistsOfAccount();
                foreach (var playlist in playlists)
                {
                    playlist.coverImage = await MediaRepository.GetImage(playlist.cover, typeImage);
                }
                ListViewPlaylists.ItemsSource = playlists;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
        private void ListViewPlaylists_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            MessageBox.Show("Added to playlist");
        }
    }
}
