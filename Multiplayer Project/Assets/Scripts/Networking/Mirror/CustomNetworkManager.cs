using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Steamworks;
using Mirror;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

public class CustomNetworkManager : NetworkManager
{
    // Overrides the base singleton so we don't
    // have to cast to this type everywhere.
    public static new CustomNetworkManager singleton { get; private set; }

    /// <summary>
    /// Runs on both Server and Client
    /// Networking is NOT initialized when this fires
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        singleton = this;
        
        // TODO get all spawnable prefabs and set them in the registered spawnable prefabs list
    }
    
    public override void Start()
    {
        base.Start();
        #if UNITY_SERVER
        Application.targetFrameRate = 64;
        QualitySettings.vSyncCount = 0;
        Debug.Log("")
        #endif
    }
}
