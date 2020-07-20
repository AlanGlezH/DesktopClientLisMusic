using LisMusic.player;
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
    /// Interaction logic for ViewQueue.xaml
    /// </summary>
    public partial class ViewQueue : Page
    {
        public ViewQueue()
        {
            InitializeComponent();
            LoadTracks();
        }

        public void LoadTracks()
        {
            try
            {
                ListView_tracks_queue.ItemsSource = Player.queueTracks;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Button_go_back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
