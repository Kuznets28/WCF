using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary1;

namespace WcfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создание базового адреса для службы
            Uri baseAddress = new Uri("http://localhost:8000/Service1");

            // Создание хостинга службы
            using (ServiceHost host = new ServiceHost(typeof(Service1), baseAddress))
            {
                // Создание привязки с увеличенным размером сообщения
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxBufferSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                binding.ReaderQuotas.MaxArrayLength = 2147483647;
                binding.ReaderQuotas.MaxBytesPerRead = 2147483647;
                binding.ReaderQuotas.MaxDepth = 32;
                binding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
                binding.ReaderQuotas.MaxStringContentLength = 2147483647;

                // Добавление конечной точки
                host.AddServiceEndpoint(typeof(IService1), binding, "");

                // Открытие хостинга
                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Закрытие хостинга
                host.Close();
            }
        }
    }
}
