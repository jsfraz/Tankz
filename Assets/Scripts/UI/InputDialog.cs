using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void OkEvent(string text); 

public class InputDialog : MonoBehaviour
{
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private TMP_InputField input;
    [SerializeField]
    private Button okButton;
    public event OkEvent OkPressed;

    private void Awake()
    {
        okButton.onClick.AddListener(() =>
        {
            OkPressed(input.text);
            Destroy(gameObject);
        });
        input.Select();
    }

    public void SetTitle(string value)
    {
        title.text = value;
    }

    public void SetText(string value)
    {
        input.text = value;
    }
}
