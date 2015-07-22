using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading;

namespace VirtoCommerce.Webshop.Client.Security
{
    public class HmacMessageProcessingHandler : MessageProcessingHandler
    {
        private readonly string _appId;

        private readonly string _secretKey;

        public HmacMessageProcessingHandler(string appId, string secretKey)
            : base(new WebRequestHandler())
        {
            if (string.IsNullOrWhiteSpace(appId))
            {
                throw new ArgumentException("appId must not be empty.");
            }
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new ArgumentException("secretKey must not be empty.");
            }

            _appId = appId;
            _secretKey = secretKey;
        }

        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var signature = new ApiRequestSignature { AppId = _appId };

            var parameters = new[]
            {
                new NameValuePair(null, _appId),
                new NameValuePair(null, signature.TimestampString)
            };

            signature.Hash = HmacUtility.GetHashString(key => new HMACSHA256(key), _secretKey, parameters);

            request.Headers.Authorization = new AuthenticationHeaderValue("HMACSHA256", signature.ToString());
            return request;
        }

        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            return response;
        }
    }
}