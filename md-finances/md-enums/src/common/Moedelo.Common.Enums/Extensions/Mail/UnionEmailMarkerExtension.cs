using Moedelo.Common.Enums.Enums.Email;

namespace Moedelo.Common.Enums.Extensions.Mail
{
    public static class UnionEmailMarkerExtension
    {
        public static string GetSubject(this UnionEmailMarker marker)
        {
            return marker.GetDescription();
        }

        public static string GetSubject(this UnionEmailMarker marker, params object[] args)
        {
            return string.Format(marker.GetDescription(), args);
        }
    }
}