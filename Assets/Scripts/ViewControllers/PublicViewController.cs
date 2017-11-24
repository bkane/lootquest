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
        public TextMeshProUGUI MaxCustomersText;
        public TextMeshProUGUI MicrotransactionRevenueText;
        public TextMeshProUGUI MicrotransactionRevenuePerCustomerPerTickText;
        public TextMeshProUGUI PercentWhoMonetizeText;

        //Budget Allocation
        public TextMeshProUGUI BudgetText;
        public TextMeshProUGUI BaseOperatingCostsText;
        
        public GameObject FinesLineItem;
        public TextMeshProUGUI FinesAllocationText;

        public TextMeshProUGUI DevAllocationText;
        public Button DevPlus;
        public Button DevMinus;

        public TextMeshProUGUI MarketerAllocationText;
        public Button MarketingPlus;
        public Button MarketingMinus;

        //public TextMeshProUGUI DataAnalystAllocationText;
        //public Button DataAnalysisPlus;
        //public Button DataAnalysisMinus;

        public GameObject LobbyistLineItem;
        public TextMeshProUGUI LobbyistAllocationText;
        public Button LobbyingPlus;
        public Button LobbyingMinus;

        public GameObject CPULineItem;
        public TextMeshProUGUI CPUAllocationText;
        public Button MLPlus;
        public Button MLMinus;

        public GameObject BioEngineerLineItem;
        public TextMeshProUGUI BioengineerAllocationText;
        public Button BioPlus;
        public Button BioMinus;

        //Employees
        public TextMeshProUGUI DeveloperText;
        public TextMeshProUGUI MTXNImprovementText;

        public TextMeshProUGUI MarketersText;
        public TextMeshProUGUI CustomerAcquisitionRateText;

        public TextMeshProUGUI LobbyistsText;
        public TextMeshProUGUI FavorText;

        public TextMeshProUGUI CPUsText;
        public TextMeshProUGUI CyclesText;

        public TextMeshProUGUI BioengineersText;
        public TextMeshProUGUI GenomeDataText;



        private void Awake()
        {
            DevPlus.onClick.AddListener(() => LootBoxModel.Instance.Public.Allocate(Units.Developer));
            DevMinus.onClick.AddListener(() => LootBoxModel.Instance.Public.Deallocate(Units.Developer));

            MarketingPlus.onClick.AddListener(() => LootBoxModel.Instance.Public.Allocate(Units.Marketer));
            MarketingMinus.onClick.AddListener(() => LootBoxModel.Instance.Public.Deallocate(Units.Marketer));

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
            CustomersText.text = string.Format("Customerbase: {0}", LootBoxModel.Instance.ActivePlayers);
            MaxCustomersText.text = string.Format("Potential Customers: {0}", LootBoxModel.Instance.Public.GetMaxCustomers());
            MicrotransactionRevenueText.text = string.Format("Loot Box Rev: ${0}/s", LootBoxModel.Instance.Public.MicrotransactionRevenuePerTick() * 30);
            MicrotransactionRevenuePerCustomerPerTickText.text = string.Format("Loot Box Spend Rate: ${0}/customer", (BigNum) (LootBoxModel.Instance.Public.MicrotransactionRevenuePerCustomerPerTick() * 30));
            PercentWhoMonetizeText.text = string.Format("Percent monetize: {0}%", LootBoxModel.Instance.Public.PercentWhoMonetize() * 100);

            //Budget
            BudgetText.text = string.Format("${0}", LootBoxModel.Instance.Public.GetBudget());
            BaseOperatingCostsText.text = string.Format("${0}", LootBoxModel.Instance.Public.GetBaseOperatingCosts());
            FinesAllocationText.text = string.Format("${0}", LootBoxModel.Instance.Public.GetFines());
            FinesLineItem.gameObject.SetActive(LootBoxModel.Instance.Public.GetFines() > 0);

            DevAllocationText.text = string.Format("{0:0}%", LootBoxModel.Instance.Public.DevAllocation);
            MarketerAllocationText.text = string.Format("{0:0}%", LootBoxModel.Instance.Public.MarketerAllocation);
            LobbyistAllocationText.text = string.Format("{0:0}%", LootBoxModel.Instance.Public.LobbyistAllocation);
            CPUAllocationText.text = string.Format("{0:0}%", LootBoxModel.Instance.Public.CPUAllocation);
            BioengineerAllocationText.text = string.Format("{0:0}%", LootBoxModel.Instance.Public.BioengineerAllocation);


            //Employees
            DeveloperText.text = string.Format("Devs: {0}", LootBoxModel.Instance.Developers);
            MTXNImprovementText.text = string.Format("Types of Loot Boxes: {0}", LootBoxModel.Instance.LootBoxTypes);

            MarketersText.text = string.Format("Marketers: {0}", LootBoxModel.Instance.Marketers);
            CustomerAcquisitionRateText.text = string.Format("Customer Aquisition: {0}/s", LootBoxModel.Instance.Public.CustomerAcquisitionPerTick() * 30);

            LobbyistsText.text = string.Format("Lobbyists: {0}", LootBoxModel.Instance.Lobbyists);
            FavorText.text = string.Format("Gov't Favor: {0}", LootBoxModel.Instance.Favor);

            CPUsText.text = string.Format("CPUs: {0}", LootBoxModel.Instance.CPUs);
            CyclesText.text = string.Format("CPU Cycles: {0}", LootBoxModel.Instance.Cycles);

            BioengineersText.text = string.Format("BioEngineers: {0}", LootBoxModel.Instance.Bioengineers);
            GenomeDataText.text = string.Format("Genome Data: {0}GB", LootBoxModel.Instance.GenomeData);

            bool showLobby = LootBoxModel.Instance.UpgradeManager.IsActive(Upgrade.EUpgradeType.UnlockLobbying);
            LobbyistLineItem.SetActive(showLobby);
            LobbyistsText.gameObject.SetActive(showLobby);
            FavorText.gameObject.SetActive(showLobby);

            bool showCPU = LootBoxModel.Instance.UpgradeManager.IsActive(Upgrade.EUpgradeType.UnlockCPU);
            CPULineItem.SetActive(showCPU);
            CPUsText.gameObject.SetActive(showCPU);
            CyclesText.gameObject.SetActive(showCPU);

            bool showBio = LootBoxModel.Instance.UpgradeManager.IsActive(Upgrade.EUpgradeType.UnlockBioEngineering);
            BioEngineerLineItem.SetActive(showBio);
            BioengineersText.gameObject.SetActive(showBio);
            GenomeDataText.gameObject.SetActive(showBio);

        }
    }
}