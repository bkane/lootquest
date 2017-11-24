using UnityEngine;

namespace Assets.Scripts.Model
{
    /// <summary>
    /// Studio after IPO
    /// </summary>
    public class PublicModel
    {
        public bool IsActive { get; set; }

        private LootBoxModel model;

        public int DevAllocation          = 0;
        public int MarketerAllocation     = 0;
        public int LobbyistAllocation     = 0;
        public int CPUAllocation          = 0;
        public int BioengineerAllocation  = 0;

        public PublicModel(LootBoxModel model)
        {
            this.model = model;
        }

        private float UsedBudget()
        {
            return (DevAllocation + MarketerAllocation + LobbyistAllocation + CPUAllocation + BioengineerAllocation);
        }

        public void Allocate(Units type)
        {
            float availableBudget = 100 - UsedBudget();

            if (availableBudget > 0)
            {
                switch (type)
                {
                    case Units.Developer:   { DevAllocation         = Mathf.Clamp(DevAllocation             + 10, 0, 100); } break;
                    case Units.Marketer:    { MarketerAllocation    = Mathf.Clamp(MarketerAllocation        + 10, 0, 100); } break;
                    case Units.Lobbyist:    { LobbyistAllocation    = Mathf.Clamp(LobbyistAllocation        + 10, 0, 100); } break;
                    case Units.CPU:         { CPUAllocation         = Mathf.Clamp(CPUAllocation             + 10, 0, 100); } break;
                    case Units.Bioengineer: { BioengineerAllocation = Mathf.Clamp(BioengineerAllocation     + 10, 0, 100); } break;
                }
            }
        }
        public void Deallocate(Units type)
        {
            switch (type)
            {
                    case Units.Developer:   { DevAllocation         = Mathf.Clamp(DevAllocation             - 10, 0, 100); } break;
                    case Units.Marketer:    { MarketerAllocation    = Mathf.Clamp(MarketerAllocation        - 10, 0, 100); } break;
                    case Units.Lobbyist:    { LobbyistAllocation    = Mathf.Clamp(LobbyistAllocation        - 10, 0, 100); } break;
                    case Units.CPU:         { CPUAllocation         = Mathf.Clamp(CPUAllocation             - 10, 0, 100); } break;
                    case Units.Bioengineer: { BioengineerAllocation = Mathf.Clamp(BioengineerAllocation     - 10, 0, 100); } break;
            }
        }

        public BigNum MicrotransactionRevenuePerCustomerPerTick()
        {
            return (model.Studio.MicrotransactionRevenuePerTick() + model.LootBoxTypes) / model.ActivePlayers;
        }

        public BigNum PercentWhoMonetize()
        {
            return model.Studio.PercentOfPlayersWhoMonetize();
        }

        public BigNum MicrotransactionRevenuePerTick()
        {
            return model.ActivePlayers * PercentWhoMonetize() * MicrotransactionRevenuePerCustomerPerTick();
        }

        public BigNum CustomerAcquisitionPerTick()
        {
            return model.Marketers;
        }

        public BigNum GetBudget()
        {
            return MicrotransactionRevenuePerTick();
        }

        public BigNum GetMaxCustomers()
        {
            return model.Resources[Units.ActivePlayer].MaxValue;
        }

        public BigNum GetBaseOperatingCosts()
        {
            float percent = 0.5f;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.Layoffs))
            {
                percent /= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.ContractEmployees))
            {
                percent /= 2;
            }

            return GetBudget() * percent;
        }

        public BigNum GetFines()
        {
            return 0f;
        }

        public void UpdateMaxCustomers()
        {
            BigNum amount = 30e6f;

            //Additions
            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.PurchaseBelovedStudio))
            {
                amount += 30e6f;
            }


            //Multiplications
            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.TargetChildren))
            {
                amount *= 2;
            }

            model.Resources[Units.ActivePlayer].MaxValue = amount;
        }

        public void Tick()
        {
            if (!IsActive) { return; }

            //Set resources according to budget
            BigNum budget = GetBudget();
            budget -= GetBaseOperatingCosts();
            budget -= GetFines();

            BigNum costPerResource = 10000; //TODO: everything costs the same

            model.Resources[Units.Developer].Amount     = budget / costPerResource * (DevAllocation / 100f);
            model.Resources[Units.Marketer].Amount      = budget / costPerResource * (MarketerAllocation / 100f);
            model.Resources[Units.Lobbyist].Amount      = budget / costPerResource * (LobbyistAllocation / 100f);
            model.Resources[Units.CPU].Amount           = budget / costPerResource * (CPUAllocation / 100f);
            model.Resources[Units.Bioengineer].Amount   = budget / costPerResource * (BioengineerAllocation / 100f);


            BigNum spent = budget * UsedBudget();
            model.ConsumeUpTo(Units.Money, spent);

            //Generate resources
            model.Add(Units.LootBoxType, model.Developers);
            model.Add(Units.ActivePlayer, CustomerAcquisitionPerTick()); //TODO: cap
            model.Add(Units.Favor, model.Lobbyists);
            model.Add(Units.Cycle, model.CPUs);
            model.Add(Units.GenomeData, model.Bioengineers);

            //Finally, make money
            model.Add(Units.Money, MicrotransactionRevenuePerTick());

            //Make a note of the highest operating cost we've seen
            model.Resources[Units.OperatingCost].Amount = Mathf.Max(model.HighestOperatingCost, GetBaseOperatingCosts());
        }
    }
}

