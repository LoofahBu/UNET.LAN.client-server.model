# Unity UNET Client-Server Simple Abstraction For LAN

## Introduction

This project is a simple abstraction wrapper for Unity UNET to help developers can rapid develop an application for local network. Just override *OnClientCmdHandler* and *OnServerAckHandler* which on [NetworkClient](Assets/NetworkLANClient.cs) and [NetworkServer](Assets/NetworkServer.cs). It also provides the auto-reconnection mechanism when client can't connect to server.

## Quick Start
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
