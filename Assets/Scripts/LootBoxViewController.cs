using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LootBoxViewController : MonoBehaviour
    {
        public LootBoxModel Model;

        public TextMeshProUGUI MoneyText;
        public Button MoneyButton;

        public TextMeshProUGUI MoneyPerClickText;
        public Button MoneyPerClickButton;

        private void Awake()
        {
            MoneyButton.onClick.AddListener(() => { Model.AddMoney(Model.MoneyPerClick); });
            MoneyPerClickButton.onClick.AddListener(() => { Model.AddMoneyPerClick(10); });
        }

        private void Update()
        {
            MoneyText.text = string.Format("Money: ${0}", Model.Money);
            MoneyPerClickText.text = string.Format("Money per click: ${0}", Model.MoneyPerClick);
        }
    }
}
