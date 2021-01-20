# UNET LAN Client-Server Simple Abstraction

## Pros

### Rapid Development and light-weight
Using [NetworkClient](Assets/NetworkLANClient.cs) and [NetworkServer](Assets/NetworkServer.cs) to communicate.

### Ease-of-Use
If you want to send custom command, just override *OnClientCmdHandler* and *OnServerAckHandler*.

### Client auto-reconnection
If client doesn't connect to server, will try contiuously.

## Cons

### Default max connections
The default max connections is 100 now.

## Quick start
1. Implement your custom client and server. [NetworkClient](Assets/NetworkLANClient.cs) and [NetworkServer](Assets/NetworkServer.cs) both are abstract class.
2. Implement abstract method *OnClientCmdHandler* and *OnServerAckHandler*.
3. Initialize server and add event callback
```cs
ConnectionConfig config = NetworkRPCHelper.GetConnectionConfig();
server = new CustomServer();
server.SetNetworkSetting(ip, port);
server.Initialize(this, config);
```

4. Initialize client and add event callback
```cs
ConnectionConfig config = NetworkRPCHelper.GetConnectionConfig();
client = new CustomClient();
client.AddEventCallback(MsgType.Connect, OnClientConnected);
client.AddEventCallback(MsgType.Disconnect, OnClientDisconnected);
client.SetNetworkSetting(ip, port);
client.Initialize(this, config);
```

5. Using SendRPC method to start communication
