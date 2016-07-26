using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public abstract class NetworkLANClient : NetworkLANBase
{
    #region Private-Field
    private NetworkClient mPeer;
    #endregion

    #region Public-Method
    public override void Initialize(MonoBehaviour mono, ConnectionConfig config)
    {
        if (mPeer == null)
        {
            mPeer = new NetworkClient();
            mPeer.Configure(config, 100);
        }
        Debug.Log("[NetworkLANClient.Initialize]");
        AddHandler();
        mPeerType = NetworkPeerType.Disconnected;
        mono.StartCoroutine(ConnectToServer());
    }

    public void SendRPC(MessageBase message)
    {
        if (mPeer == null)
        {
            Debug.Log("[NetworkLANClient.SendRPC] Peer is null");
            return;
        }
        mPeer.Send((short)NetworkType.OnRPC, message);
    }
    #endregion

    #region Protected-Method
    protected abstract void OnServerAckHandler(NetworkMessage message);
    protected override void Dispose(bool disposing)
    {
        Debug.Log("[NetworkLANClient.Dispose]");
        if (mPeer != null)
        {
            RemoveHandler();
            mPeer.Disconnect();
            mPeer = null;
            NetworkClient.ShutdownAll();
        }
        base.Dispose(disposing);
    }

    protected override void AddHandler()
    {
        mPeer.RegisterHandler((short)NetworkType.OnRPC, OnServerAckHandler);
        mPeer.RegisterHandler(MsgType.Connect, OnPeerConnected);
        mPeer.RegisterHandler(MsgType.Disconnect, OnPeerDisconnected);
    }

    protected override void RemoveHandler()
    {
        mPeer.UnregisterHandler((short)NetworkType.OnRPC);
        mPeer.UnregisterHandler(MsgType.Connect);
        mPeer.UnregisterHandler(MsgType.Disconnect);
    }

    protected override void OnPeerConnected(NetworkMessage message)
    {
        Debug.Log("[NetworkLANClient.OnPeerConnected] Connected to server");
        mPeerType = NetworkPeerType.Client;
        ExecuteHandler(MsgType.Connect);
        base.OnPeerConnected(message);
    }

    protected override void OnPeerDisconnected(NetworkMessage message)
    {
        if (mPeer != null)
        {
            mPeer.Disconnect();
            mPeer.Shutdown();
        }
        Debug.Log("[NetworkLANClient.OnPeerDisconnected] Disconnected from server");
        mPeerType = NetworkPeerType.Disconnected;
        ExecuteHandler(MsgType.Disconnect);
        base.OnPeerDisconnected(message);
    }
    #endregion

    #region Private-Method
    private IEnumerator ConnectToServer()
    {
        while ((mPeerType == NetworkPeerType.Connecting) || (mPeerType == NetworkPeerType.Disconnected))
        {
            if (mPeerType == NetworkPeerType.Connecting)
            {
                yield return null;
                continue;
            }

            Debug.Log("[NetworkLANClient.ConnectToServer] Connecting: " + mServerIP + ":" + mServerPort);
            mPeerType = NetworkPeerType.Connecting;
            mPeer.Connect(mServerIP, mServerPort);
            yield return new WaitForSeconds(1);
        }
    }
    #endregion
}