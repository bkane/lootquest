namespace Assets.Scripts.Model
{
    public class InfluencerModel
    {
        public bool IsActive { get; set; }

        public BigNum VideoProgressPerMakeVideoClick = 50;
        public int TicksPerVideoEditor = 30;
        public BigNum MoneyPerFollowerPerTick = 0.001f;

        public BigNum FollowersPerVideoPerTick = 1/30f;

        protected LootBoxModel model;

        public InfluencerModel(LootBoxModel model)
        {
            this.model = model;
        }

        public void DoMakeVideo()
        {
            if (model.Consume(Units.VideoContent, 1))
            {
                model.Add(Units.VideoProgress, VideoProgressPerMakeVideoClick);

                if (model.Consume(Units.VideoProgress, 100))
                {
                    model.Add(Units.PublishedVideo, 1);
                }
            }
        }

        public BigNum AdRevenuePerTick()
        {
            return model.Followers * MoneyPerFollowerPerTick;
        }

        public void Tick()
        {
            model.Add(Units.Money, AdRevenuePerTick());

            model.Add(Units.Follower, model.PublishedVideos * FollowersPerVideoPerTick);

            if (model.TickCount % TicksPerVideoEditor == 0 &&
                model.UpgradeManager.IsActive(Upgrade.EUpgradeType.HireVideoEditor))
            {
                DoMakeVideo();
            }
        }
    }
}
