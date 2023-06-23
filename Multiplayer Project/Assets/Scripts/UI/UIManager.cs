using TMPro;
using UnityEngine;
using Mirror;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text ping;
    
    private void OnGUI()
    {
        ping.text = "ping: " + NetworkTime.rtt.ToString("N0") + "ms";
    }
}
