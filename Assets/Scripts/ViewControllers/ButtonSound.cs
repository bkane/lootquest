using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class ButtonSound : MonoBehaviour
    {
        private void Awake()
        {
            Button button = GetComponent<Button>();

            if (button != null)
            {
                button.onClick.AddListener(() => { DarkTonic.MasterAudio.MasterAudio.PlaySound("click"); });
            }
        }
    }
}
