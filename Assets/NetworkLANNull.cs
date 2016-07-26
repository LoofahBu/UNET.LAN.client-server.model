using System;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkLANNull : NetworkLANBase
{
    public override void Initialize(MonoBehaviour mono, ConnectionConfig config)
    {
        Debug.Log("[NetworkLANNull.Initialize]");
    }
}