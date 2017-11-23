using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class PublicViewController : MonoBehaviour
    {
        public LootBoxModel Model;



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
            DevPlus.onClick.AddListener(() => Model.Public.Allocate(Units.Developer));
            DevMinus.onClick.AddListener(() => Model.Public.Deallocate(Units.Developer));

            MarketingPlus.onClick.AddListener(() => Model.Public.Allocate(Units.Marketer));
            MarketingMinus.onClick.AddListener(() => Model.Public.Deallocate(Units.Marketer));

            DataAnalysisPlus.onClick.AddListener(() => Model.Public.Allocate(Units.DataAnalyst));
            DataAnalysisMinus.onClick.AddListener(() => Model.Public.Deallocate(Units.DataAnalyst));

            LobbyingPlus.onClick.AddListener(() => Model.Public.Allocate(Units.Lobbyist));
            LobbyingMinus.onClick.AddListener(() => Model.Public.Deallocate(Units.Lobbyist));

            MLPlus.onClick.AddListener(() => Model.Public.Allocate(Units.CPU));
            MLMinus.onClick.AddListener(() => Model.Public.Deallocate(Units.CPU));

            BioPlus.onClick.AddListener(() => Model.Public.Allocate(Units.Bioengineer));
            BioMinus.onClick.AddListener(() => Model.Public.Deallocate(Units.Bioengineer));
        }

        private void Update()
        {
            //Resources
            CustomersText.text = string.Format("Customerbase: {0}", Model.Customers);
            MicrotransactionRevenueText.text = string.Format("MTXN Rev: ${0}/s", Model.Public.MicrotransactionRevenuePerTick() * 30);
            MicrotransactionRevenuePerCustomerPerTickText.text = string.Format("MTXN Spend Rate: ${0}/customer", Model.Public.MicrotransactionRevenuePerCustomerPerTick() * 30);
            PercentWhoMonetizeText.text = string.Format("% customers monetize: {0}%", Model.Public.PercentWhoMonetize() * 100);

            //Budget
            DevAllocationText.text = string.Format("{0:0}%", Model.Public.DevAllocation * 100);
            MarketerAllocationText.text = string.Format("{0:0}%", Model.Public.MarketerAllocation * 100);
            DataAnalystAllocationText.text = string.Format("{0:0}%", Model.Public.DataAnalystAllocation * 100);
            LobbyistAllocationText.text = string.Format("{0:0}%", Model.Public.LobbyistAllocation * 100);
            CPUAllocationText.text = string.Format("{0:0}%", Model.Public.CPUAllocation * 100);
            BioengineerAllocationText.text = string.Format("{0:0}%", Model.Public.BioengineerAllocation * 100);


            //Employees
            DeveloperText.text = string.Format("Devs: {0}", Model.Developers);
            DevHoursText.text = string.Format("Dev Hours: {0}", Model.DevHours);

            DataAnalystsText.text = string.Format("Data Analysts: {0}", Model.DataAnalysts);
            CustomerDataText.text = string.Format("Customer Data: {0} GB", Model.CustomerData);

            MarketersText.text = string.Format("Marketers: {0}", Model.Marketers);
            CustomerAcquisitionRateText.text = string.Format("Customer Aquisition: {0}/s", Model.Public.CustomerAcquisitionPerTick() * 30);

            LobbyistsText.text = string.Format("Lobbyists: {0}", Model.Lobbyists);
            FavorText.text = string.Format("Gov't Favor: {0}", Model.Favor);

            CPUsText.text = string.Format("CPUs: {0}", Model.CPUs);
            CyclesText.text = string.Format("CPU Cycles: {0}", Model.Cycles);

            BioengineersText.text = string.Format("BioEngineers: {0}", Model.Bioengineers);
            GenomeDataText.text = string.Format("Genome Data: {0}GB", Model.GenomeData);

            MicrotransactionRevenueText.gameObject.SetActive(Model.UpgradeManager.IsActive(Scripts.Model.Upgrade.EUpgradeType.EnableMicrotransactions));
        }
    }
}