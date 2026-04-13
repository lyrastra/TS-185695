using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.RuCaptchaClient
{
    public class RuCaptchaResolverClient : ICaptchaResolverClient, IDisposable
    {
        private bool disposed = false;

        private HttpClient httpClient;

        private const string okResultPrefix = "OK|";

        private const string notReady = "CAPCHA_NOT_READY";

        private const string noSlotAvailable = "ERROR_NO_SLOT_AVAILABLE";

        private readonly SettingValue accountKey;

        private readonly SettingValue serviceUri;

        public RuCaptchaResolverClient(ISettingRepository settingRepository)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            accountKey = settingRepository.Get("RuCaptchaAccountKey");
            serviceUri = settingRepository.Get("RuCaptchaUrl");
        }

        public async Task<string> DecryptCaptchaAsync(string base64File)
        {
            CheckDisposed();

            try
            {
                var captchaId = await UploadCaptcha(base64File).ConfigureAwait(false);

                if (captchaId == null)
                {
                    return null;
                }

                var captchaResult = await GetCaptchaResult(captchaId).ConfigureAwait(false);

                return captchaResult;
            }
            catch
            {
                return null;
            }
        }

        private async Task<string> UploadCaptcha(string base64File)
        {
            const int tryCount = 10;
            var tryDelay = new TimeSpan(0, 0, 0, 2);
            var url = $"{serviceUri.Value}/in.php?";
            var parameters = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("key", accountKey.Value),
                new KeyValuePair<string, string>("method", "base64"),
                new KeyValuePair<string, string>("body", base64File)
            };
            string captchaId = null;

            for (var i = 0; i < tryCount && captchaId == null; i++)
            {
                using (
                    var response =
                        await httpClient.PostAsync(url, new FormUrlEncodedContent(parameters)).ConfigureAwait(false))
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (responseContent.StartsWith(okResultPrefix))
                    {
                        captchaId = responseContent.Substring(okResultPrefix.Length);
                    }
                    else if (responseContent == noSlotAvailable)
                    {
                        await Task.Delay(tryDelay).ConfigureAwait(false);
                    }
                    else
                    {
                        break;
                    }
                }
            }


            return captchaId;
        }

        private async Task<string> GetCaptchaResult(string captchaId)
        {
            string captcha = null;
            var url = $"{serviceUri}/res.php?key={accountKey}&action=get&id={captchaId}";
            const int tryCount = 5;
            var tryDelay = new TimeSpan(0, 0, 0, 3);


            for (var i = 0; i < tryCount && captcha == null; i++)
            {
                using (var response = await httpClient.GetAsync(url).ConfigureAwait(false))
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (responseContent.StartsWith(okResultPrefix))
                    {
                        captcha = responseContent.Substring(okResultPrefix.Length);
                    }
                    else if (responseContent == notReady)
                    {
                        await Task.Delay(tryDelay).ConfigureAwait(false);
                    }
                    else
                    {
                        break;
                    }
                }
            }


            return captcha;
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
                throw new ObjectDisposedException("RuCaptchaResolverClient");
            }
        }
    }
}