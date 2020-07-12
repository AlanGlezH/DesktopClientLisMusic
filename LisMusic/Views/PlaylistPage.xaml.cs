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
    /// Interaction logic for PlaylistPage.xaml
    /// </summary>
    public partial class PlaylistPage : Page
    {
        public PlaylistPage()
        {
            InitializeComponent();
            var playlists = GetPlaylist();
            if(playlists.Count > 0)
            {
                Playlist playlist = new Playlist(1, "Playlist 1", "now", "/Resources/Img/Logo.png", false, 0, null, "victornino");
                
                ListViewPlaylists.ItemsSource = playlists;
            }
            
        }

        private List<Playlist> GetPlaylist()
        {
            return new List<Playlist>()
              {
                new Playlist(1,"Playlist 1","now","/Resources/Img/defaultPlaylistCover.jpeg",false,0,null,"victornino"),
                new Playlist(1,"Playlist 1","now","/Resources/Img/defaultPlaylistCover.jpeg",false,0,null,"victornino"),
                new Playlist(1,"Playlist 1","now","/Resources/Img/defaultPlaylistCover.jpeg",false,0,null,"victornino"),
                new Playlist(1,"Playlist 1","now","/Resources/Img/defaultPlaylistCover.jpeg",false,0,null,"victornino"),
                new Playlist(1,"Playlist 1","now","/Resources/Img/defaultPlaylistCover.jpeg",false,0,null,"victornino"),
                new Playlist(1,"Playlist 1","now","/Resources/Img/defaultPlaylistCover.jpeg",false,0,null,"victornino"),
                new Playlist(1,"Playlist 1","now","/Resources/Img/defaultPlaylistCover.jpeg",false,0,null,"victornino"),
              };

        }
    }
}
