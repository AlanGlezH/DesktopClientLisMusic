using LisMusic.RpcService;
using LisMusic.Views;
using NAudio.Wave;
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
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;

namespace LisMusic
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            centralFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            centralFrame.Navigate(new LoginPage());
            //RpcStreamingService.Connect();

            //TestRpc();


        }

        private async void TestRpc()
        {
            
            byte[] bytes = await RpcStreamingService.GetTrackAudio("track1");
            var mp3Reader = new Mp3FileReader(new MemoryStream(bytes));
            WaveStream wave32 = new WaveChannel32(mp3Reader);
            WaveOutEvent _waveOutEvent = new WaveOutEvent();
            _waveOutEvent.Init(wave32);
            _waveOutEvent.Play();
        }
    }


}
