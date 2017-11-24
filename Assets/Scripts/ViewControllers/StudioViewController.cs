using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class StudioViewController : MonoBehaviour
    {
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
        public TextMeshProUGUI AnalyticsDataText;

        //Buttons
        public Button HireDeveloper;
        public Button FireDeveloper;

        public Button HireDataAnalyst;
        public Button FireDataAnalyst;

        public Button ReleaseGame;

        private void Awake()
        {
            HireDeveloper.onClick.AddListener(LootBoxModel.Instance.Studio.HireDeveloper);
            FireDeveloper.onClick.AddListener(LootBoxModel.Instance.Studio.FireDeveloper);

            HireDataAnalyst.onClick.AddListener(LootBoxModel.Instance.Studio.HireDataAnalyst);
            FireDataAnalyst.onClick.AddListener(LootBoxModel.Instance.Studio.FireDataAnalyst);

            ReleaseGame.onClick.AddListener(LootBoxModel.Instance.Studio.ReleaseGame);
        }

        private void Update()
        {
            DeveloperText.text = string.Format("Devs: {0}", LootBoxModel.Instance.Developers);
            DevCostText.text = string.Format("Dev Cost: ${0}/s", LootBoxModel.Instance.Studio.DevCostPerTick() * 30);
            DevHoursText.text = string.Format("Dev Hours: {0}", LootBoxModel.Instance.DevHours);
            ReleasedGameText.text = string.Format("Released Games: {0}", LootBoxModel.Instance.ReleasedGames);
            HypeText.text = string.Format("Hype: {0}", LootBoxModel.Instance.Hype);
            CopiesSoldText.text = string.Format("Total Copies Sold: {0}", LootBoxModel.Instance.CopiesSold);
            ActivePlayersText.text = string.Format("Active Players: {0}", LootBoxModel.Instance.ActivePlayers);
            MicrotransactionRevenueText.text = string.Format("MTXN Rev: ${0}/s", LootBoxModel.Instance.Studio.MicrotransactionRevenuePerTick() * 30);

            DataAnalystsText.text = string.Format("Data Analysts: {0}", LootBoxModel.Instance.DataAnalysts);
            DataAnalystsCostText.text = string.Format("Data Analyst Cost: ${0}/s", LootBoxModel.Instance.Studio.DataAnalystCostPerTick() * 30);
            AnalyticsDataText.text = string.Format("Analytics Data: {0} GB", LootBoxModel.Instance.AnalyticsData);


            bool microTxnActive = LootBoxModel.Instance.UpgradeManager.IsActive(Upgrade.EUpgradeType.EnableMicrotransactions);
            MicrotransactionRevenueText.gameObject.SetActive(microTxnActive);
            DataAnalystsText.gameObject.SetActive(microTxnActive);
            DataAnalystsCostText.gameObject.SetActive(microTxnActive);
            AnalyticsDataText.gameObject.SetActive(microTxnActive);
            HireDataAnalyst.gameObject.SetActive(microTxnActive);
            FireDataAnalyst.gameObject.SetActive(microTxnActive);
        }
    }
}

