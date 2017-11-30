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
        AutoSaveToggle.isOn = Game.Instance.Settings.AutoSave;
        SteamMusicToggle.isOn = Game.Instance.Settings.UseSteamMusic;

        CloseOptionsPanel.onClick.AddListener(Game.Instance.CloseOptionsPanel);

        SaveAndQuitButton.onClick.AddListener(Game.Instance.SaveAndQuit);
        ResetProgressButton.onClick.AddListener(Game.Instance.OpenRestartPopup);

        AutoSaveToggle.onValueChanged.AddListener(UpdateAutoSave);
        SteamMusicToggle.onValueChanged.AddListener(UpdateUseSteamMusic);

        SteamMusicToggle.gameObject.SetActive(SteamManager.Client.IsValid);
    }

    protected void UpdateAutoSave(bool val)
    {
        Game.Instance.Settings.AutoSave = val;
    }

    protected void UpdateUseSteamMusic(bool val)
    {
        Game.Instance.Settings.UseSteamMusic = val;
        Game.Instance.UpdateMusicSettings();
    }
}