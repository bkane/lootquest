using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmRestartPopup : MonoBehaviour
{
    public Button Restart;
    public Button Cancel;

    void Awake()
    {
        Cancel.onClick.AddListener(Game.Instance.CloseRestartPopup);

        Restart.onClick.AddListener(Game.Instance.ResetProgress);
    }
}