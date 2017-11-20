using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class JobViewController : MonoBehaviour
    {
        public LootBoxModel Model;

        //Labels
        public TextMeshProUGUI MoneyText;

        //Buttons
        public Button MakeMoneyButton;

        private void Awake()
        {
            MakeMoneyButton.onClick.AddListener(() => { Model.Click(1); });
        }

        private void Update()
        {
            MoneyText.text = string.Format("Money: ${0}", Model.Money);
        }
    }
}
