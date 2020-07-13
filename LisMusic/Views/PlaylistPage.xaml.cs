using LisMusic.playlists;
using LisMusic.playlists.domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for PlaylistPage.xaml
    /// </summary>
    public partial class PlaylistPage : Page
    {
        public PlaylistPage()
        {
            InitializeComponent();
                       
            ListViewPlaylists.ItemsSource = PlaylistRepository.GetPlaylistsOfAccount();
                          
        }

        private void Button_create_playlist_Click(object sender, RoutedEventArgs e)
        {
            FloatingWindow floating = new FloatingWindow();
            floating.ShowDialog();
            ListViewPlaylists.ItemsSource = PlaylistRepository.GetPlaylistsOfAccount();

        }
    }
}
