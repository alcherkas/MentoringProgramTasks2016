using System;
using System.Web;

namespace Server
{
    public class CurrentTimeHandler : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write(DateTime.UtcNow.ToString("o"));
        }

        #endregion
    }
}
