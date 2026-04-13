using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.CommonV2.Auth.Mobile
{
    [Inject(InjectionType.Singleton)]
    public class IdentityServerClient : IIdentityServerClient
    {
        private bool disposed;

        private HttpClient httpClient;

        public IdentityServerClient(ISettingRepository settingRepository)
        {
            httpClient = new HttpClient { BaseAddress = new Uri(settingRepository.Get("IdentityServerUrl").Value) };
            httpClient.DefaultRequestHeaders.Authorization = GetBasicAuth("client", "secret");
        }

        public TicketResponse SendAuthenticationRequest(string authToken, string companyId, bool useDefaultFirm)
        {
            CheckDisposed();

            using (var request = CreateAuthenticationRequest(authToken, companyId, useDefaultFirm))
            {
                using (var result = httpClient.SendAsync(request).Result)
                {
                    var content = result.Content.ReadAsStringAsync().Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        throw new AuthenticationException("Authentication Exception: " + content, null);
                    }

                    return content.FromJsonString<TicketResponse>();
                }
            }
        }

        public OAuthTicket SendAuthenticationRequest(string login, string password)
        {
            CheckDisposed();

            using (var request = CreateAuthenticationRequest(login, password))
            {
                using (var result = httpClient.SendAsync(request).Result)
                {
                    var content = result.Content.ReadAsStringAsync().Result;

                    if (!result.IsSuccessStatusCode)
                    {
                        throw new AuthenticationException("Authentication Exception: " + content, null);
                    }

                    return content.FromJsonString<OAuthTicket>();
                }
            }
        }

        public OAuthTicket SendRefreshTokenRequest(string refreshToken)
        {
            using (var request = CreateRefreshTokenRequest(refreshToken))
            {
                using (var result = httpClient.SendAsync(request).Result)
                {
                    var content = result.Content.ReadAsStringAsync().Result;
                    var data = content.FromJsonString<Dictionary<string, string>>();
                    if (data.ContainsKey("error"))
                    {
                        return new OAuthTicket();
                    }

                    return new OAuthTicket
                    {
                        access_token = data["access_token"],
                        expires_in = long.Parse(data["expires_in"]),
                        refresh_token = data["refresh_token"]
                    };
                }
            }
        }

        private static HttpRequestMessage CreateAuthenticationRequest(string token, string companyId, bool useDefaultFirm)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/identityv2?companyId={companyId}&useDefaultFirm={useDefaultFirm}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return request;
        }

        private static HttpRequestMessage CreateAuthenticationRequest(string login, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/token/mobile")
            {
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", login),
                    new KeyValuePair<string, string>("password", password),
                    new KeyValuePair<string, string>("client_id", "client"),
                    new KeyValuePair<string, string>("client_secret", "secret")
                })
            };
            return request;
        }

        private static HttpRequestMessage CreateRefreshTokenRequest(string refreshToken)
        {
            return new HttpRequestMessage(HttpMethod.Post, "/token/mobile")
            {
                Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("refresh_token", refreshToken),
                    new KeyValuePair<string, string>("client_id", "client"),
                    new KeyValuePair<string, string>("client_secret", "secret")
                })
            };
        }

        private static AuthenticationHeaderValue GetBasicAuth(string userName, string password)
        {
            var basicAuth = Encoding.ASCII.GetBytes($"{userName}:{password}");
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(basicAuth));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                    httpClient = null;
                }
            }

            disposed = true;
        }

        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("IdentityServerClient");
            }
        }
    }
}