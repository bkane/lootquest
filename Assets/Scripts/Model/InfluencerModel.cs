namespace Assets.Scripts.Model
{
    public class InfluencerModel
    {
        public bool IsActive { get; set; }

        public int TicksPerVideoEditor = 30;

        protected LootBoxModel model;

        public InfluencerModel(LootBoxModel model)
        {
            this.model = model;
        }

        public BigNum VideoProgressPerContent()
        {
            BigNum amount = 5;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.BuyVideoEditingSoftware))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.MakeVideoIntro))
            {
                amount *= 2;
            }

            return amount;
        }

        public void DoMakeVideo()
        {
            if (model.ConsumeExactly(Units.VideoContent, 1))
            {
                model.Add(Units.VideoProgress, VideoProgressPerContent());

                if (model.ConsumeExactly(Units.VideoProgress, 100))
                {
                    model.Add(Units.PublishedVideo, 1);
                }
            }
        }

        public BigNum AdRevenuePerTick()
        {
            BigNum moneyPerFollowerPerTick = 0.0001f;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.DoSponsoredVideos))
            {
                moneyPerFollowerPerTick *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.CompletelySellOut))
            {
                moneyPerFollowerPerTick *= 10;
            }

            return model.Followers * moneyPerFollowerPerTick;
        }

        public BigNum FollowersPerVideoPerTick()
        {
            BigNum amount = 1 / 30f;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.StartStreaming))
            {
                amount *= 2;
            }

            return amount;
        }

        public void Tick()
        {
            if (!IsActive) { return; }

            //Ad rev
            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.GetPartnered))
            {
                model.Add(Units.Money, AdRevenuePerTick());
            }

            //Channel Growth
            model.Add(Units.Follower, model.PublishedVideos * FollowersPerVideoPerTick());

            //Auto-video production
            if (model.TickCount % TicksPerVideoEditor == 0 &&
                model.UpgradeManager.IsActive(Upgrade.EUpgradeType.HireVideoEditor))
            {
                DoMakeVideo();
            }
        }
    }
}
