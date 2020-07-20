using LisMusic.albums;
using LisMusic.albums.domain;
using LisMusic.RpcService;
using LisMusic.tracks.domain;
using LisMusic.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        public List<String> filePaths;
        public List<Track> tracks;
        public string absolutePathCover;
        public UploadAlbumPage()
        {
            filePaths = new List<string>();
            tracks = new List<Track>();
            InitializeComponent();
        }

        private void LoadTracksTable()
        {
            ListView_tracks.ItemsSource = tracks;
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
            LoadTracksTable();
        }

        private void Button_create_album_Click(object sender, RoutedEventArgs e)
        {
            SaveAlbum();
        }

        private byte[] GetTrackBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }


        private async void SaveAlbum()
        {
            Album album = new Album()
            {
                title = TextBox_title_album.Text,
                cover = Utils.Encoder.EncodeBase64(absolutePathCover),
                publication = "2020-06-23",
                recordCompany = TextBox_company_album.Text,
                idMusicGender = 15,
                idAlbumType = GetAlbumType(),
                idArtist = SingletonArtist.GetSingletonArtist().idArtist,

            };

            album.tracks = this.tracks;
            album = await AlbumRepository.CreateAlbum(album);

            for (int i = 0; i < album.tracks.Count; i++)
            {
                TrackAudio trackAudio = new TrackAudio()
                {
                    IdTrack = album.tracks[i].idTrack,
                    TrackName = album.tracks[i].title,
                    Audio = GetTrackBytes(filePaths[i])
                };

                try
                {
                    await RpcStreamingService.UploadTrack(trackAudio);
                    MessageBox.Show("ALbum created");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Please relod");

                }
            }

        }

        public int GetAlbumType()
        {
            int albumType = 1;
            if (filePaths.Count > 1)
            {
                albumType = 2;
            }

            return albumType;
        }

        private void Button_upload_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {

                    absolutePathCover = openFileDialog.FileName;
                    Image_cover_album.Source = new BitmapImage(new Uri(absolutePathCover, UriKind.Absolute));
                        
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Only images are accepted");

            }
        }
    }
}

