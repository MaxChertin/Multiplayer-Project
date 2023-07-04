using System;
using Steamworks;
using UnityEngine;

public class SteamManager : MonoBehaviour
{
    // Steam Manger is down for now as it is not necessary to keep initializing steam every time.
    
    private void Awake()
    {
        try
        {
            SteamClient.Init(480);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Couldn't initialize Steam client. exception: {ex}");
        }
        
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDisable()
    {
        SteamClient.Shutdown();
    }

    private void Update()
    {
        SteamClient.RunCallbacks();
    }
}
