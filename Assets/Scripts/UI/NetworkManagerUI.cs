using System;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    // network manager
    [SerializeField]
    private UnityTransport unityTransport;
    // ui buttons
    [SerializeField]
    private Button serverButton;
    [SerializeField]
    private Button hostButton;
    [SerializeField]
    private Button clientButton;
    // alert dialog
    [SerializeField]
    private AlertDialog alertDialogPrefab;
    // input dialog
    [SerializeField]
    private InputDialog inputDialogPrefab;

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
            InputDialog inputDialog = Instantiate(inputDialogPrefab);
            inputDialog.SetTitle("Server address");
            inputDialog.OkPressed += new OkEvent(ConnectClient);
        });
    }

    private void ConnectClient(string address)
    {
        // check server address
        if (IsValidHostOrAddress(address))
        {
            // connect
            unityTransport.ConnectionData.Address = address;
            NetworkManager.Singleton.StartClient();
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
