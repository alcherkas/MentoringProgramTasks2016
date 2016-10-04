using GrabMySite;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GrabMySite.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = "http://www.w3schools.com/xsl/xpath_syntax.asp";
            var grabber = new Grabber(address);
            Task.WaitAny(grabber.Grab());
        }


    }
}
