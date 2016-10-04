using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GrabMySite
{
    public class Grabber
    {
        private readonly string _sourceUrl;

        public Grabber(string sourceUrl)
        {
            _sourceUrl = sourceUrl;
        }

        public async Task Grab()
        {
            var client = new WebClient();
            await Grab(client, _sourceUrl, 0, 1);
        }

        private async Task Grab(WebClient client, string address, int currentDepth, int allowedDepth)
        {
            if (!IsAddressExists(address)) return;

            string html = client.DownloadString(address);


            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var parentUrl = GetParentUrl(address);
            await Task.Run(() => WriteContentToFile(address, html, parentUrl));

            foreach (HtmlNode item in htmlDocument.DocumentNode.SelectNodes("//a[@href" + "]"))
            {
                var attributeValue = item.GetAttributeValue("href", string.Empty);
                var nextUrl = parentUrl + attributeValue;

                if (currentDepth <= allowedDepth)
                {
                    await Task.Run(() => Grab(client, nextUrl, currentDepth + 1, allowedDepth));
                }
            }
        }

        private static void WriteContentToFile(string address, string html, string parentUrl)
        {
            var fileName = GetFileName(address, parentUrl);

            var path = GetFolderPath(parentUrl);

            var filePath = Path.Combine(path, fileName);

            Directory.CreateDirectory(path);
            File.WriteAllText(filePath, html);
        }

        private readonly Dictionary<string, bool> _verifiedAddresses = new Dictionary<string, bool>();

        private bool IsAddressExists(string address)
        {
            bool isExists;
            var isAddressCached = _verifiedAddresses.TryGetValue(address, out isExists);

            if (isAddressCached) return isExists;

            isExists = RemoteFileExists(address);
            _verifiedAddresses.Add(address, isExists);
            return isExists;
        }

        private bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

        private static string GetFolderPath(string parentUrl)
        {
            var startIndex = parentUrl.IndexOf("//");
            var startIndexToCopy = parentUrl.IndexOf('/', startIndex + 2) + 1;
            char[] folders = new char[parentUrl.Length - startIndexToCopy];
            parentUrl.CopyTo(startIndexToCopy, folders, 0, folders.Length);
            var foldersString = new string(folders);

            var path = Path.Combine(Environment.CurrentDirectory, foldersString);

            return path;
        }

        private static string GetFileName(string url, string parentUrl)
        {
            return url.Replace(parentUrl, string.Empty).Replace("/", string.Empty);
        }

        private static string GetParentUrl(string url)
        {
            int lastIndex = url.LastIndexOf('/');
            var parentUrl = new char[lastIndex];
            url.CopyTo(0, parentUrl, 0, lastIndex);
            return new string(parentUrl);
        }
    }
}
