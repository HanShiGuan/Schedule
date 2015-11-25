using Rongzi.Infrastructure.Http;
using Rongzi.Infrastructure.Util;

namespace Rongzi.Mvc.Infrastructure
{
    /// <summary>
    /// Api host used by mvc
    /// </summary>
    public class MvcApiHost : ApiHost
    {
        public MvcApiHost(string apiUrl)
        {
            ApiName = apiUrl;
            Domain = Helper.WebAPIHost;
            DataMimeType = MimeTypeConstant.Application_Json;
            InputQueryString = string.Empty;
        }
    }
}
