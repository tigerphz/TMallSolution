using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TMall.Infrastructure.Web
{
    public interface IWebHelper
    {
        string GetUrlReferrer();

        /// <summary>
        /// 获取ip
        /// </summary>
        /// <returns></returns>
        string GetCurrentIpAddress();

        string GetCurrentHostName();

        /// <summary>
        /// 获取网页url
        /// </summary>
        /// <returns></returns>
        string GetThisPageUrl();

        /// <summary>
        /// Gets a value indicating whether current connection is secured
        /// </summary>
        /// <returns>true - secured, false - not secured</returns>
        bool IsCurrentConnectionSecured();

        /// <summary>
        /// Gets server variable by name
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Server variable</returns>
        string ServerVariables(string name);

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
        bool IsStaticResource(HttpRequest request);

        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        string MapPath(string path);

        /// <summary>
        /// Get a value indicating whether the request is made by search engine (web crawler)
        /// </summary>
        /// <param name="context">HTTP context</param>
        /// <returns>Result</returns>
        bool IsSearchEngine(HttpContextBase context);

        /// <summary>
        /// Gets a value that indicates whether the client is being redirected to a new location
        /// </summary>
        bool IsRequestBeingRedirected { get; }
    }
}
