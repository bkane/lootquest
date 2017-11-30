using Assets.Scripts;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public Button OpenOptionsPanel;
    public Button MuteButton;
    public Button NextTrackButton;

    public Sprite MutedIcon;
    public Sprite NotMutedIcon;

    protected bool isMuted;

    void Awake()
    {
        OpenOptionsPanel.onClick.AddListener(Game.Instance.OpenOptionsPanel);
        MuteButton.onClick.AddListener(ToggleAudio);
        NextTrackButton.onClick.AddListener(NextTrack);
    }

    public void SetMute(bool isMuted)
    {
        this.isMuted = isMuted;

        if (isMuted)
        {
            MuteButton.GetComponent<Image>().sprite = MutedIcon;
            MasterAudio.OnlyPlaylistController.PausePlaylist();

            if (Game.Instance.Settings.UseSteamMusic)
            {
                if (SteamManager.Client.IsValid)
                {
                    SteamManager.Client.Music.Pause();
                }
            }

            MasterAudio.MixerMuted = true;
            MasterAudio.PlaylistsMuted = true;
        }
        else
        {
            MasterAudio.MixerMuted = false;
            MuteButton.GetComponent<Image>().sprite = NotMutedIcon;

            if (Game.Instance.Settings.UseSteamMusic && SteamManager.Client.IsValid)
            {
                SteamManager.Client.Music.Play();
            }
            else
            {    
                MasterAudio.PlaylistsMuted = false;
                MasterAudio.OnlyPlaylistController.UnpausePlaylist();   
            }
        }
    }

    protected void ToggleAudio()
    {
        SetMute(!isMuted);
    }

    protected void NextTrack()
    {
        if (isMuted) { return; }

        if (Game.Instance.Settings.UseSteamMusic)
        {
            if (SteamManager.Client.IsValid)
            {
                SteamManager.Client.Music.Next();
            }
        }
        else
        {
            MasterAudio.OnlyPlaylistController.PlayNextSong();
        }
    }


    private void OnDestroy()
    {
        if (SteamManager.Client.IsValid)
        {
            SteamManager.Client.Music.Pause();
        }
    }
}