using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public Button SaveButton;
    public Button LoadButton;

    void Awake()
    {
        SaveButton.onClick.AddListener(Util.Save);
        LoadButton.onClick.AddListener(Util.Load);
    }
}