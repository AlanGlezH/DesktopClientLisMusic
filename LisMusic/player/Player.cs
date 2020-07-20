using LisMusic.RpcService;
using LisMusic.tracks;
using LisMusic.tracks.domain;
using LisMusic.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisMusic.player
{
    class Player
    {
        public static WaveOutEvent waveOutEvent { set; get; }
        public static WaveStream waveStream  { set; get; }
        public static Queue<Track> queueTracks { set; get; }
        public static bool isTrackReady { set; get; }

        public Player()
        {
            
        }
        public static void Initialize()
        {
            waveOutEvent = new WaveOutEvent();
            isTrackReady = false;
            queueTracks = new Queue<Track>();
            RpcStreamingService.Connect();
        }

        public static async Task<bool> UploadTrackAsync(Track track)
        {
            try
            {
                byte[] bytes = await RpcStreamingService.GetTrackAudio(track.fileTrack);
                Player.StopPlayer();
                Mp3FileReader mp3Reader = new Mp3FileReader(new MemoryStream(bytes));
                waveStream = new WaveChannel32(mp3Reader);
                waveOutEvent.Init(waveStream);
                isTrackReady = true;
                Player.StartPlayer();
                TrackRepository.AddPlayToTrack(SingletonSesion.GetSingletonSesion().account.idAccount, track.idTrack);
                return true;

            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public static async Task<Track> UploadNextTrack()
        {
            if(queueTracks.Count > 0)
            {
                Player.StopPlayer();
                Track track = queueTracks.Dequeue();
                isTrackReady = false;
                if( await Player.UploadTrackAsync(track))
                {
                    return track;
                }
                else
                {
                    return null;
                }
                
            }
            else
            {
                return null;
            }
        }

        public static bool StartPlayer()
        {
            if (isTrackReady)
            {
                waveOutEvent.Play();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsTrackPlaying()
        {
            if (waveOutEvent.PlaybackState == PlaybackState.Playing)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool StopPlayer()
        {
            if (isTrackReady)
            {
                waveOutEvent.Stop();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void RestartTrack()
        {
            if (isTrackReady)
            {
                waveStream.Position = 0;

            }
        }

        public static void UpdateVolume(double volume)
        {
            if(waveOutEvent != null)
            {
                waveOutEvent.Volume = (Convert.ToSingle(volume)) / 100f;

            }
        }

        public static void UpdatePositionTrack(double position)
        {
            var totalSeconds = (position * waveStream.TotalTime.TotalSeconds) / 100;

            waveStream.CurrentTime = TimeSpan.FromSeconds(totalSeconds);
        }

        public static bool IsTrackOver()
        {
            if (waveStream.Position >= waveStream.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static double GetTotalSecondsTrack()
        {
            return waveStream.TotalTime.TotalSeconds;
        }

        public static double GetCurretTimeSeconds()
        {
            return waveStream.CurrentTime.TotalSeconds;
        }

        public static double GetCurretTimeForSlider()
        {
            return (waveStream.CurrentTime.TotalSeconds * 100) / waveStream.TotalTime.TotalSeconds;
        }
        public static void AddTrackToQueue(Track track)
        {
            queueTracks.Enqueue(track);
        }

        public static void AddListTracksToQueue(List<Track> tracks)
        {
            foreach (var item in tracks)
            {
                queueTracks.Enqueue(item);
            }
        }
    }
}
