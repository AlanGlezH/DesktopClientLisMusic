﻿using LisMusic.player;
using LisMusic.tracks;
using LisMusic.tracks.domain;
using LisMusic.Utils;
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
    /// Interaction logic for HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        List<Track> tracks;
        public HistoryPage()
        {
            InitializeComponent();
            LoadHistory();
        }

        public async void LoadHistory()
        {
            try
            {
                tracks = await TrackRepository.GetTrackAccountHistory();
                ListView_tracks.ItemsSource = tracks;
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message, "Please reload");
            }
            
        }

        private void Button_add_queue_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Track track = button.DataContext as Track;
            Player.AddTrackToQueue(track);
        }

        private void Button_generate_radio_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Track track = button.DataContext as Track;
            GenerateRadio(track);
        }
        private async void GenerateRadio(Track track)
        {
            try
            {
                var tracks = await TrackRepository.GetRadioTrack(track);
                Player.AddListTracksToQueue(tracks);
                MessageBox.Show("Gadio station generated: " + track.album.musicGender.genderName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private void Button_add_playlist_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Track track = button.DataContext as Track;
            FloatingWindow floating = new FloatingWindow(new AddToPlaylist(track));
            floating.ShowDialog();
        }

        private void Button_play_tracks_Click(object sender, RoutedEventArgs e)
        {
            Track.PlayListTracks(this.tracks);
        }

        private async void ListView_tracks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Track track = (Track)ListView_tracks.SelectedValue;
            if (track != null)
            {
                var result = await Player.UploadTrackAsync(track);
                if (result)
                {
                    SingletonMainWindows.GetSingletonWindow().UpdateInfoPlayer(track);
                }
            }
        }
    }
}
