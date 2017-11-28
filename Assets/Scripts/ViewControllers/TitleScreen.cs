using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Button StartGame;
    public Button Quit;

    void Awake()
    {
        StartGame.onClick.AddListener(Game.Instance.StartGameClicked);
        Quit.onClick.AddListener(Game.Instance.QuitGameClicked);
    }
}