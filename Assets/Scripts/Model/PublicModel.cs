namespace Assets.Scripts.Model
{
    /// <summary>
    /// Studio after IPO
    /// </summary>
    public class PublicModel
    {
        public bool IsActive { get; set; }

        private LootBoxModel model;

        public float DevAllocation = 0.1f;
        public float MarketerAllocation = 0.1f;
        public float DataAnalystAllocation = 0.1f;
        public float LobbyistAllocation = 0.1f;
        public float CPUAllocation = 0.1f;
        public float BioengineerAllocation = 0.1f;

        public PublicModel(LootBoxModel model)
        {
            this.model = model;
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
            float budget = 100; //TODO
            model.Resources[Units.Developer].Amount     = budget * DevAllocation;
            model.Resources[Units.Marketer].Amount      = budget * MarketerAllocation;
            model.Resources[Units.DataAnalyst].Amount   = budget * DataAnalystAllocation;
            model.Resources[Units.Lobbyist].Amount      = budget * LobbyistAllocation;
            model.Resources[Units.CPU].Amount           = budget * CPUAllocation;
            model.Resources[Units.Bioengineer].Amount   = budget * BioengineerAllocation;

            //Generate resources
            model.Add(Units.DevHour, model.Developers * StudioModel.DevHourPerDeveloperTick);
            model.Add(Units.Customer, CustomerAcquisitionPerTick());
            model.Add(Units.CustomerData, model.DataAnalysts * StudioModel.CustomerDataPerDataAnalystTick);
            model.Add(Units.Favor, model.Lobbyists);
            model.Add(Units.Cycle, model.CPUs);
            model.Add(Units.GenomeData, model.Bioengineers);

            //Finally, make money
            model.Add(Units.Money, MicrotransactionRevenuePerTick());
        }
    }
}
