using LisMusic.albums.domain;
using LisMusic.musicgenders.domain;
using LisMusic.tracks.domain;
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
    /// Interaction logic for UploadTrack.xaml
    /// </summary>
    public partial class UploadTrack : Page
    {
        UploadAlbumPage uploadAlbum; 
        
        
        public UploadTrack(UploadAlbumPage page)
        {
            InitializeComponent();
            this.uploadAlbum = page;
        }

        private void Button_add_track_Click(object sender, RoutedEventArgs e)
        {
            Track trackAux = new Track(null, TextBox_title_track.Text, 0, 0, null, false, new MusicGender(), new Album());
            uploadAlbum.tracks.Add(trackAux);
            Window.GetWindow(this).Close();
        }

        private void Button_upload_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Mp3 Files | *.mp3";
            string path;

            if (openFileDialog.ShowDialog() == true)
            {
                TextBlock_track_file.Text = openFileDialog.SafeFileName;
                path = openFileDialog.FileName;
                uploadAlbum.filePaths.Add(path);
            }
        }
    }
}
