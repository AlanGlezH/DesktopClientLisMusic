using LisMusic.ApiServices;
using LisMusic.artists;
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
    /// Interaction logic for ArtistPage.xaml
    /// </summary>
    public partial class ArtistPage : Page
    {
        List<Artist> artists;
        public ArtistPage()
        {
            InitializeComponent();
            ApiServiceReader.Initialize();
            LoadArtists();

        }

        public async void LoadArtists()
        {
            try

            {
                string typeImage = "artists";
                artists = await ArtistRepository.GetArtistsOfAccount();
                foreach (var artist in artists)
                {
                    artist.coverImage = await MediaRepository.GetImage(artist.cover,typeImage);
                }
                ListView_artists.ItemsSource = artists;
               
            }
            catch (Exception)
            {

                MessageBox.Show("Please reload");
            }
           
        }

        private void ScrollViewer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Artist artist = (Artist)ListView_artists.SelectedItem;
            if(artist != null)
            {
                NavigationService.Navigate(new ArtistAlbumsPage(artist));
            }
        }
    }
}
