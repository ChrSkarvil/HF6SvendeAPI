using Azure;
using HF6Svende.Application.Service_Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF6Svende.Application.Services
{
    public class TokenService : ITokenService
    {
        //private readonly string _clientId;
        //private readonly string _clientSecret;
        //private readonly string _tenantId;

        //public TokenService(string clientId, string clientSecret, string tenantId)
        //{
        //    _clientId = clientId;
        //    _clientSecret = clientSecret;
        //    _tenantId = tenantId;
        //}

        //public async Task<string> GetAccessTokenAsync()
        //{
        //    var tokenEndpoint = $"https://login.microsoftonline.com/{_tenantId}/oauth2/v2.0/token";
        //    using var client = new HttpClient();

        //    var requestBody = new Dictionary<string, string>
        //{
        //        { "grant_type", "client_credentials" },
        //        { "client_id", _clientId },
        //        { "client_secret", _clientSecret },
        //        { "scope", $"api://{_clientId}/.default" }
        //    };

        //    var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(requestBody));
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        var errorResponse = await response.Content.ReadAsStringAsync();
        //        throw new HttpRequestException($"Failed to obtain token: {response.StatusCode} - {errorResponse}");
        //    }

        //    var responseContent = await response.Content.ReadAsStringAsync();
        //    var json = JObject.Parse(responseContent);
        //    var accessToken = json.Value<string>("access_token");

        //    return accessToken;
        //}
    }
}
