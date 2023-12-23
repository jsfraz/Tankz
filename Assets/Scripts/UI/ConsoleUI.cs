using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TMPro;
using UnityEngine;

public class ConsoleUI : MonoBehaviour
{
    // canvas
    private Canvas canvas;
    // ASCII art
    [SerializeField]
    private TextAsset asciiArt;
    // scroll rect
    [SerializeField]
    private ScrollRectNoDrag scrollRect;
    // console input
    [SerializeField]
    private TMP_InputField inputField;

    private void Start()
    {
        //canvas
        canvas = gameObject.GetComponent<Canvas>();
        // splash
        Splash();
        // logger
        Application.logMessageReceived += HandleLog;
        // input
        inputField.enabled = false;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // formatted time
        string formattedTime = "[" + DateTime.Now.ToString("HH:mm:ss") + "]";
        // add the log to the text field with the appropriate color
        switch (type)
        {
            // display warnings in yellow color
            case LogType.Warning:
                scrollRect.content.GetComponent<TextMeshProUGUI>().text += $"<color=yellow>{formattedTime} {logString}</color>\n";
                break;
            // display errors and exceptions in red color
            case LogType.Error:
            case LogType.Exception:
                scrollRect.content.GetComponent<TextMeshProUGUI>().text += $"<color=red>{formattedTime} {logString}\nStackTrace: {stackTrace}</color>";
                break;
            // display other logs with their default color
            default:
                scrollRect.content.GetComponent<TextMeshProUGUI>().text += $"{formattedTime} {logString}\n";
                break;
        }

        // scroll to bottom (https://stackoverflow.com/a/47613689/19371130)
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }

    // return commands
    private Dictionary<string, Action> GetCommands()
    {
        Dictionary<string, Action> commands = new Dictionary<string, Action>();

        var methods = GetType().GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

        foreach (var method in methods)
        {
            var commandAttribute = (CommandAttribute)Attribute.GetCustomAttribute(method, typeof(CommandAttribute));

            if (commandAttribute != null)
            {
                commands.Add(commandAttribute.Name, (Action)Delegate.CreateDelegate(typeof(Action), this, method));
            }
        }

        return commands.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
    }

    // execute command
    public void ExecuteCommand()
    {
        if (inputField.text != "")
        {
            Dictionary<string, Action> commands = GetCommands();
            // check if command exists and execute
            if (commands.ContainsKey(inputField.text))
                commands[inputField.text].Invoke();
            else
                Debug.LogWarning("Command '" + inputField.text + "' does not exist!");

            inputField.text = "";
        }

        // scroll to bottom and focus input
        inputField.ActivateInputField();
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }

    // help command
    [Command("help")]
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private void Help()
    {
        string text = "You can use the following commands:";
        // get command and add name to string
        foreach (string command in GetCommands().Keys)
            text += "\n" + command;
        // log
        Debug.Log(text);
    }

    // splash command
    [Command("splash")]
    private void Splash()
    {
        scrollRect.content.GetComponent<TextMeshProUGUI>().text += asciiArt;
        scrollRect.content.GetComponent<TextMeshProUGUI>().text += "\n" + Application.productName + " v" + Application.version + " (c) " + Application.companyName + "\n\n";
    }

    // clear command
    [Command("clear")]
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private void Clear()
    {
        scrollRect.content.GetComponent<TextMeshProUGUI>().text = "";
    }

    // exit command
    [Command("exit")]
    [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members")]
    private void Exit()
    {
        Application.Quit();
    }

    // shows/hides canvas
    public void ShowHide()
    {
        // enable/disable
        canvas.enabled = !canvas.enabled;
        // log
        if (canvas.enabled)
        {
            Debug.Log("Opened console.");
            inputField.enabled = true;
            inputField.ActivateInputField();
        }
        else
        {
            Debug.Log("Closed console.");
            inputField.enabled = false;
            inputField.text = "";
        }
    }
}
