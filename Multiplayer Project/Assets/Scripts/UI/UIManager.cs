using System;
using TMPro;
using UnityEngine;
using Mirror;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ping;

    private void Update()
    {
        ping.text = "ping: " + NetworkTime.rtt.ToString("0") + "ms" + "\n" + "fps: " + (1f / Time.unscaledDeltaTime).ToString("0");
    }
}
