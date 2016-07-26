using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class NetworkLANServer : NetworkLANBase
{
    #region Private-Field
    private bool mIsListening;
    #endregion

    #region Public-Method
    public override void Initialize(MonoBehaviour mono, ConnectionConfig config)
    {
        if (NetworkServer.active || mIsListening)
        {
            return;
        }
        mPeerType = NetworkPeerType.Server;
        AddHandler();
        NetworkServer.Configure(config, 100);
        mIsListening = NetworkServer.Listen(mServerPort);
    }

    public List<NetworkConnection> GetConnectedPeerList()
    {
        return new List<NetworkConnection>(NetworkServer.connections);
    }

    public void SendRPC(int connectedID, MessageBase message)
    {
        NetworkServer.SendToClient(connectedID, (short)NetworkType.OnRPC, message);
    }

    public void SendToAll(MessageBase message)
    {
        NetworkServer.SendToAll((short)NetworkType.OnRPC, message);
    }
    #endregion

    #region Protected-Method
    protected abstract void OnClientCmdHandler(NetworkMessage message);
    protected override void Dispose(bool disposing)
    {
        if (NetworkServer.active)
        {
            RemoveHandler();
            NetworkServer.DisconnectAll();
            NetworkServer.Shutdown();
            mIsListening = false;
        }
        base.Dispose(disposing);
    }

    protected override void AddHandler()
    {
        NetworkServer.RegisterHandler((short)NetworkType.OnRPC, OnClientCmdHandler);
    }

    protected override void RemoveHandler()
    {
        NetworkServer.UnregisterHandler((short)NetworkType.OnRPC);
    }
    #endregion
}