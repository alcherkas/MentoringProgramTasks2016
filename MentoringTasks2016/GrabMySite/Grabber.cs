using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace GrabMySite
{
    public enum GrabbingWidth
    {
        Unlimited,
        Domain,
        UnderLink
    }

    public class Grabber
    {
        private const string LongPathPrefix = @"";
        private readonly IProgress<string> _progress;
        private readonly string _path;
        private readonly GrabbingWidth _grabbingWidth;
        private readonly string _url;

        private string _root;
        private string _extensionsRegex;

        public Grabber(string url, string path, GrabbingWidth grabbingWidth, IProgress<string> progress)
        {
            _url = url;
            _path = path;
            _grabbingWidth = grabbingWidth;
            Extensions = "*";
            Depth = null;
            _progress = progress;
        }

        public string Extensions { set; get; }

        public int? Depth { set; get; }

        public void Grab()
        {
            var url = new Uri(_url);
            _extensionsRegex = $@"^.*\.({Extensions.Replace(",", "|").Replace("*", ".*")})$";

            PrepareDirectory(url);

            using (HttpClient client = new HttpClient())
            {
                GrabPage(client, url);
                _progress.Report("Done");
            }
        }

        private void GrabPage(HttpClient client, Uri url, int deep = 0)
        {
            _progress.Report("Page processing: " + url);
            if (!IsAddressExists(url.OriginalString)) return;

            using (HttpResponseMessage response = client.GetAsync(url).Result)
            using (HttpContent content = response.Content)
            {
                string result = content.ReadAsStringAsync().Result;
                if (result != null)
                {
                    var fileName = GetFileName(url);
                    HtmlDocument hap = new HtmlDocument();
                    hap.LoadHtml(result);

                    DownloadResources(hap);
                    IEnumerable<Uri> links = new List<Uri>();
                    if (Depth.HasValue && Depth > deep)
                    {
                        links = ProcessLinks(hap);
                    }

                    SavePage(fileName, hap.DocumentNode.OuterHtml);

                    foreach (var link in links)
                    {
                        fileName = GetFileName(link);
                        if (!File.Exists(fileName))
                        {
                            GrabPage(client, link, ++deep);
                        }
                    }
                }
            }
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
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                if (request == null) return false;

                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response?.Close();
                return (response?.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        private void SavePage(string fileName, string page)
        {
            try
            {
                fileName = Regex.Replace(fileName, @"(.asp)$", ".html");
                FileInfo file = new FileInfo(fileName);
                file.Directory?.Create();
                File.WriteAllText(fileName, page);
            }
            catch (Exception e)
            {
                _progress.Report(e.Message);
            }
        }

        private void PrepareDirectory(Uri url)
        {
            var directoryName = url.Host + url.LocalPath.Replace('/', '\\');
            var fullDirectoryName = Path.Combine(_path, directoryName);
            if (Directory.Exists(fullDirectoryName))
            {
                Directory.Delete(fullDirectoryName, true);
            }

            Directory.CreateDirectory(fullDirectoryName);
            _root = fullDirectoryName;
        }

        private string GetFileName(Uri url)
        {
            string fileName;
            if (url.IsAbsoluteUri)
            {
                fileName = url.LocalPath.Trim("/".ToCharArray()).Replace('/', '\\');
                fileName = (string.IsNullOrEmpty(fileName) ? "index" : fileName) + url.Query.Replace('?', '!');
            }
            else
            {
                fileName = url.OriginalString.Trim("/".ToCharArray()).Replace('/', '\\');
            }

            fileName = Regex.Replace(fileName, @"[#].*$", "");

            if (!Regex.IsMatch(fileName, @"[.]\w+$"))
            {
                fileName = (string.IsNullOrEmpty(fileName) ? "index" : fileName) + ".html";
            }

            return LongPathPrefix + Path.Combine(_root, fileName);
        }


        private IEnumerable<Uri> ProcessLinks(HtmlDocument document)
        {
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//a");
            var result = new List<Uri>();
            if (nodes != null)
            {
                foreach (var htmlNode in nodes)
                {
                    var link = htmlNode.GetAttributeValue("href", null);
                    Uri url;
                    if (!string.IsNullOrEmpty(link) && !link.StartsWith("http"))
                    {
                        var rootUrl = new Uri(_url);
                        if (link.StartsWith(@"/"))
                        {
                            link = rootUrl.Scheme + "://" + rootUrl.Host + link;
                        }
                    }
                    if (Uri.TryCreate(link, UriKind.Absolute, out url))
                    {
                        if (!CheckLinkWidth(url))
                        {
                            continue;
                        }
                        var newLink = GetFileName(url);

                        newLink = Regex.Replace(newLink, @"(.asp)$", ".html");

                        htmlNode.SetAttributeValue("href", "file:///" + newLink);
                        result.Add(url);
                    }
                }
            }

            return result;
        }

        private bool CheckLinkWidth(Uri link)
        {
            switch (_grabbingWidth)
            {
                case GrabbingWidth.Domain:
                    var rootUrl = new Uri(_url);
                    return link.Host.Equals(rootUrl.Host);
                case GrabbingWidth.UnderLink:
                    return link.AbsolutePath.StartsWith(_url);
                default:
                    return true;
            }
        }

        private void DownloadResources(HtmlDocument document)
        {
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//img");

            _progress.Report("\tResources downloading...");
            if (nodes != null)
            {
                using (var client = new WebClient())
                {
                    foreach (var htmlNode in nodes)
                    {
                        DownloadResource(htmlNode, client, "src");
                    }
                }
            }

            var links = document.DocumentNode.SelectNodes("//link");
            if (links != null)
            {
                using (var client = new WebClient())
                {
                    foreach (var htmlNode in links)
                    {
                        DownloadResource(htmlNode, client, "href");
                    }
                }
            }

        }

        private void DownloadResource(HtmlNode htmlNode, WebClient client, string attributeName)
        {
            var link = htmlNode.GetAttributeValue(attributeName, null);
            Uri url;
            if (Uri.TryCreate(link, UriKind.RelativeOrAbsolute, out url))
            {
                if (!Regex.IsMatch(link, _extensionsRegex))
                {
                    return;
                }

                var newLink = GetFileName(url);

                if (link.StartsWith("/"))
                    link = _url.TrimEnd('/') + link;
                try
                {
                    FileInfo file = new FileInfo(newLink);
                    file.Directory?.Create();
                    if (!File.Exists(newLink))
                    {
                        _progress.Report(link);
                        client.DownloadFile(link, newLink);
                    }

                    htmlNode.SetAttributeValue(attributeName, "file:///" + newLink);
                }
                catch (Exception e)
                {
                    _progress.Report(e.Message);
                }
            }
        }
    }
}
