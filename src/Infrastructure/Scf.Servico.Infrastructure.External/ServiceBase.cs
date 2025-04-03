using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Scf.Servico.Infrastructure.External
{
    public abstract class ServiceBase
    {
        internal readonly MicroServiceConfiguration configuration;

        protected ServiceBase(MicroServiceConfiguration configuration) =>
            this.configuration = configuration;

        public async Task<string> GetRequest(string url, IDictionary<string, string>? headers = null)
        {
            var token = await GetTokenBearer();

            if (!string.IsNullOrEmpty(token))
            {
                if (headers == null)
                    headers = new Dictionary<string, string>();

                headers.Add("Authorization", $"Bearer {token}");
            }

            return await RestGetRequest(url, headers);
        }

        public async Task<ResponseMessage> GetResponse(string url, IDictionary<string, string>? headers = null)
        {
            var token = await GetTokenBearer();

            if (!string.IsNullOrEmpty(token))
            {
                if (headers == null)
                    headers = new Dictionary<string, string>();

                headers.Add("Authorization", $"Bearer {token}");
            }

            return await RestGetResponse(url, headers);
        }

        public async Task<string> PostRequest(string url, string content, IDictionary<string, string>? headers = null)
        {
            var token = await GetTokenBearer();

            if (!string.IsNullOrEmpty(token))
            {
                if (headers == null)
                    headers = new Dictionary<string, string>();

                headers.Add("Authorization", $"Bearer {token}");
            }

            return await RestPostRequest(url, content, headers);
        }

        private string? GetTokenBasic()
        {
            if (configuration.ClientId == null ||
                configuration.ClientSecret == null)
                return null;

            var clientId = configuration.ClientId;
            var clientSecret = configuration.ClientSecret;
            var base64 = Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");

            return Convert.ToBase64String(base64);
        }

        private async Task<string?> GetTokenBearer()
        {
            if (configuration.TokenSSO == null)
                return null;

            string? token;

            if (TokenExpired(configuration.DataToken, configuration.TokenExpiresIn))
            {
                var urlTokenSSO = configuration.TokenSSO;
                var tokenBasic = GetTokenBasic();

                var headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/x-www-form-urlencoded" },
                    { "Authorization", $"Basic {tokenBasic}" }
                };

                var content = new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" }
                };

                var resposta = await RestPostRequest(urlTokenSSO, content, headers);

                if (string.IsNullOrEmpty(resposta))
                    return null;

                var tokenSSO = JsonConvert.DeserializeObject(resposta) as dynamic;

                if (tokenSSO == null)
                    return null;

                token = tokenSSO.access_token;

                configuration.DataToken = DateTime.Now;
                configuration.TokenExpiresIn = tokenSSO.expires_in;
                configuration.Token = tokenSSO.access_token;
            }
            else
                token = configuration.Token;

            return token;
        }

        private static async Task<ResponseMessage> RestGetResponse(string url, IDictionary<string, string>? headers = null)
        {
            using (var client = new HttpClient())
            {

                if (headers != null && headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        if (header.Key == "Content-Type")
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header.Value));
                        else
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                using (var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url)).Result)
                {
                    var responseMessage = new ResponseMessage()
                    {
                        IsSuccessStatusCode = response.IsSuccessStatusCode,
                        StatusCode = (int)response.StatusCode,
                        Content = await response.Content.ReadAsStringAsync()
                    };

                    return responseMessage;
                }
            }
        }

        private static Task<string> RestGetRequest(string url, IDictionary<string, string>? headers = null)
        {
            using (var client = new HttpClient())
            {
                if (headers != null && headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        if (header.Key == "Content-Type")
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header.Value));
                        else
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                using (var response = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url)).Result)
                {
                    using (var responseContent = response.Content)
                        return responseContent.ReadAsStringAsync();
                }
            }
        }

        private static Task<string> RestPostRequest(string url, string content, IDictionary<string, string>? headers = null)
        {
            using (var client = new HttpClient())
            {
                if (headers != null && headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        if (header.Key == "Content-Type")
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header.Value));
                        else
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                using (var response = client.SendAsync(content != null ?
                        new HttpRequestMessage(HttpMethod.Post, url) { Content = new StringContent(content, Encoding.UTF8, "application/json") } :
                        new HttpRequestMessage(HttpMethod.Post, url)).Result)
                {
                    using (var responseContent = response.Content)
                        return responseContent.ReadAsStringAsync();
                }
            }
        }

        private static Task<string> RestPostRequest(string url, IDictionary<string, string> content, IDictionary<string, string>? headers = null)
        {
            using (var client = new HttpClient())
            {
                if (headers != null && headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        if (header.Key == "Content-Type")
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header.Value));
                        else
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                using (var response = client.SendAsync(content != null ?
                        new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(content) } :
                        new HttpRequestMessage(HttpMethod.Post, url)).Result)
                {
                    using (var responseContent = response.Content)
                        return responseContent.ReadAsStringAsync();
                }
            }
        }

        private static bool TokenExpired(DateTime dataToken, int tokenExpiresIn)
        {
            double initial = new TimeSpan(dataToken.Ticks).TotalSeconds;
            double final = new TimeSpan(DateTime.Now.Ticks).TotalSeconds;
            double currentTokenSeconds = (final - initial);

            if (currentTokenSeconds < tokenExpiresIn)
                return false;

            return true;
        }
    }

    public class ResponseMessage 
    {
        public bool IsSuccessStatusCode { get; set; }
        public int StatusCode{ get; set; }
        public string? Content { get; set; }
    }
}
