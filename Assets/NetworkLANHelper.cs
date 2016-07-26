using UnityEngine.Networking;

public static class NetworkLANHelper
{
    /// <summary>
    /// It's better to use the xml to record your setting
    /// </summary>
    /// <returns></returns>
    public static ConnectionConfig GetConnectionConfig()
    {
        ConnectionConfig config = new ConnectionConfig();
        config.ConnectTimeout = 10000;
        config.AckDelay = 100;
        config.AddChannel(QosType.ReliableFragmented);
        return config;
    }
}