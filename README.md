# UNET.LAN.client-server.model
## Benefits
* **Rapid Development and light-weight** - Using 'NetworkClient' and 'NetworkServer' to communicate.
* **Ease-of-Use** - If you want to send custom command, just override 'OnClientCmdHandler' and 'OnServerAckHandler'.

## Caveats
* **Client auto-reconnection** - If client doesn't connect to server, will try contiuously.
* **Default max connections** - The default max connections is 100 now.


## Quick start
* **Implement your custom client and server** - NetworkLANClient and NetworkLANServer both are abstract class.
* **Implement abstract method 'OnClientCmdHandler' and 'OnServerAckHandler'**
* **Initalize server and add event callback**
```cs
ConnectionConfig config = NetworkRPCHelper.GetConnectionConfig();
server = new CustomServer();
server.SetNetworkSetting(ip, port);
server.Initialize(this, config);
```

* **Initalize client and add event callback**
```cs
ConnectionConfig config = NetworkRPCHelper.GetConnectionConfig();
client = new CustomClient();
client.AddEventCallback(MsgType.Connect, OnClientConnected);
client.AddEventCallback(MsgType.Disconnect, OnClientDisconnected);
client.SetNetworkSetting(ip, port);
client.Initialize(this, config);
```
* **Using SendRPC function to start communication**
