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
        public TextMeshProUGUI EnergyText;

        //Buttons
        public Button SleepButton;
        public Button BuyCoffeeButton;

        private void Awake()
        {
            SleepButton.onClick.AddListener(DoSleep);
            BuyCoffeeButton.onClick.AddListener(DoBuyCoffee);
        }

        private void DoSleep()
        {
            Model.Add(Units.Energy, 2);
        }

        private void DoBuyCoffee()
        {
            if (Model.Consume(Units.Money, 2))
            {
                Model.Add(Units.Energy, 5);
                Model.Add(Units.Caffeine, 1);
                Stats.Instance.CoffeeConsumed++;
            }
        }

        private void Update()
        {
            EnergyText.text = string.Format("Energy: {0}", Model.Energy);
        }
    }
}
