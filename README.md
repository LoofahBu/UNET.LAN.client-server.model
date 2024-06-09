# Unity UNET Client-Server Wrapper For LAN

## Introduction
This project is an abstraction wrapper for Unity UNET that helps developers develop applications for the local network. You can override `OnClientCmdHandler` and `OnServerAckHandler` on [NetworkClient](Assets/NetworkLANClient.cs) and [NetworkServer](Assets/NetworkLANServer.cs) to customize their behavior.

It also provides an auto-reconnection mechanism when a client can't connect to a server.

## Quick Start
1. Create your client class that inherits from [NetworkClient](Assets/NetworkLANClient.cs)
2. Create your server class that inherits from [NetworkServer](Assets/NetworkLANServer.cs)
3. Implement the abstract methods `OnClientCmdHandler` and `OnServerAckHandler`
4. Initialize the server and add an event callback:
```cs
ConnectionConfig config = NetworkRPCHelper.GetConnectionConfig();
server = new CustomServer();
server.SetNetworkSetting(ip, port);
server.Initialize(this, config);
```

5. Initialize the client and add event callbacks:
```cs
ConnectionConfig config = NetworkRPCHelper.GetConnectionConfig();
client = new CustomClient();
client.AddEventCallback(MsgType.Connect, OnClientConnected);
client.AddEventCallback(MsgType.Disconnect, OnClientDisconnected);
client.SetNetworkSetting(ip, port);
client.Initialize(this, config);
```

6. Use the SendRPC method to start communication
