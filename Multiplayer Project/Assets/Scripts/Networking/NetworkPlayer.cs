using System;
using Mirror;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject localPlayerPrefab;
    [SerializeField] private GameObject playerPrefab;
    
    private void Start()
    {
        if (isLocalPlayer)
            playerPrefab.SetActive(false);
        else 
            localPlayerPrefab.SetActive(false);

        Destroy(this);
    }
}
