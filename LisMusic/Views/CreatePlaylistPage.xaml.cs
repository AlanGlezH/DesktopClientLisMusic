using LisMusic.playlists;
using LisMusic.playlists.domain;
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
    /// Interaction logic for CreatePlaylistPage.xaml
    /// </summary>
    public partial class CreatePlaylistPage : Page
    {
        private string idAccount;
        public CreatePlaylistPage()
        {
            InitializeComponent();
            idAccount = SingletonSesion.GetSingletonSesion().account.idAccount;
        }

        private void Button_create_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TextBox_title_playlist.Text))
            {
                MessageBox.Show("Please enter playlist title");
            } else
            {
                SavePlaylist();
            }
        }

        private async void SavePlaylist()
        {
            Playlist playlist = new Playlist()
            {
                cover = "DefaultPlaylistCover.jpg",
                idAccount = idAccount,
                idPlaylistType = 4,
                title = TextBox_title_playlist.Text,
                publicPlaylist = IsPublicPlaylist()
            };

            try
            {
                var response = await PlaylistRepository.CreatePlaylistAsync(playlist);
                if (response)
                {
                    MessageBox.Show("Playlist has been created");
                    Window.GetWindow(this).Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private bool IsPublicPlaylist()
        {
            bool isPublic = false;
            if (ToggleButton_public_playlist.IsChecked.Value)
            {
                isPublic = true;
            }

            return isPublic;
        }

        
    }
}
