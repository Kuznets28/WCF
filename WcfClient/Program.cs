using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfClient.ServiceReference1;

namespace WcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxBufferSize = 2147483647;
            binding.MaxReceivedMessageSize = 2147483647;
            binding.ReaderQuotas.MaxArrayLength = 2147483647;
            binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
            binding.ReaderQuotas.MaxDepth = 32;
            binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;


            var client = new Service1Client(binding, new EndpointAddress("http://localhost:8000/Service1"));

            // Генерация данных объемом 1 ГБ
            byte[] data = new byte[1024 * 1024 * 1024];
            new Random().NextBytes(data);

            // Размер части данных (например, 10 МБ)
            int chunkSize = 10 * 1024 * 1024;
            int offset = 0;

            // Измерение времени передачи данных
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (offset < data.Length)
            {

                int currentChunkSize = Math.Min(chunkSize, data.Length - offset);


                byte[] dataChunk = new byte[currentChunkSize];
                Array.Copy(data, offset, dataChunk, 0, currentChunkSize);


                string response = client.SendData(dataChunk);



                offset += currentChunkSize;
            }

            stopwatch.Stop();


            Console.WriteLine($"Measured transfer time: {stopwatch.ElapsedMilliseconds} ms");


            client.Close();
        }
    }
}