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

        public void DoJobClick()
        {
            BigNum baseJobAmount = 10;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.WorkSmarter))
            {
                baseJobAmount += 10;
            }

            DoJob(baseJobAmount);

            model.Add(Units.Click, 1);
        }

        public void DoJob(BigNum amount)
        {
            model.Add(Units.JobProgress, amount);

            if (model.ConsumeExactly(Units.JobProgress, 100))
            {
                model.Add(Units.Money, MoneyPerJobCompleted());
                model.Add(Units.JobCompleted, 1);
            }
        }

        public BigNum MoneyPerJobCompleted()
        {
            BigNum baseValue = 2;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.DressForSuccess))
            {
                baseValue += 3;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.SecondJob))
            {
                baseValue *= 2;
            }

            return baseValue;
        }

        public BigNum TicksPerJobAutomation()
        {
            BigNum amount = 4;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.FasterComputer))
            {
                amount /= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.WatercooledComputer))
            {
                amount /= 2;
            }

            return amount;
        }

        public void Tick()
        {
            if (!IsActive) { return; }

            if (model.TickCount % TicksPerJobAutomation() == 0 &&
                model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.JobAutomationScript))
            {
                DoJob(5);
            }
        }
    }
}
