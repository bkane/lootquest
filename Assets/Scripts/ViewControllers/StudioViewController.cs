using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class StudioViewController : MonoBehaviour
    {
        public TextMeshProUGUI DeveloperText;
        public TextMeshProUGUI DevCostText;
        public TextMeshProUGUI DevHoursText;
        public TextMeshProUGUI ReleasedGameText;
        public TextMeshProUGUI HypeText;
        public TextMeshProUGUI CopiesSoldText;
        

        public GameObject AnalyticsPanel;
        public TextMeshProUGUI ActivePlayersText;
        public TextMeshProUGUI PercentMonetizeText;
        public TextMeshProUGUI MicrotransactionRevenueText;


        public Button HireDeveloper;
        public Button HireDeveloper10;
        public Button FireDeveloper;
        public Button FireDeveloper10;


        public GameObject DataAnalystsPanel;
        public TextMeshProUGUI DataAnalystsText;
        public TextMeshProUGUI DataAnalystsCostText;
        public TextMeshProUGUI AnalyticsDataText;

        public Button HireDataAnalyst;
        public Button HireDataAnalyst10;
        public Button FireDataAnalyst;
        public Button FireDataAnalyst10;

        public Button ReleaseGame;

        private void Awake()
        {
            HireDeveloper.onClick.AddListener(() => { LootBoxModel.Instance.Studio.HireDeveloper(1); });
            HireDeveloper10.onClick.AddListener(() => { LootBoxModel.Instance.Studio.HireDeveloper(10); });
            FireDeveloper.onClick.AddListener(() => { LootBoxModel.Instance.Studio.FireDeveloper(1); });
            FireDeveloper10.onClick.AddListener(() => { LootBoxModel.Instance.Studio.FireDeveloper(10); });

            HireDataAnalyst.onClick.AddListener(() => { LootBoxModel.Instance.Studio.HireDataAnalyst(1); });
            HireDataAnalyst10.onClick.AddListener(() => { LootBoxModel.Instance.Studio.HireDataAnalyst(10); });
            FireDataAnalyst.onClick.AddListener(() => { LootBoxModel.Instance.Studio.FireDataAnalyst(1); });
            FireDataAnalyst10.onClick.AddListener(() => { LootBoxModel.Instance.Studio.FireDataAnalyst(10); });

            ReleaseGame.onClick.AddListener(LootBoxModel.Instance.Studio.ReleaseGame);
        }

        private void Update()
        {
            DeveloperText.text = string.Format("Devs: {0}", LootBoxModel.Instance.Developers);
            DevCostText.text = string.Format("Dev Cost: ${0}/s", (BigNum) (LootBoxModel.Instance.Studio.DevCostPerTick() * 30));
            DevHoursText.text = string.Format("Dev Hours: {0}", LootBoxModel.Instance.DevHours);
            ReleasedGameText.text = string.Format("Released Games: {0}", LootBoxModel.Instance.ReleasedGames);
            HypeText.text = string.Format("Hype: {0}", LootBoxModel.Instance.Hype);
            CopiesSoldText.text = string.Format("Total Copies Sold: {0}", LootBoxModel.Instance.CopiesSold);


            DataAnalystsText.text = string.Format("Data Analysts: {0}", LootBoxModel.Instance.DataAnalysts);
            DataAnalystsCostText.text = string.Format("Data Analyst Cost: ${0}/s", (BigNum)(LootBoxModel.Instance.Studio.DataAnalystCostPerTick() * 30));
            AnalyticsDataText.text = string.Format("Analytics Data: {0}", LootBoxModel.Instance.AnalyticsData);

            ReleaseGame.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("Release Game ({0})", LootBoxModel.Instance.Studio.CostOfGameInDevHours());

            DataAnalystsPanel.gameObject.SetActive(LootBoxModel.Instance.UpgradeManager.IsActive(Upgrade.EUpgradeType.EnableAnalytics));

            AnalyticsPanel.gameObject.SetActive(LootBoxModel.Instance.UpgradeManager.IsActive(Upgrade.EUpgradeType.EnableAnalytics));
            ActivePlayersText.text = string.Format("Active Players: {0}", LootBoxModel.Instance.ActivePlayers);
            PercentMonetizeText.text = string.Format("Players Monetized: {0}%", LootBoxModel.Instance.Studio.PercentOfPlayersWhoMonetize());
            MicrotransactionRevenueText.text = string.Format("MTXN Rev: ${0}/s", (BigNum)(LootBoxModel.Instance.Studio.MicrotransactionRevenuePerTick() * 30));
        }
    }
}

