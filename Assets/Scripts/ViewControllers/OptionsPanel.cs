using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    public Button CloseOptionsPanel;

    public Toggle AutoSaveToggle;
    public Toggle SteamMusicToggle;
    public Toggle MuteSFXToggle;
    public Toggle MuteMusicToggle;

    public Button SaveAndQuitButton;
    public Button ResetProgressButton;


    void Awake()
    {
        CloseOptionsPanel.onClick.AddListener(Game.Instance.CloseOptionsPanel);

        SaveAndQuitButton.onClick.AddListener(Game.Instance.SaveAndQuit);
        ResetProgressButton.onClick.AddListener(Game.Instance.ResetProgress);
    }
}