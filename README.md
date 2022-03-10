# Unity UNET Client-Server Wrapper For LAN

## Introduction

This project is an abstraction wrapper for Unity UNET to help developers develop an application for the local network. Just override **OnClientCmdHandler** and **OnServerAckHandler** which on [NetworkClient](Assets/NetworkLANClient.cs) and [NetworkServer](Assets/NetworkServer.cs).

It also provides the auto-reconnection mechanism when a client can't connect to a server.

## Quick Start
1. Create your client class that inherits [NetworkClient](Assets/NetworkLANClient.cs)
2. Create your server class that inherits [NetworkServer](Assets/NetworkServer.cs).
3. Implement abstract method **OnClientCmdHandler** and **OnServerAckHandler**.
4. Initialize server and add event callback
```cs
ConnectionConfig config = NetworkRPCHelper.GetConnectionConfig();
server = new CustomServer();
server.SetNetworkSetting(ip, port);
server.Initialize(this, config);
```

5. Initialize client and add event callback
```cs
ConnectionConfig config = NetworkRPCHelper.GetConnectionConfig();
client = new CustomClient();
client.AddEventCallback(MsgType.Connect, OnClientConnected);
client.AddEventCallback(MsgType.Disconnect, OnClientDisconnected);
client.SetNetworkSetting(ip, port);
client.Initialize(this, config);
```

6. Using SendRPC method to start communication
