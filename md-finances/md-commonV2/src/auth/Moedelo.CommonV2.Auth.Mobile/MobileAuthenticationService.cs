using System;
using System.Web;
using Moedelo.CommonV2.Auth.Domain;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.Auth.Mobile
{
    [Inject(InjectionType.Singleton)]
    public class MobileAuthenticationService : IAuthenticationService, IDisposable
    {
        private bool disposed;
        private IdentityServerClient identityServerClient;

        public MobileAuthenticationService(IIdentityServerClient identityServerClient)
        {
            this.identityServerClient = (IdentityServerClient)identityServerClient;
        }

        private static string AccessToken => HttpContext.Current.Request.Headers.Get("MDAccessToken");
        private static string CompanyId => HttpContext.Current.Request.Headers.Get("MDFirmID");

        public AuthenticationInfo Authenticate()
        {
            CheckDisposed();

            try
            {
                if (AccessToken == null)
                {
                    return null;
                }

                var tiket = identityServerClient.SendAuthenticationRequest(AccessToken, CompanyId, true);
                if (tiket == null || tiket.IsNotAuthorized)
                {
                    return null;
                }

                return new AuthenticationInfo { UserId = tiket.UserId, FirmId = tiket.FirmId };
            }
            catch
            {
                return null;
            }
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
                if (identityServerClient != null)
                {
                    identityServerClient.Dispose();
                    identityServerClient = null;
                }
            }

            disposed = true;
        }

        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("MobileAuthenticationService");
            }
        }
    }
}