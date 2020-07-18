﻿using LisMusic.RpcService;
using LisMusic.tracks.domain;
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
            byte[] bytes = await RpcStreamingService.GetTrackAudio(track.fileTrack);
            Player.StopPlayer();
            Mp3FileReader mp3Reader = new Mp3FileReader(new MemoryStream(bytes));
            waveStream = new WaveChannel32(mp3Reader);
            waveOutEvent.Init(waveStream);
            isTrackReady = true;
            Player.StartPlayer();
            return true;
        }
        public static async Task<Track> UploadNextTrack()
        {
            if(queueTracks.Count > 0)
            {
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
            waveStream.Position = 0;
        }

        public static void UpdateVolume(double volume)
        {
            if(waveOutEvent != null)
            {
                waveOutEvent.Volume = (Convert.ToSingle(volume)) / 100f;

            }
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
    }
}
