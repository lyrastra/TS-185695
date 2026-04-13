using System;
using System.ServiceModel;

namespace Moedelo.AstralV3.Client.Extensions
{
    public static class BasicHttpBindingEx
    {
        public static BasicHttpBinding SetSendTimeout(this BasicHttpBinding httpBinding, TimeSpan timeout)
        {
            httpBinding.SendTimeout = timeout;
            return httpBinding;
        }

    }
}
