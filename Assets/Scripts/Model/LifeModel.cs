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

        public void Tick()
        {
            if (!IsActive) { return; }

        }
    }
}
