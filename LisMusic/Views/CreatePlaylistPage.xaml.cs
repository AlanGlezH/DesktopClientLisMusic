using LisMusic.playlists;
using LisMusic.playlists.domain;
using LisMusic.Utils;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LisMusic.Views
{
    /// <summary>
    /// Interaction logic for CreatePlaylistPage.xaml
    /// </summary>
    public partial class CreatePlaylistPage : Page
    {
        private string idAccount;
        string absolutePathCover;
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
            string coverPlaylist = "";
            if(absolutePathCover != null)
            {
                coverPlaylist = Utils.Encoder.EncodeBase64(absolutePathCover);
            }
            Playlist playlist = new Playlist()
            {
                cover = coverPlaylist,
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

        private void Button_upload_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    string image = openFileDialog.FileName;
                    absolutePathCover = image;
                    Image_cover_playlist.Source = new BitmapImage(new Uri(image, UriKind.Absolute)); ;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Only images are accepted");

            }
        }
    }
}
