using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class OptionsPanel : MonoBehaviour
{
    public Button CloseOptionsPanel;

    public Toggle AutoSaveToggle;
    public Toggle SteamMusicToggle;
    public Toggle FullscreenToggle;
    public Dropdown ResolutionList;
    public Button ApplyGraphics;

    public Button SaveAndQuitButton;
    public Button ResetProgressButton;

    protected List<Resolution> resolutions;

    void Awake()
    {
        AutoSaveToggle.isOn = Game.Instance.Settings.AutoSave;
        SteamMusicToggle.isOn = Game.Instance.Settings.UseSteamMusic;
        FullscreenToggle.isOn = Screen.fullScreen;

        //Get resolutions
        resolutions = new List<Resolution>(Screen.resolutions).GroupBy(x => new { x.width, x.height }).Select(x => x.First()).ToList();
        ResolutionList.ClearOptions();
        ResolutionList.AddOptions(resolutions.Select(r => string.Format("{0}x{1}", r.width, r.height)).ToList());

        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.currentResolution.Equals(resolutions[i]))
            {
                ResolutionList.value = i;
                break;
            }
        }

        ApplyGraphics.onClick.AddListener(ApplyGraphicsChanges);

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

    protected void ApplyGraphicsChanges()
    {
        Resolution selectedResolution = resolutions[ResolutionList.value];
        Debug.LogFormat("Applying graphics change: {0}x{1} Fullscreen:{2}", selectedResolution.width, selectedResolution.height, FullscreenToggle.isOn);
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, FullscreenToggle.isOn);
    }
}