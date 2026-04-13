namespace Moedelo.CommonV2.Utils
{
    public static class CompanyIdHelper
    {
        public static string GetUrlWithCompanyId(string baseUrl, int firmId)
        {
            if (firmId == 0)
            {
                return baseUrl;
            }

            return AddQueryParam(baseUrl, "_companyId", firmId);
        }

        private static string AddQueryParam(string url, string name, int value)
        {
            if (string.IsNullOrEmpty(url))
            {
                return url;
            }

            if (url.Contains(name))
            {
                return url;
            }

            var ch = url.IndexOf('?') != -1 ? '&' : '?';
            var param = $"{ch}{name}={value}";
            var fragmentPos = url.IndexOf('#');
            if (fragmentPos == -1)
            {
                return url + param;
            }
            return url.Insert(fragmentPos, param);
        }
    }
}