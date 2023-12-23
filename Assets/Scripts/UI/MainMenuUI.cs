using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // network manager
    [SerializeField]
    private UnityTransport unityTransport;
    [SerializeField]
    private Button connectButton;
    // alert dialog
    [SerializeField]
    private AlertDialog alertDialogPrefab;
    // input dialog
    [SerializeField]
    private InputDialog inputDialogPrefab;

    private void Awake()
    {
        // button onClick events
        connectButton.onClick.AddListener(() =>
        {
            InputDialog inputDialog = Instantiate(inputDialogPrefab);
            inputDialog.SetTitle("Server address");
            inputDialog.SetText("127.0.0.1");
            inputDialog.OkPressed += new OkEvent(ConnectClient);
        });
    }

    // connect to server
    private void ConnectClient(string address)
    {
        // check server address
        if (IsValidHostOrAddress(address))
        {
            // connect
            unityTransport.ConnectionData.Address = address;
            NetworkManager.Singleton.StartClient();
            // SceneManager.LoadScene("Map");
        }
        else
        {
            // alert
            AlertDialog alertDialog = Instantiate(alertDialogPrefab);
            alertDialog.SetText("Server address is not valid");
        }
    }

    // validate server address
    bool IsValidHostOrAddress(string input)
    {
        if (IPAddress.TryParse(input, out IPAddress address))
        {
            // valid IP
            return true;
        }
        else
        {
            try
            {
                Dns.GetHostAddresses(input);
                // valid hostname
                return true;
            }
            catch
            {
                // invalid hostname
                return false;
            }
        }
    }
}
