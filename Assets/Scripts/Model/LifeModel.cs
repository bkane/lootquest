namespace Assets.Scripts.Model
{
    public class LifeModel
    {
        private LootBoxModel model;

        public LifeModel(LootBoxModel model)
        {
            this.model = model;
        }

        public void DoSleep()
        {
            model.Add(Units.Energy, 2);
        }

        public void DoBuyCoffee()
        {
            if (model.Consume(Units.Money, 2))
            {
                model.Add(Units.Energy, 5);
                model.Add(Units.Caffeine, 1);
                Stats.Instance.CoffeeConsumed++;
            }
        }

        public void Tick()
        {
            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.SleepApp))
            {
                if (model.Resources[Units.Energy].Amount == 0)
                {
                    DoSleep();
                    DoSleep();
                    DoSleep();
                    DoSleep();
                }
            }
        }
    }
}
