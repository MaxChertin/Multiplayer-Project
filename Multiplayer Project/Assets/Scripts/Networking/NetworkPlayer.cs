using System;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject localPlayerPrefab;
    [SerializeField] private SkinnedMeshRenderer[] playerSkinMesh;
    
    private void Start()
    {
        if (isLocalPlayer)
        {
            foreach (var skin in playerSkinMesh)
            {
                skin.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            }
        }
        else 
            localPlayerPrefab.SetActive(false);

        Destroy(this);
    }
}
