using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading;
using System.Net.Cache;
using System.Net;

namespace Client
{
    [TestClass]
    public class TimeServerTest
    {
        string ServerBaseAddress = "http://localhost/Server/";

        [TestInitialize]
        public void Init()
        {
            var uriBuilder = new UriBuilder(ServerBaseAddress);
            if (uriBuilder.Host == "localhost")
                uriBuilder.Host = System.Net.Dns.GetHostName();

            ServerBaseAddress = uriBuilder.ToString();
        }

        void ReadDateTimeTestRun(HttpClient client, Uri uri)
        {
            for (int i = 0; i < 10; i++)
            {
                var result = client.GetStringAsync(uri).Result;
                var dateTime = DateTime.Parse(result);
                Console.WriteLine(dateTime.ToString("hh:mm:ss"));

                Thread.Sleep(1000);
            }
        }

        [TestMethod]
        public void NoCache()
        {
            var uri = new Uri(new Uri(ServerBaseAddress), "time");

            using (var client = new HttpClient(new HttpClientHandler() { Proxy = new WebProxy() }))
            {
                ReadDateTimeTestRun(client, uri);
            }
        }

        [TestMethod]
        public void ServerCache()
        {
            var uri = new Uri(new Uri(ServerBaseAddress), "time.t1");

            using (var client = new HttpClient(new HttpClientHandler()))
            {
                ReadDateTimeTestRun(client, uri);
            }
        }

        [TestMethod]
        public void ClientCache()
        {
            var uri = new Uri(new Uri(ServerBaseAddress), "cachecontrol.txt");

            using (var client = new HttpClient(
                new WebRequestHandler()
                {
                    UseProxy = true,
                    CachePolicy = new RequestCachePolicy(RequestCacheLevel.Default)
                }))
            {
                for (int i = 0; i < 10; i++)
                {                    
                    var result = client.GetStringAsync(uri).Result;
                    Console.WriteLine(result);
                    Thread.Sleep(1000);
                }
            }
        }

    }
}
