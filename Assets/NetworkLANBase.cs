using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum NetworkType
{
    OnRPC = 1024,
}

public abstract class NetworkLANBase : IDisposable
{
    #region Property
    public NetworkPeerType PeerType
    {
        get
        {
            return mPeerType;
        }
    }
    #endregion

    #region Protected-Field
    protected NetworkPeerType mPeerType;
    protected string mServerIP;
    protected int mServerPort;
    #endregion

    #region Private-Field
    private bool mDisposedValue;
    private Dictionary<short, Action> mCallbacks;
    #endregion

    public abstract void Initialize(MonoBehaviour mono, ConnectionConfig config);
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void SetNetworkSetting(string ip, int port)
    {
        mServerIP = ip;
        mServerPort = port;
    }

    public void AddEventCallback(short type, Action handler)
    {
        mCallbacks = ((mCallbacks == null) ? new Dictionary<short, Action>() : mCallbacks);
        Dictionary<short, Action> map = mCallbacks;
        if (map.ContainsKey(type))
        {
            map[type] = handler;
            return;
        }
        map.Add(type, handler);
    }

    public void RemoveEventCallback(short type)
    {
        mCallbacks = ((mCallbacks == null) ? new Dictionary<short, Action>() : mCallbacks);
        Dictionary<short, Action> map = mCallbacks;
        if (map.ContainsKey(type))
        {
            map.Remove(type);
        }
    }

    protected virtual void OnPeerConnected(NetworkMessage message)
    {
    }

    protected virtual void OnPeerDisconnected(NetworkMessage message)
    {
    }

    protected virtual void AddHandler()
    {
    }

    protected virtual void RemoveHandler()
    {
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!mDisposedValue)
        {
            if (disposing)
            {
                /// TODO: dispose managed state (managed objects).
                if (mCallbacks != null)
                {
                    mCallbacks.Clear();
                    mCallbacks = null;
                }
            }

            /// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            /// TODO: set large fields to null.
            mDisposedValue = true;
        }
    }

    protected void ExecuteHandler(short type)
    {
        Dictionary<short, Action> map = mCallbacks;
        if (map.ContainsKey(type))
        {
            map[type]();
            return;
        }
        Debug.LogError("[NetworkLANBase.ExecuteHandler] Not register [" + MsgType.MsgTypeToString(type) + "] callback");
    }
}