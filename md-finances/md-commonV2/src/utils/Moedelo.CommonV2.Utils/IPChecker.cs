using System.Web;

namespace Moedelo.CommonV2.Utils
{
    public static class IPChecker
    {
        /// <summary> Получение IP-адреса из HttpRequest'а. Работает с учетом проксей </summary>
        /// <returns> IP в строковом представлении </returns>
        public static string GetIPAddress(HttpRequest request)
        {
            // Правильнее проверить так: сначала X_FORWARDED_FOR, потом, если его нет,
            // уже проверять REMOTE_ADDR. Это верно для проксевых соединений
            string ipAddr = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            // В случае нескольких проксей X_FORWARDED_FOR будет содержать 
            // несколько IP через запятую, нам нужен первый (client, proxy1, proxy2)
            // В случае прямого коннекта - ничего
            ipAddr = string.IsNullOrEmpty(ipAddr) ? request.ServerVariables["REMOTE_ADDR"] : ipAddr.Split(',')[0];

            return ipAddr;
        }
    }
}