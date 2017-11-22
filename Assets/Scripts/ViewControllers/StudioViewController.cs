using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class StudioViewController : MonoBehaviour
    {
        public LootBoxModel Model;

        //Labels
        public TextMeshProUGUI DeveloperText;
        public TextMeshProUGUI DevCostText;
        public TextMeshProUGUI DevHoursText;
        public TextMeshProUGUI ReleasedGameText;
        public TextMeshProUGUI HypeText;
        public TextMeshProUGUI CopiesSoldText;
        public TextMeshProUGUI ActivePlayersText;
        public TextMeshProUGUI MicrotransactionRevenueText;

        public TextMeshProUGUI DataAnalystsText;
        public TextMeshProUGUI DataAnalystsCostText;
        public TextMeshProUGUI CustomerDataText;

        //Buttons
        public Button HireDeveloper;
        public Button FireDeveloper;

        public Button HireDataAnalyst;
        public Button FireDataAnalyst;

        public Button ReleaseGame;

        private void Awake()
        {
            HireDeveloper.onClick.AddListener(Model.Studio.HireDeveloper);
            FireDeveloper.onClick.AddListener(Model.Studio.FireDeveloper);

            HireDataAnalyst.onClick.AddListener(Model.Studio.HireDataAnalyst);
            FireDataAnalyst.onClick.AddListener(Model.Studio.FireDataAnalyst);

            ReleaseGame.onClick.AddListener(Model.Studio.ReleaseGame);
        }

        private void Update()
        {
            DeveloperText.text = string.Format("Devs: {0}", Model.Developers);
            DevCostText.text = string.Format("Dev Cost: ${0}/s", Model.Studio.DevCostPerTick() * 30);
            DevHoursText.text = string.Format("Dev Hours: {0}", Model.DevHours);
            ReleasedGameText.text = string.Format("Released Games: {0}", Model.ReleasedGames);
            HypeText.text = string.Format("Hype: {0}", Model.Hype);
            CopiesSoldText.text = string.Format("Total Copies Sold: {0}", Model.CopiesSold);
            ActivePlayersText.text = string.Format("Active Players: {0}", Model.ActivePlayers);
            MicrotransactionRevenueText.text = string.Format("MTXN Rev: ${0}/s", Model.Studio.MicrotransactionRevenuePerTick() * 30);

            DataAnalystsText.text = string.Format("Data Analysts: {0}", Model.DataAnalysts);
            DataAnalystsCostText.text = string.Format("Data Analyst Cost: ${0}/s", Model.Studio.DataAnalystCostPerTick() * 30);
            CustomerDataText.text = string.Format("Customer Data: {0} GB", Model.CustomerData);

            MicrotransactionRevenueText.gameObject.SetActive(Model.UpgradeManager.IsActive(Scripts.Model.Upgrade.EUpgradeType.EnableMicrotransactions));
        }
    }
}

