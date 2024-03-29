﻿using UnityEngine;

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

        public static void Allocate(Units type)
        {
            float availableBudget = 100 - LootBoxModel.Instance.Public.UsedBudget();

            if (availableBudget > 0)
            {
                switch (type)
                {
                    case Units.Developer:   { LootBoxModel.Instance.Public.DevAllocation         = Mathf.Clamp(LootBoxModel.Instance.Public.DevAllocation             + 10, 0, 100); } break;
                    case Units.Marketer:    { LootBoxModel.Instance.Public.MarketerAllocation    = Mathf.Clamp(LootBoxModel.Instance.Public.MarketerAllocation        + 10, 0, 100); } break;
                    case Units.Lobbyist:    { LootBoxModel.Instance.Public.LobbyistAllocation    = Mathf.Clamp(LootBoxModel.Instance.Public.LobbyistAllocation        + 10, 0, 100); } break;
                    case Units.CPU:         { LootBoxModel.Instance.Public.CPUAllocation         = Mathf.Clamp(LootBoxModel.Instance.Public.CPUAllocation             + 10, 0, 100); } break;
                    case Units.Bioengineer: { LootBoxModel.Instance.Public.BioengineerAllocation = Mathf.Clamp(LootBoxModel.Instance.Public.BioengineerAllocation     + 10, 0, 100); } break;
                }
            }
            LootBoxModel.Instance.Add(Units.Click, 1);
        }
        public static void Deallocate(Units type)
        {
            switch (type)
            {
                    case Units.Developer:   { LootBoxModel.Instance.Public.DevAllocation         = Mathf.Clamp(LootBoxModel.Instance.Public.DevAllocation             - 10, 0, 100); } break;
                    case Units.Marketer:    { LootBoxModel.Instance.Public.MarketerAllocation    = Mathf.Clamp(LootBoxModel.Instance.Public.MarketerAllocation        - 10, 0, 100); } break;
                    case Units.Lobbyist:    { LootBoxModel.Instance.Public.LobbyistAllocation    = Mathf.Clamp(LootBoxModel.Instance.Public.LobbyistAllocation        - 10, 0, 100); } break;
                    case Units.CPU:         { LootBoxModel.Instance.Public.CPUAllocation         = Mathf.Clamp(LootBoxModel.Instance.Public.CPUAllocation             - 10, 0, 100); } break;
                    case Units.Bioengineer: { LootBoxModel.Instance.Public.BioengineerAllocation = Mathf.Clamp(LootBoxModel.Instance.Public.BioengineerAllocation     - 10, 0, 100); } break;
            }
            LootBoxModel.Instance.Add(Units.Click, 1);
        }

        public BigNum MicrotransactionRevenuePerCustomerPerTick()
        {
            BigNum amount = model.Studio.MicrotransactionRevenuePerTick() / model.ActivePlayers;

            amount += model.LootBoxTypes / 100f;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.ReduceLootBoxOdds))
            {
                amount *= 1.2f;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.ReduceLootBoxOddsToZero))
            {
                amount *= 1.2f;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.EnforceWatchingAds))
            {
                amount *= 2f;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.EmitInaudibleSound))
            {
                amount *= 2f;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.SellBuffsToOdds))
            {
                amount *= 2f;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.OptimizeRoAS))
            {
                amount *= 2f;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.TargetedPersonalAds))
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

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.Layoffs))
            {
                percent /= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.ContractEmployees))
            {
                percent /= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.ComputersMaintainGame))
            {
                percent = 0;
            }

            return GetBudget() * percent;
        }

        public BigNum GetFines()
        {
            BigNum amount = 0;
            BigNum fineFraction = 0.45f;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.ReduceFines))
            {
                fineFraction /= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.DiscloseOdds))
            {
                fineFraction /= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.ReduceLootBoxOddsToZero))
            {
                amount = GetBudget() * fineFraction;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.RollBackBan))
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

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.PurchaseBelovedStudio))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.TargetMinnows))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.UseDataBreach))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.CauseDataBreach))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.TargetChildren))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.DetermineDesires))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.IsolateMicrotransactionGene))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.LaunchMeshNetwork))
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
            model.Add(Units.LootBoxType, model.Developers / 1000f);
            model.Add(Units.ActivePlayer, CustomerAcquisitionPerTick());
            model.Add(Units.Favor, FavorPerTick());
            model.Add(Units.Cycle, CyclesPerTick());
            model.Add(Units.GenomeData, GenomePerTick());

            //Make a note of the highest operating cost we've seen
            model.Resources[Units.OperatingCost].Amount = Mathf.Max(model.HighestOperatingCost, GetBaseOperatingCosts());
        }
    }
}

