using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlertDialog : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    [SerializeField]
    private Button okButton;

    private void Awake()
    {
        okButton.onClick.AddListener(() =>
        {
            Destroy(gameObject);
        });
    }

    public void SetText(string value)
    {
        text.text = value;
    }
}
