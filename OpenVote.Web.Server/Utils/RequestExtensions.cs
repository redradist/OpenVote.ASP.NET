using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace OpenVote.Web.Server.Utils
{
    public static class RequestExtensions
    {
        public static string GetIpAddress(this HttpRequest request)
        {
            var cfConnectionIpAddess = request.Headers["CF-CONNECTING-IP"];
            if (!StringValues.IsNullOrEmpty(cfConnectionIpAddess))
                return cfConnectionIpAddess;

            var httpXForwardedForIpAddress = request.Headers["HTTP_X_FORWARDED_FOR"];
            if (!StringValues.IsNullOrEmpty(httpXForwardedForIpAddress))
            {
                var addresses = httpXForwardedForIpAddress.ToArray();
                if (addresses.Length != 0)
                    return addresses[0];
            }

            return request.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
