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
            model.Add(Units.Click, 1);
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
            model.Add(Units.Click, 1);
        }

        public BigNum MicrotransactionRevenuePerCustomerPerTick()
        {
            BigNum amount = model.Studio.MicrotransactionRevenuePerTick() / model.ActivePlayers;

            amount += model.LootBoxTypes / 100000f;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.ReduceLootBoxOdds))
            {
                amount *= 1.2f;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.ReduceLootBoxOddsToZero))
            {
                amount *= 1.2f;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.EnforceWatchingAds))
            {
                amount *= 2f;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.EmitInaudibleSound))
            {
                amount *= 2f;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.SellBuffsToOdds))
            {
                amount *= 2f;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.OptimizeRoAS))
            {
                amount *= 2f;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.TargetedPersonalAds))
            {
                amount *= 2f;
            }

            return amount;
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
            if (model.ActivePlayers == model.Resources[Units.ActivePlayer].MaxValue)
            {
                return 0;
            }

            BigNum rate = 2f;

            return model.Marketers * rate;
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

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.ComputersMaintainGame))
            {
                percent = 0;
            }

            return GetBudget() * percent;
        }

        public BigNum GetFines()
        {
            BigNum amount = 0;
            BigNum fineFraction = 0.45f;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.ReduceFines))
            {
                fineFraction /= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.DiscloseOdds))
            {
                fineFraction /= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.ReduceLootBoxOddsToZero))
            {
                amount = GetBudget() * fineFraction;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.RollBackBan))
            {
                amount = 0;
            }

            return amount;
        }

        public BigNum FavorPerTick()
        {
            return model.Lobbyists / 5000f;
        }

        public BigNum CyclesPerTick()
        {
            return model.CPUs * 1000f;
        }

        public BigNum GenomePerTick()
        {
            return model.Bioengineers / 1e5f;
        }

        public void UpdateMaxCustomers()
        {
            //total should be 500M before multi
            BigNum amount = 30e6f;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.PurchaseBelovedStudio))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.TargetMinnows))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.UseDataBreach))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.CauseDataBreach))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.TargetChildren))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.DetermineDesires))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.IsolateMicrotransactionGene))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.LaunchMeshNetwork))
            {
                amount = 7.6e9f;
            }

            model.Resources[Units.ActivePlayer].MaxValue = amount;
        }

        public void Tick()
        {
            if (!IsActive) { return; }

            UpdateMaxCustomers();

            //Set resources according to budget
            BigNum budget = GetBudget();
            BigNum overhead = GetBaseOperatingCosts() + GetFines();
            budget -= overhead;

            BigNum devBudget = (budget * (DevAllocation / 100f));
            BigNum numDevs = 0;
            if (devBudget > 0) { numDevs = Mathf.Pow(devBudget, 0.5f) / 10; }
            model.Resources[Units.Developer].Amount = numDevs;

            BigNum marketBudget = (budget * (MarketerAllocation / 100f));
            BigNum numMarketers = 0;
            if (marketBudget > 0) { numMarketers = Mathf.Pow(marketBudget, 0.5f) / 10; }
            model.Resources[Units.Marketer].Amount = numMarketers;

            BigNum lobbyBudget = (budget * (LobbyistAllocation / 100f));
            BigNum numLobby = 0;
            if (lobbyBudget > 0) { numLobby = Mathf.Pow(lobbyBudget, 0.5f) / 10e3f; }
            model.Resources[Units.Lobbyist].Amount = numLobby;

            BigNum cpuBudget = (budget * (CPUAllocation / 100f));
            BigNum numCPU = 0;
            if (cpuBudget > 0) { numCPU = Mathf.Pow(cpuBudget, 0.5f) / 10; }
            model.Resources[Units.CPU].Amount = numCPU;

            BigNum bioBudget = (budget * (BioengineerAllocation / 100f));
            BigNum numBio = 0;
            if (bioBudget > 0) { numBio = Mathf.Pow(bioBudget, 0.5f) / 10e4f; }
            model.Resources[Units.Bioengineer].Amount = numBio;


            BigNum spent = overhead + devBudget + marketBudget + lobbyBudget + cpuBudget + bioBudget;
            model.ConsumeUpTo(Units.Money, spent);

            //Make money
            model.Add(Units.Money, MicrotransactionRevenuePerTick());

            //Generate resources
            model.Add(Units.LootBoxType, model.Developers);
            model.Add(Units.ActivePlayer, CustomerAcquisitionPerTick());
            model.Add(Units.Favor, FavorPerTick());
            model.Add(Units.Cycle, CyclesPerTick());
            model.Add(Units.GenomeData, GenomePerTick());

            //Make a note of the highest operating cost we've seen
            model.Resources[Units.OperatingCost].Amount = Mathf.Max(model.HighestOperatingCost, GetBaseOperatingCosts());
        }
    }
}

