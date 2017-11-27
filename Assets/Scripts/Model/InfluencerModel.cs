namespace Assets.Scripts.Model
{
    public class InfluencerModel
    {
        public bool IsActive { get; set; }

        protected LootBoxModel model;

        public InfluencerModel(LootBoxModel model)
        {
            this.model = model;
        }

        public BigNum VideoProgressPerContent()
        {
            BigNum amount = 5;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.BuyVideoEditingSoftware))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.MakeVideoIntro))
            {
                amount *= 2;
            }

            return amount;
        }

        public void DoMakeVideoClick()
        {
            model.Add(Units.Click, 1);
            DoMakeVideo();
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
            BigNum moneyPerFollowerPerTick = 0.00001f;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.DoSponsoredVideos))
            {
                moneyPerFollowerPerTick *= 1.5f;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.CompletelySellOut))
            {
                moneyPerFollowerPerTick *= 2f;
            }

            return model.Followers * moneyPerFollowerPerTick;
        }

        public BigNum FollowersPerTick()
        {
            BigNum amount = 10 / 30f;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.StartStreaming))
            {
                amount *= 1.3f;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.OptimizeContentForChannelGrowth))
            {
                amount *= 1.5f;
            }

            return amount * model.PublishedVideos;
        }

        public int TicksPerVideoEditor()
        {
            int ticks = 20;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.HireProVideoEditor))
            {
                ticks /= 4;
            }

            return ticks;
        }

        public void Tick()
        {
            if (!IsActive) { return; }

            //Ad rev
            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.GetPartnered))
            {
                model.Add(Units.Money, AdRevenuePerTick());
            }

            //Channel Growth
            model.Add(Units.Follower, FollowersPerTick());

            //Auto-video production
            if (model.TickCount % TicksPerVideoEditor() == 0 &&
                model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.HireVideoEditor))
            {
                DoMakeVideo();
            }
        }
    }
}
