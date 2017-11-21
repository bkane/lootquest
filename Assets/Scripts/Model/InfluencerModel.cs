using UnityEngine;

namespace Assets.Scripts.Model
{
    public class InfluencerModel
    {
        public bool IsActive { get; set; }

        public BigNum VideoProgressPerMakeVideoClick = 2;
        public BigNum MoneyPerFollowerPerVideoPerTick = 0.001f;

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
                    model.Add(Units.Follower, Mathf.Max(1, model.Followers * 0.1f));
                }
            }
        }

        public void Tick()
        {
            model.Add(Units.Money, model.PublishedVideos * model.Followers * MoneyPerFollowerPerVideoPerTick);
        }
    }
}
