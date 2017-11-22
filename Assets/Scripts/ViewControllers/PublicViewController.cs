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
        public TextMeshProUGUI DataAnalystAllocationText;
        public TextMeshProUGUI LobbyistAllocationText;
        public TextMeshProUGUI CPUAllocationText;
        public TextMeshProUGUI BioengineerAllocationText;

        //Employees
        public TextMeshProUGUI DeveloperText;
        public TextMeshProUGUI DevHoursText;

        public TextMeshProUGUI DataAnalystsText;
        public TextMeshProUGUI CustomerDataText;

        public TextMeshProUGUI LobbyistsText;
        public TextMeshProUGUI FavorText;

        public TextMeshProUGUI CPUsText;
        public TextMeshProUGUI CyclesText;

        public TextMeshProUGUI BioengineersText;
        public TextMeshProUGUI GenomeDataText;


        //Buttons



        private void Awake()
        {

        }

        private void Update()
        {
            //Resources
            CustomersText.text = string.Format("Customerbase: {0}", Model.Customers);
            MicrotransactionRevenueText.text = string.Format("MTXN Rev: ${0}/s", Model.Public.MicrotransactionRevenuePerTick() * 30);
            MicrotransactionRevenuePerCustomerPerTickText.text = string.Format("MTXN Spend Rate: ${0}/customer", Model.Public.MicrotransactionRevenuePerCustomerPerTick() * 30);
            PercentWhoMonetizeText.text = string.Format("% customers monetize: {0}%", Model.Public.PercentWhoMonetize() * 100);

            //Budget
            DevAllocationText.text = string.Format("DevAllocation: {0}", Model.Public.DevAllocation);
            DataAnalystAllocationText.text = string.Format("DataAnalystAllocation: {0}", Model.Public.DataAnalystAllocation);
            LobbyistAllocationText.text = string.Format("LobbyistAllocation: {0}", Model.Public.LobbyistAllocation);
            CPUAllocationText.text = string.Format("CPUAllocation: {0}", Model.Public.CPUAllocation);
            BioengineerAllocationText.text = string.Format("BioengineerAllocation: {0}", Model.Public.BioengineerAllocation);


            //Employees
            DeveloperText.text = string.Format("Devs: {0}", Model.Developers);
            DevHoursText.text = string.Format("Dev Hours: {0}", Model.DevHours);

            DataAnalystsText.text = string.Format("Data Analysts: {0}", Model.DataAnalysts);
            CustomerDataText.text = string.Format("Customer Data: {0} GB", Model.CustomerData);

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

