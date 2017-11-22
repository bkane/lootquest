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
        public TextMeshProUGUI GameProgressText;
        public TextMeshProUGUI ReleasedGameText;
        public TextMeshProUGUI HypeText;
        public TextMeshProUGUI CopiesSoldText;
        public TextMeshProUGUI MicrotransactionRevenueText;

        //Buttons
        public Button HireDeveloper;
        public Button FireDeveloper;

        private void Awake()
        {
            HireDeveloper.onClick.AddListener(Model.Studio.HireDeveloper);
            FireDeveloper.onClick.AddListener(Model.Studio.FireDeveloper);
        }

        private void Update()
        {
            DeveloperText.text = string.Format("Devs: {0}", Model.Developers);
            DevCostText.text = string.Format("Dev Cost: ${0}/s", Model.Studio.DevCostPerTick() * 30);
            GameProgressText.text = string.Format("Game Progress: {0}%", Model.GameProgress);
            ReleasedGameText.text = string.Format("Released Games: {0}", Model.ReleasedGames);
            HypeText.text = string.Format("Hype: {0}", Model.Hype);
            CopiesSoldText.text = string.Format("Copies Sold: {0}", Model.CopiesSold);
            MicrotransactionRevenueText.text = string.Format("MTXN Rev: ${0}/s", Model.Studio.MicrotransactionRevenuePerTick() * 30);

            MicrotransactionRevenueText.gameObject.SetActive(Model.UpgradeManager.IsActive(Scripts.Model.Upgrade.EUpgradeType.EnableMicrotransactions));
        }
    }
}

