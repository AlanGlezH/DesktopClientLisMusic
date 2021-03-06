﻿using LisMusic.ApiServices;
using LisMusic.artists;
using LisMusic.Media;
using LisMusic.personaltracks.domain;
using LisMusic.player;
using LisMusic.RpcService;
using LisMusic.tracks.domain;
using LisMusic.Utils;
using LisMusic.Views;
using NAudio.Utils;
using NAudio.Wave;
using System;
using System.Collections;
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
using System.Windows.Threading;

namespace LisMusic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer loadProgressTrackTimer;

        public MainWindow()
        {
            InitializeComponent();
            centralFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            ApiServiceReader.Initialize();
            Player.Initialize();
            InitializeWindow();


        }
        public async void InitializeWindow()
        {
            SingletonMainWindows.SetSingletonWindow(this);
            loadProgressTrackTimer = new DispatcherTimer();
            loadProgressTrackTimer.Tick += new EventHandler(PrintProgress);
            loadProgressTrackTimer.Interval = new TimeSpan(0, 0, 0, 1);
            if (SingletonSesion.GetSingletonSesion().account.contentCreator)
            {
                ItemCreator.Visibility = Visibility.Visible;
                if(await ArtistRepository.GetArtistOfAccount(SingletonSesion.GetSingletonSesion().account.idAccount))
                {
                    Console.WriteLine("Artist profile loaded: " + SingletonArtist.GetSingletonArtist().name );

                }
            }
        }

        public async void ReloadArtist()
        {
            if (SingletonSesion.GetSingletonSesion().account.contentCreator)
            {
                ItemCreator.Visibility = Visibility.Visible;
                if (await ArtistRepository.GetArtistOfAccount(SingletonSesion.GetSingletonSesion().account.idAccount))
                {
                    Console.WriteLine("Artist profile loaded: " + SingletonArtist.GetSingletonArtist().name);

                }
            }
        }

        public async void UpdateInfoPlayer(Track track)
        {
            string typeImage = "albums";
            TextBlock_track_name.Text = track.title;
            TextBlock_artist_name.Text = track.album.artist.name;
            StartTrack();
            track.album.coverImage = await MediaRepository.GetImage(track.album.cover, typeImage);
            image_cover_player.Source = track.album.coverImage;
        }



        private void PrintProgress(object sender, EventArgs e)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(Player.GetTotalSecondsTrack());
            TextBlock_final_duration.Text = string.Format("{0}:{1}", timeSpan.Duration().Minutes, timeSpan.Duration().Seconds);
            var time = Player.GetCurretTimeSeconds();
            TimeSpan timeInitial = TimeSpan.FromSeconds(time);
            TextBlock_initial_duration.Text = timeInitial.ToString(@"mm\:ss");
            Slider_track_duration.Value = Player.GetCurretTimeForSlider();
            if (Player.IsTrackOver())
            {
                StopTrack();
                GoToNextTrack();
            }
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemCreator":
                    centralFrame.Navigate(new ContentCreatorPage());
                    break;
                case "ItemHome":
                    centralFrame.Navigate(new HomePage());
                    break;
                case "ItemSearch":
                    centralFrame.Navigate(new SearchPage());
                    break;
                case "ItemPlaylist":
                    centralFrame.Navigate(new PlaylistPage());
                    break;
                case "ItemAlbums":
                    centralFrame.Navigate(new AlbumPage());
                    break;
                case "ItemArtists":
                    centralFrame.Navigate(new ArtistPage());
                    break;
                case "ItemHistory":
                    break;
                case "ItemExit":
                    LogOut();
                    break;
            }
        }

        private void LogOut()
        {
            SingletonSesion.CleanSingleton();
            SingletonMainWindows.CleanSingleton();
            SingletonArtist.CleanSingleton();
            StopTrack();
            RpcStreamingService.Disconnect();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void StopTrack()
        {
            if (Player.StopPlayer())
            {
                icon_playPause_button.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "Play");
                loadProgressTrackTimer.Stop();
            }
        }
        private void StartTrack()
        {
            if (Player.StartPlayer())
            {
                icon_playPause_button.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "Pause");
                loadProgressTrackTimer.Start();
            }
        }
        private void Button_track_playPause_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Player.IsTrackPlaying())
                {
                    StopTrack();
                }
                else
                {
                    StartTrack();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            
        }

        private void Button_track_previous_Click(object sender, RoutedEventArgs e)
        {
            Player.RestartTrack();
        }

        private void Slider_volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.UpdateVolume(Slider_volume.Value);
        }

        public async void GoToNextTrack()
        {
            Track track = await Player.UploadNextTrack();
            if (track != null)
            {
                UpdateInfoPlayer(track);
                StartTrack();
            }
        }

        public void Button_track_next_Click(object sender, RoutedEventArgs e)
        {
            GoToNextTrack();
        }

        private void Slider_track_duration_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.UpdatePositionTrack(Slider_track_duration.Value);
        }

        private void Buttom_view_queue_Click(object sender, RoutedEventArgs e)
        {
            centralFrame.Navigate(new ViewQueue());
        }

        private void Button_account_Click(object sender, RoutedEventArgs e)
        {
            centralFrame.Navigate(new AccountPage());
        }
    }
}
