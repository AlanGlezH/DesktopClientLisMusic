using LisMusic.albums;
using LisMusic.albums.domain;
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
    /// Interaction logic for AlbumPage.xaml
    /// </summary>
    public partial class AlbumPage : Page
    {
        public AlbumPage()
        {
            InitializeComponent();
            LoadAlbums();
           
        }

        public async void LoadAlbums()
        {
            try
            {
                ListViewAlbums.ItemsSource = await AlbumRepository.GetAlbumsLikeOfAccount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Please reload");
            }
        }

        private void ListViewAlbums_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var album = (Album)ListViewAlbums.SelectedItem;
            if (ListViewAlbums.SelectedValue != null)
            {
                NavigationService.Navigate(new AlbumTracksPage(album));
            }
        }
    }
}
