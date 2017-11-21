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
        public TextMeshProUGUI EnergyText;

        //Buttons
        public Button SleepButton;
        public Button BuyCoffeeButton;

        private void Awake()
        {
            SleepButton.onClick.AddListener(Model.Life.DoSleep);
            BuyCoffeeButton.onClick.AddListener(Model.Life.DoBuyCoffee);
        }

        private void Update()
        {
            TimeText.text = string.Format("Time: {0}", Model.Time.GetTimeString());
            EnergyText.text = string.Format("Energy: {0}", Model.Energy);
        }
    }
}
