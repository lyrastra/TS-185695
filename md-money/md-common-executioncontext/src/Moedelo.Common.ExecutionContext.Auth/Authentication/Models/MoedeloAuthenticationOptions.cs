namespace Moedelo.Common.ExecutionContext.Auth.Authentication.Models
{
    public class MoedeloAuthenticationOptions
    {
        /// <summary>
        /// default value is true
        /// </summary>
        public bool UseOAuth { get; set; } = true;

        /// <summary>
        /// default value is true
        /// </summary>
        public bool UseApiKey { get; set; } = true;

        /// <summary>
        /// default value is true
        /// </summary>
        public bool UseCookie { get; set; } = true;
    }
}
