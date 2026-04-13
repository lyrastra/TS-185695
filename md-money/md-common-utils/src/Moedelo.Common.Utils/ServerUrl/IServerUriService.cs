using System;

namespace Moedelo.Common.Utils.ServerUrl
{
    public interface IServerUriService
    {
        string GetBaseUrl(Uri currentUrl);

        string GetSsoUrl(Uri currentUrl);

        string GetAuthServerUrl(Uri uri);
    }
}
