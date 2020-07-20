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
    /// Interaction logic for UploadAlbumPage.xaml
    /// </summary>
    public partial class UploadAlbumPage : Page
    {
        public UploadAlbumPage()
        {
            InitializeComponent();
        }

        private void Button_back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void Button_add_track_Click(object sender, RoutedEventArgs e)
        {
            FloatingWindow floating = new FloatingWindow(new UploadTrack(this));
            floating.ShowDialog();
        }
    }
}
