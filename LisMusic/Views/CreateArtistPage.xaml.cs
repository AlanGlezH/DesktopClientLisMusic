using LisMusic.artists;
using LisMusic.artists.domain;
using LisMusic.Utils;
using Microsoft.Win32;
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
    /// Interaction logic for CreateArtistPage.xaml
    /// </summary>
    public partial class CreateArtistPage : Page
    {
        private string idAccount;
        string absolutePathCover;
        public CreateArtistPage()
        {
            InitializeComponent();
            idAccount = SingletonSesion.GetSingletonSesion().account.idAccount;
        }

        private void Button_upload_cover_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    string image = openFileDialog.FileName;
                    absolutePathCover = image;
                    Image_cover_artist.Source = new BitmapImage(new Uri(image, UriKind.Absolute)); ;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Only images are accepted");

            }
        }

        private void Button_create_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TextBox_name_artist.Text))
            {
                MessageBox.Show("Please enter artist name");
            }
            else if(String.IsNullOrEmpty(TextBox_description_artist.Text))
            {
                MessageBox.Show("Please enter artist description");
            }
            else if(String.IsNullOrEmpty(absolutePathCover))
            {
                MessageBox.Show("Please enter artist cover");
            }
            else
            {
                SaveArtist();

            }
        }

        private async void SaveArtist()
        {
            string coverArtist = "";
            if (absolutePathCover != null)
            {
                coverArtist = Utils.Encoder.EncodeBase64(absolutePathCover);
            }
            Artist artist = new Artist();
            artist.name = TextBox_name_artist.Text;
            artist.description = TextBox_description_artist.Text;
            artist.cover = coverArtist;
            artist.idAccount = idAccount;
            try
            {
                if (await ArtistRepository.CreateArtist(artist))
                {
                    SingletonMainWindows.GetSingletonWindow().ItemCreator.Visibility = Visibility.Visible;
                    SingletonSesion.GetSingletonSesion().account.contentCreator = true;
                    MessageBox.Show("Artist created");
                    Window.GetWindow(this).Close();
                }
                else
                {
                    MessageBox.Show("Error to create artist");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
