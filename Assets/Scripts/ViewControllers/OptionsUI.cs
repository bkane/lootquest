using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public Button SaveButton;
    public Button LoadButton;

    public Button Music_PlayButton;
    public Button Music_NextButton;

    void Awake()
    {
        SaveButton.onClick.AddListener(Util.Save);
        LoadButton.onClick.AddListener(Util.Load);

        Music_PlayButton.onClick.AddListener(PlayMusic);
        Music_NextButton.onClick.AddListener(Next);
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