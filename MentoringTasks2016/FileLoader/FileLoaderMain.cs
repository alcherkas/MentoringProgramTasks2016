using System;
using System.Net;
using System.Threading;

namespace FileLoader
{
    public class FileLoaderMain
    {
        private static void DownloadFile(string[] args, bool async)
        {
            WebClient webClient = null;
            try
            {
                webClient = new WebClient();
                if (async)
                    webClient.DownloadFileAsync(new Uri(args[0]), args[1]);
                else
                    webClient.DownloadFile(new Uri(args[0]), args[1]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Parameters specified incorrenctly. Params list:");
                foreach (var s in args)
                    Console.WriteLine(s);
            }
            catch (ArgumentNullException ex) when (ex.ParamName.Contains("uriString")) // Uri constructor could throw
            {
                Console.WriteLine("Specified URI parameter is invalid.");
            }
            catch (ArgumentNullException ex) when (ex.ParamName.Contains("address")) // Download file async could throw
            {
                Console.WriteLine("Specified address is invalid.");
            }
            catch (ArgumentNullException ex) when (ex.ParamName.Contains("fileName")) // Download file async could throw
            {
                Console.WriteLine("Destination file path is invalid.");
            }
            catch (NotSupportedException) // ClearWebClientState
            {
                // ToDo: not async download.
                Console.WriteLine("Concurrent io is not allowed.");
                if (async)
                {
                    DownloadFile(args, false);
                }
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Something went wrong during file download.");
            }
            catch (StackOverflowException)
            {
                Console.WriteLine("Something went wrong during file download.");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Something went wrong during file download.");
            }
            finally
            {
                webClient?.Dispose();
            }

            Console.WriteLine("Download completed successfully.");
        }

        private static void Main(params string[] args)
        {
            DownloadFile(args, true);
            Console.ReadLine();
        }
    }
}