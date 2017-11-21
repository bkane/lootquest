namespace Assets.Scripts.Model
{
    public class JobModel
    {
        private LootBoxModel model;

        public JobModel(LootBoxModel model)
        {
            this.model = model;
        }

        public void DoJob()
        {
            if (model.Consume(Units.Energy, 1))
            {
                model.Click(1);
            }
        }
    }
}
