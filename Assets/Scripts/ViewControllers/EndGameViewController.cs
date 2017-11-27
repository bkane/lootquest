using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class EndGameViewController : MonoBehaviour
    {
        public Button GoOutside;

        private void Awake()
        {
            GoOutside.onClick.AddListener(Game.Instance.EndGame);
        }
    }
}
