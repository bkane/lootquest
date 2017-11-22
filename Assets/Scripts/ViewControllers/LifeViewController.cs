using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class LifeViewController : MonoBehaviour
    {
        public LootBoxModel Model;

        //Labels
        public TextMeshProUGUI TimeText;
        public TextMeshProUGUI MoneyText;

        //Buttons
        public Button BuyCoffeeButton;

        private void Awake()
        {
            BuyCoffeeButton.onClick.AddListener(Model.Life.DoBuyCoffee);
        }

        private void Update()
        {
            TimeText.text = string.Format("Time: {0}", Model.Time.GetTimeString());
            MoneyText.text = string.Format("Money: ${0}", Model.Money);
        }
    }
}
