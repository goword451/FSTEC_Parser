using System.Net;
using System.Net.NetworkInformation;
using FSTEC.Properties;

namespace FSTEC
{
    class Network
    {
        internal static bool CheckInternetConnection()
        {
            try { return new Ping().Send("google.com").Status == IPStatus.Success ? true : false; }
            catch { return false; }
        }
        internal static void DownloadFile()
        {
            var webClient = new WebClient();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            webClient.DownloadFile(Settings.Default.WebFilePath, Settings.Default.LocalFilePath);
        }
    }
}
