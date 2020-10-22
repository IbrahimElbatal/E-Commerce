using PayPal.Api;
using System.Collections.Generic;

namespace UsingRepository.Configuration
{
    public static class PaypalConfiguration
    {
        public static readonly string ClientId;
        public static readonly string ClientSecret;

        static PaypalConfiguration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }

        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            var accessToken = new OAuthTokenCredential(ClientId, ClientSecret, GetConfig())
                .GetAccessToken();

            return accessToken;
        }

        public static APIContext GetApiContext()
        {
            var context = new APIContext(GetAccessToken());
            context.Config = GetConfig();
            return context;
        }
    }
}