using LisMusic.personaltracks;
using LisMusic.personaltracks.domain;
using LisMusic.RpcService;
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
    /// Interaction logic for UploadPersonalTrackPage.xaml
    /// </summary>
    public partial class UploadPersonalTrackPage : Page
    {
        private string filePath;

        public UploadPersonalTrackPage()
        {
            InitializeComponent();
        }

        private void Button_upload_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Mp3 Files | *.mp3";

            if (openFileDialog.ShowDialog() == true) 
            {
                TextBlock_track_file.Text = openFileDialog.SafeFileName;
                filePath = openFileDialog.FileName;   
            }
        }
        private bool ValidateEmptyFields()
        {
            bool areValids = true;
            if (String.IsNullOrEmpty(TextBox_title_personal_track.Text))
            {
                areValids = false;
            }
            else if (String.IsNullOrEmpty(TextBox_album_personal_track.Text))
            {
                areValids = false;
            }
            else if (String.IsNullOrEmpty(TextBox_gender_personal_track.Text))
            {
                areValids = false;
            }

            return areValids;
        }
        

        private byte[] GetTrackBytes() {
            return File.ReadAllBytes(filePath);
        }

        private async void SavePersonalTrack()
        {
            
            PersonalTrack personalTrack = new PersonalTrack(null, SingletonSesion.GetSingletonSesion().account.idAccount, TextBox_title_personal_track.Text, TextBox_gender_personal_track.Text, TextBox_album_personal_track.Text, 100, null, false,0);
         
            try
            {
                personalTrack = await PersonalTrackRepository.CreatePersonalTrack(personalTrack);
                TrackAudio trackAudio = new TrackAudio()
                {
                    IdTrack = personalTrack.idPersonalTrack,
                    TrackName = personalTrack.title,
                    Audio = GetTrackBytes()
                };
                var result = await RpcStreamingService.UploadPersonalTrack(trackAudio);
                if (result)
                {
                    MessageBox.Show("Track uploaded");
                    Window.GetWindow(this).Close();
                } else
                {
                    MessageBox.Show("Connection error", "Please try again");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Button_upload_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateEmptyFields())
            {
                MessageBox.Show("Empty fields");
            }
            else if (String.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please select a file");
            } else
            {
                SavePersonalTrack();
            }
        }
    }
}
