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
            //Destroy(playerPrefab);
            playerPrefab.SetActive(false);
        else
        {
            //Destroy(localCamera);
            //Destroy(localPlayerPrefab);
            localPlayerPrefab.SetActive(false);
        }

        Destroy(this);
    }
}
