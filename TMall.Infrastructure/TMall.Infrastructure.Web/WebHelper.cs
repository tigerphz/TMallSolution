using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Web;
using System.Web.Hosting;
using System.IO;

namespace TMall.Infrastructure.Web
{
    public class WebHelper : IWebHelper
    {
        private readonly HttpContextBase _httpContext;

        public WebHelper(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }

        public virtual string GetUrlReferrer()
        {
            string referrerUrl = string.Empty;

            //UrlReferrer在某些浏览器返回空 (比如 IE 8)
            if (_httpContext != null &&
                _httpContext.Request != null &&
                _httpContext.Request.UrlReferrer != null)
                referrerUrl = _httpContext.Request.UrlReferrer.PathAndQuery;

            return referrerUrl;
        }

        public virtual string GetCurrentIpAddress()
        {
            if (_httpContext != null &&
                _httpContext.Request != null &&
                _httpContext.Request.UserHostAddress != null)
                return _httpContext.Request.UserHostAddress;

            return string.Empty;
        }

        public virtual string GetCurrentHostName()
        {
            if (_httpContext != null &&
                _httpContext.Request != null &&
                _httpContext.Request.UserHostAddress != null)
                return _httpContext.Request.UserHostName;

            return string.Empty;
        }

        public virtual string GetThisPageUrl()
        {
            string url = string.Empty;
            if (_httpContext == null || _httpContext.Request == null)
                return url;

            if (_httpContext.Request.Url != null)
            {
                url = _httpContext.Request.Url.GetLeftPart(UriPartial.Path);
            }
            url = url.ToLowerInvariant();
            return url;
        }

        /// <summary>
        /// Gets a value indicating whether current connection is secured
        /// </summary>
        /// <returns>true - secured, false - not secured</returns>
        public virtual bool IsCurrentConnectionSecured()
        {
            bool useSsl = false;
            if (_httpContext != null && _httpContext.Request != null)
            {
                useSsl = _httpContext.Request.IsSecureConnection;
                //when your hosting uses a load balancer on their server then the Request.IsSecureConnection is never got set to true, use the statement below
                //just uncomment it
                //useSSL = _httpContext.Request.ServerVariables["HTTP_CLUSTER_HTTPS"] == "on" ? true : false;
            }

            return useSsl;
        }

        /// <summary>
        /// Gets server variable by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Server variable</returns>
        public virtual string ServerVariables(string name)
        {
            string result = string.Empty;

            try
            {
                if (_httpContext == null || _httpContext.Request == null)
                    return result;

                //put this method is try-catch 
                //as described here http://www.nopcommerce.com/boards/t/21356/multi-store-roadmap-lets-discuss-update-done.aspx?p=6#90196
                if (_httpContext.Request.ServerVariables[name] != null)
                {
                    result = _httpContext.Request.ServerVariables[name];
                }
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// Returns true if the requested resource is one of the typical resources that needn't be processed by the cms engine.
        /// </summary>
        /// <param name="request">HTTP Request</param>
        /// <returns>True if the request targets a static resource file.</returns>
        /// <remarks>
        /// These are the file extensions considered to be static resources:
        /// .css
        ///	.gif
        /// .png 
        /// .jpg
        /// .jpeg
        /// .js
        /// .axd
        /// .ashx
        /// </remarks>
        public virtual bool IsStaticResource(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            string path = request.Path;
            string extension = VirtualPathUtility.GetExtension(path);

            if (extension == null) return false;

            switch (extension.ToLower())
            {
                case ".axd":
                case ".ashx":
                case ".bmp":
                case ".css":
                case ".gif":
                case ".htm":
                case ".html":
                case ".ico":
                case ".jpeg":
                case ".jpg":
                case ".js":
                case ".png":
                case ".rar":
                case ".zip":
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public virtual string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }
            else
            {
                //not hosted. For example, run in unit tests
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
                return Path.Combine(baseDirectory, path);
            }
        }

        /// <summary>
        /// Get a value indicating whether the request is made by search engine (web crawler)
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <returns>Result</returns>
        public virtual bool IsSearchEngine(HttpContextBase context)
        {
            //we accept HttpContext instead of HttpRequest and put required logic in try-catch block
            //more info: http://www.nopcommerce.com/boards/t/17711/unhandled-exception-request-is-not-available-in-this-context.aspx
            if (context == null)
                return false;

            bool result = false;
            try
            {
                result = context.Request.Browser.Crawler;
                if (!result)
                {
                    //put any additional known crawlers in the Regex below for some custom validation
                    //var regEx = new Regex("Twiceler|twiceler|BaiDuSpider|baduspider|Slurp|slurp|ask|Ask|Teoma|teoma|Yahoo|yahoo");
                    //result = regEx.Match(request.UserAgent).Success;
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
            return result;
        }

        /// <summary>
        /// Gets a value that indicates whether the client is being redirected to a new location
        /// </summary>
        public virtual bool IsRequestBeingRedirected
        {
            get
            {
                var response = _httpContext.Response;
                return response.IsRequestBeingRedirected;
            }
        }
    }
}
