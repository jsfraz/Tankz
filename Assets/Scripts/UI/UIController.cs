using UnityEngine;

public class UIController : MonoBehaviour
{
    // console
    [SerializeField]
    private ConsoleUI consolePrefab;
    [SerializeField]
    private KeyCode consoleKey;
    private ConsoleUI console;

    private void Start()
    {
        // instantiate UIs
        console = Instantiate(consolePrefab, transform);
    }

    private void Update()
    {
        // console
        if (Input.GetKeyDown(consoleKey) && Debug.isDebugBuild)
        {
            console.ShowHide();
        }
    }
}
