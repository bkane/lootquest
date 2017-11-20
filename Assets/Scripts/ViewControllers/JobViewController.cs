using Assets.Scripts.Model;
using System.Collections.Generic;
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
        public Button DoJobButton;

        private void Awake()
        {
            DoJobButton.onClick.AddListener(DoJob);
        }

        private void DoJob()
        {
            if (Model.Consume(Units.Energy, 1))
            {
                Model.Click(1);
            }
        }

        private void Update()
        {
            MoneyText.text = string.Format("Money: ${0}", Model.Money);
        }
    }
}
