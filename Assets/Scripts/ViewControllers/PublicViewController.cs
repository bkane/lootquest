using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class PublicViewController : MonoBehaviour
    {
        //Resources
        public TextMeshProUGUI CustomersText;
        public TextMeshProUGUI MicrotransactionRevenueText;
        public TextMeshProUGUI MicrotransactionRevenuePerCustomerPerTickText;
        public TextMeshProUGUI PercentWhoMonetizeText;

        //Budget Allocation
        public TextMeshProUGUI DevAllocationText;
        public Button DevPlus;
        public Button DevMinus;

        public TextMeshProUGUI MarketerAllocationText;
        public Button MarketingPlus;
        public Button MarketingMinus;

        public TextMeshProUGUI DataAnalystAllocationText;
        public Button DataAnalysisPlus;
        public Button DataAnalysisMinus;

        public TextMeshProUGUI LobbyistAllocationText;
        public Button LobbyingPlus;
        public Button LobbyingMinus;

        public TextMeshProUGUI CPUAllocationText;
        public Button MLPlus;
        public Button MLMinus;

        public TextMeshProUGUI BioengineerAllocationText;
        public Button BioPlus;
        public Button BioMinus;

        //Employees
        public TextMeshProUGUI DeveloperText;
        public TextMeshProUGUI DevHoursText;

        public TextMeshProUGUI DataAnalystsText;
        public TextMeshProUGUI CustomerDataText;

        public TextMeshProUGUI MarketersText;
        public TextMeshProUGUI CustomerAcquisitionRateText;

        public TextMeshProUGUI LobbyistsText;
        public TextMeshProUGUI FavorText;

        public TextMeshProUGUI CPUsText;
        public TextMeshProUGUI CyclesText;

        public TextMeshProUGUI BioengineersText;
        public TextMeshProUGUI GenomeDataText;


        //Buttons



        private void Awake()
        {
            DevPlus.onClick.AddListener(() => LootBoxModel.Instance.Public.Allocate(Units.Developer));
            DevMinus.onClick.AddListener(() => LootBoxModel.Instance.Public.Deallocate(Units.Developer));

            MarketingPlus.onClick.AddListener(() => LootBoxModel.Instance.Public.Allocate(Units.Marketer));
            MarketingMinus.onClick.AddListener(() => LootBoxModel.Instance.Public.Deallocate(Units.Marketer));

            DataAnalysisPlus.onClick.AddListener(() => LootBoxModel.Instance.Public.Allocate(Units.DataAnalyst));
            DataAnalysisMinus.onClick.AddListener(() => LootBoxModel.Instance.Public.Deallocate(Units.DataAnalyst));

            LobbyingPlus.onClick.AddListener(() => LootBoxModel.Instance.Public.Allocate(Units.Lobbyist));
            LobbyingMinus.onClick.AddListener(() => LootBoxModel.Instance.Public.Deallocate(Units.Lobbyist));

            MLPlus.onClick.AddListener(() => LootBoxModel.Instance.Public.Allocate(Units.CPU));
            MLMinus.onClick.AddListener(() => LootBoxModel.Instance.Public.Deallocate(Units.CPU));

            BioPlus.onClick.AddListener(() => LootBoxModel.Instance.Public.Allocate(Units.Bioengineer));
            BioMinus.onClick.AddListener(() => LootBoxModel.Instance.Public.Deallocate(Units.Bioengineer));
        }

        private void Update()
        {
            //Resources
            CustomersText.text = string.Format("Customerbase: {0}", LootBoxModel.Instance.Customers);
            MicrotransactionRevenueText.text = string.Format("MTXN Rev: ${0}/s", LootBoxModel.Instance.Public.MicrotransactionRevenuePerTick() * 30);
            MicrotransactionRevenuePerCustomerPerTickText.text = string.Format("MTXN Spend Rate: ${0}/customer", LootBoxModel.Instance.Public.MicrotransactionRevenuePerCustomerPerTick() * 30);
            PercentWhoMonetizeText.text = string.Format("% customers monetize: {0}%", LootBoxModel.Instance.Public.PercentWhoMonetize() * 100);

            //Budget
            DevAllocationText.text = string.Format("{0:0}%", LootBoxModel.Instance.Public.DevAllocation * 100);
            MarketerAllocationText.text = string.Format("{0:0}%", LootBoxModel.Instance.Public.MarketerAllocation * 100);
            DataAnalystAllocationText.text = string.Format("{0:0}%", LootBoxModel.Instance.Public.DataAnalystAllocation * 100);
            LobbyistAllocationText.text = string.Format("{0:0}%", LootBoxModel.Instance.Public.LobbyistAllocation * 100);
            CPUAllocationText.text = string.Format("{0:0}%", LootBoxModel.Instance.Public.CPUAllocation * 100);
            BioengineerAllocationText.text = string.Format("{0:0}%", LootBoxModel.Instance.Public.BioengineerAllocation * 100);


            //Employees
            DeveloperText.text = string.Format("Devs: {0}", LootBoxModel.Instance.Developers);
            DevHoursText.text = string.Format("Dev Hours: {0}", LootBoxModel.Instance.DevHours);

            DataAnalystsText.text = string.Format("Data Analysts: {0}", LootBoxModel.Instance.DataAnalysts);
            CustomerDataText.text = string.Format("Customer Data: {0} GB", LootBoxModel.Instance.AnalyticsData);

            MarketersText.text = string.Format("Marketers: {0}", LootBoxModel.Instance.Marketers);
            CustomerAcquisitionRateText.text = string.Format("Customer Aquisition: {0}/s", LootBoxModel.Instance.Public.CustomerAcquisitionPerTick() * 30);

            LobbyistsText.text = string.Format("Lobbyists: {0}", LootBoxModel.Instance.Lobbyists);
            FavorText.text = string.Format("Gov't Favor: {0}", LootBoxModel.Instance.Favor);

            CPUsText.text = string.Format("CPUs: {0}", LootBoxModel.Instance.CPUs);
            CyclesText.text = string.Format("CPU Cycles: {0}", LootBoxModel.Instance.Cycles);

            BioengineersText.text = string.Format("BioEngineers: {0}", LootBoxModel.Instance.Bioengineers);
            GenomeDataText.text = string.Format("Genome Data: {0}GB", LootBoxModel.Instance.GenomeData);

            MicrotransactionRevenueText.gameObject.SetActive(LootBoxModel.Instance.UpgradeManager.IsActive(Upgrade.EUpgradeType.EnableMicrotransactions));
        }
    }
}