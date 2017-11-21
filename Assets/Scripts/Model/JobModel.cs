namespace Assets.Scripts.Model
{
    public class JobModel
    {
        public bool IsActive { get; set; }

        private LootBoxModel model;

        public JobModel(LootBoxModel model)
        {
            this.model = model;
        }

        public void DoJob()
        {
            model.Add(Units.JobProgress, 10);

            if (model.Consume(Units.JobProgress, 100))
            {
                model.Add(Units.Money, 5);
            }
        }
    }
}
