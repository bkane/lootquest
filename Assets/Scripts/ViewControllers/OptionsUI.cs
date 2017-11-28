using Assets.Scripts;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public Button OpenOptionsPanel;
    public Button MuteButton;

    public Sprite MutedIcon;
    public Sprite NotMutedIcon;

    void Awake()
    {
        OpenOptionsPanel.onClick.AddListener(Game.Instance.OpenOptionsPanel);
        MuteButton.onClick.AddListener(ToggleAudio);
    }

    protected void ToggleAudio()
    {
        if (MasterAudio.MixerMuted)
        {
            MasterAudio.MixerMuted = false;
            MasterAudio.PlaylistsMuted = false;
            MasterAudio.OnlyPlaylistController.UnpausePlaylist();
            MuteButton.GetComponent<Image>().sprite = NotMutedIcon;
        }
        else
        {
            MasterAudio.MixerMuted = true;
            MasterAudio.PlaylistsMuted = true;
            MasterAudio.OnlyPlaylistController.PausePlaylist();
            MuteButton.GetComponent<Image>().sprite = MutedIcon;
        }
    }

    protected void PlayMusic()
    {
        if (SteamManager.Client.Music.GetPlayback() == Facepunch.Steamworks.Music.AudioPlaybackStatus.Playing)
        {
            SteamManager.Client.Music.Pause();
        }
        else
        {
            SteamManager.Client.Music.Play();
        }
    }

    protected void Next()
    {
        SteamManager.Client.Music.Next();
    }

    private void OnDestroy()
    {
        SteamManager.Client.Music.Pause();
    }
}