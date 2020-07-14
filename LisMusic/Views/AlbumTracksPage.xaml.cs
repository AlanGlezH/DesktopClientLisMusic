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
    /// Interaction logic for AlbumTracks.xaml
    /// </summary>
    public partial class AlbumTracksPage : Page
    {
        public AlbumTracksPage(Album album)
        {
            InitializeComponent();
            TextBlock_name_album.Text = album.title;
            TextBlock_artist_name.Text = album.artistName;
            Image_cover_album.Source = album.coverImage;
        }
    }
}
