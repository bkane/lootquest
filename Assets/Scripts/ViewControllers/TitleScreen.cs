using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Button StartGame;
    public Button Quit;
    public TextMeshProUGUI VersionText;

    void Awake()
    {
        StartGame.onClick.AddListener(Game.Instance.StartGameClicked);
        Quit.onClick.AddListener(Game.Instance.QuitGameClicked);

        VersionText.text = string.Format("v{0}", Application.version);
    }
}