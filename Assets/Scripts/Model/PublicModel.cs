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
        public float DataAnalystAllocation  = 0f;
        public float LobbyistAllocation     = 0f;
        public float CPUAllocation          = 0f;
        public float BioengineerAllocation  = 0f;

        public PublicModel(LootBoxModel model)
        {
            this.model = model;
        }

        public void Allocate(Units type)
        {
            float availableBudget = 1 - (DevAllocation + MarketerAllocation + DataAnalystAllocation + LobbyistAllocation + CPUAllocation + BioengineerAllocation);

            if (availableBudget > 0)
            {
                switch (type)
                {
                    case Units.Developer:   { DevAllocation         = Mathf.Clamp01(DevAllocation           + 0.1f); } break;
                    case Units.Marketer:    { MarketerAllocation    = Mathf.Clamp01(MarketerAllocation      + 0.1f); } break;
                    case Units.Lobbyist:    { LobbyistAllocation    = Mathf.Clamp01(LobbyistAllocation      + 0.1f); } break;
                    case Units.DataAnalyst: { DataAnalystAllocation = Mathf.Clamp01(DataAnalystAllocation   + 0.1f); } break;
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
                    case Units.DataAnalyst: { DataAnalystAllocation = Mathf.Clamp01(DataAnalystAllocation   - 0.1f); } break;
                    case Units.CPU:         { CPUAllocation         = Mathf.Clamp01(CPUAllocation           - 0.1f); } break;
                    case Units.Bioengineer: { BioengineerAllocation = Mathf.Clamp01(BioengineerAllocation   - 0.1f); } break;
            }
        }

        public BigNum MicrotransactionRevenuePerCustomerPerTick()
        {
            return 0.25f / 30f;
        }

        public BigNum PercentWhoMonetize()
        {
            return 0.01f;
        }

        public BigNum MicrotransactionRevenuePerTick()
        {
            return model.Customers * PercentWhoMonetize() * MicrotransactionRevenuePerCustomerPerTick();
        }

        public BigNum CustomerAcquisitionPerTick()
        {
            return model.Marketers;
        }

        public void Tick()
        {
            if (!IsActive) { return; }

            //Set resources according to budget
            float budget = model.Money / 1000000; //TODO
            model.Resources[Units.Developer].Amount     = budget * DevAllocation;
            model.Resources[Units.Marketer].Amount      = budget * MarketerAllocation;
            model.Resources[Units.DataAnalyst].Amount   = budget * DataAnalystAllocation;
            model.Resources[Units.Lobbyist].Amount      = budget * LobbyistAllocation;
            model.Resources[Units.CPU].Amount           = budget * CPUAllocation;
            model.Resources[Units.Bioengineer].Amount   = budget * BioengineerAllocation;

            //Generate resources
            model.Add(Units.DevHour, model.Developers * StudioModel.DevHourPerDeveloperTick);
            model.Add(Units.Customer, CustomerAcquisitionPerTick());
            model.Add(Units.CustomerData, model.DataAnalysts * StudioModel.CustomerDataPerDataAnalystTick); //TODO: base on analysts? they make analytics data
            model.Add(Units.Favor, model.Lobbyists);
            model.Add(Units.Cycle, model.CPUs);
            model.Add(Units.GenomeData, model.Bioengineers);

            //Finally, make money
            model.Add(Units.Money, MicrotransactionRevenuePerTick());
        }
    }
}

