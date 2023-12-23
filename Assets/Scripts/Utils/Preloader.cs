using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    private void Start()
    {
        // starting as server
        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-server")
            {
                NetworkManager.Singleton.StartServer();
                SceneManager.LoadScene("Map");
            }
        }
    }
}