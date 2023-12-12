using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    // ui buttons
    [SerializeField]
    private Button serverButton;
    [SerializeField]
    private Button hostButton;
    [SerializeField]
    private Button clientButton;
    
    private void Awake()
    {
        // button onClick events
        // server
        serverButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });
        // host
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
        // client
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }
}
