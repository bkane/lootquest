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

        public float DevAllocation          = 0f;
        public float MarketerAllocation     = 0f;
        public float LobbyistAllocation     = 0f;
        public float CPUAllocation          = 0f;
        public float BioengineerAllocation  = 0f;

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
            float availableBudget = 1 - UsedBudget();

            if (availableBudget > 0)
            {
                switch (type)
                {
                    case Units.Developer:   { DevAllocation         = Mathf.Clamp01(DevAllocation           + 0.1f); } break;
                    case Units.Marketer:    { MarketerAllocation    = Mathf.Clamp01(MarketerAllocation      + 0.1f); } break;
                    case Units.Lobbyist:    { LobbyistAllocation    = Mathf.Clamp01(LobbyistAllocation      + 0.1f); } break;
                    case Units.CPU:         { CPUAllocation         = Mathf.Clamp01(CPUAllocation           + 0.1f); } break;
                    case Units.Bioengineer: { BioengineerAllocation = Mathf.Clamp01(BioengineerAllocation   + 0.1f); } break;
                }
            }
        }
        public void Deallocate(Units type)
        {
            switch (type)
            {
                case Units.Developer:   { DevAllocation         = Mathf.Clamp01(DevAllocation           - 0.1f); } break;
                case Units.Marketer:    { MarketerAllocation    = Mathf.Clamp01(MarketerAllocation      - 0.1f); } break;
                case Units.Lobbyist:    { LobbyistAllocation    = Mathf.Clamp01(LobbyistAllocation      - 0.1f); } break;
                case Units.CPU:         { CPUAllocation         = Mathf.Clamp01(CPUAllocation           - 0.1f); } break;
                case Units.Bioengineer: { BioengineerAllocation = Mathf.Clamp01(BioengineerAllocation   - 0.1f); } break;
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

        public BigNum GetBaseOperatingCosts()
        {
            return 100000f;
        }

        public BigNum GetFines()
        {
            return 0f;
        }

        public void Tick()
        {
            if (!IsActive) { return; }

            //Set resources according to budget
            BigNum budget = GetBudget();
            budget -= GetFines();

            BigNum costPerResource = 10000; //TODO: everything costs the same

            model.Resources[Units.Developer].Amount     = budget / costPerResource * DevAllocation;
            model.Resources[Units.Marketer].Amount      = budget / costPerResource * MarketerAllocation;
            model.Resources[Units.Lobbyist].Amount      = budget / costPerResource * LobbyistAllocation;
            model.Resources[Units.CPU].Amount           = budget / costPerResource * CPUAllocation;
            model.Resources[Units.Bioengineer].Amount   = budget / costPerResource * BioengineerAllocation;


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
        }
    }
}

