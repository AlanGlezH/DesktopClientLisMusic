using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;

namespace LisMusic.RpcService
{
    class RpcStreamingService
    {
        private static StreamingService.Client client;
        public static void Connect()
        {
            try
            {
                TTransport transport = new TSocketTransport("localhost", 8000);
                TProtocol protocol = new TBinaryProtocol(transport);
                client = new StreamingService.Client(protocol);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        public static async Task<byte[]> GetTrackAudio(string fileName)
        {
            TrackAudio trackAudio;
            try
            {
                trackAudio = await client.GetTrackAudioAsync(new TrackRequest() { FileName = fileName, Quality = Quality.LOW });
                Console.WriteLine("Recuperado" + trackAudio.Audio.Length);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return trackAudio.Audio;
        }

        public static async Task<TrackUploaded> UploadTrack(TrackAudio trackAudio)
        {
            TrackUploaded trackUploaded;
            try
            {
                trackUploaded = await client.UploadTrackAsync(trackAudio);
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return trackUploaded;
        }

        public static async Task<Boolean> UploadPersonalTrack(TrackAudio trackAudio)
        {
            
            try
            {
                await client.UploadPersonalTrackAsync(trackAudio);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }


}
