namespace Assets.Scripts.Model
{
    public class LifeModel
    {
        public bool IsActive { get; set; }

        private LootBoxModel model;

        public LifeModel(LootBoxModel model)
        {
            this.model = model;
        }


        public void DoBuyCoffee()
        {
            if (model.ConsumeExactly(Units.Money, 2))
            {
                model.Add(Units.Caffeine, 1);
                Stats.Instance.CoffeeConsumed++;
            }
        }

        public void Tick()
        {
            if (!IsActive) { return; }

        }
    }
}
