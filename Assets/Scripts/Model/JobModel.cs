namespace Assets.Scripts.Model
{
    public class JobModel
    {
        public bool IsActive { get; set; }

        private LootBoxModel model;

        public int TicksPerJobAutomation = 5;

        public JobModel(LootBoxModel model)
        {
            this.model = model;
        }

        public void DoJobClick()
        {
            BigNum baseJobAmount = 10;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.WorkSmarter))
            {
                baseJobAmount += 10;
            }

            DoJob(baseJobAmount);
        }

        public void DoJob(BigNum amount)
        {
            model.Add(Units.JobProgress, amount);

            if (model.Consume(Units.JobProgress, 100))
            {
                model.Add(Units.Money, MoneyPerJobCompleted());
                model.Add(Units.JobCompleted, 1);
            }
        }

        public BigNum MoneyPerJobCompleted()
        {
            BigNum baseValue = 5;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.DressForSuccess))
            {
                baseValue += 5;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.SecondJob))
            {
                baseValue *= 5;
            }

            return baseValue;
        }

        public void Tick()
        {
            if (!IsActive) { return; }

            if (model.TickCount % TicksPerJobAutomation == 0 &&
                model.UpgradeManager.IsActive(Upgrade.EUpgradeType.JobAutomationScript))
            {
                DoJob(1);
            }
        }
    }
}
