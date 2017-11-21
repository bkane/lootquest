using UnityEngine;

namespace Assets.Scripts.Model
{
    public class InfluencerModel
    {
        public bool IsActive { get; set; }

        public BigNum VideoContentPerVideo = 50;
        public BigNum MoneyPerFollowerPerVideoPerTick = 0.001f;

        protected LootBoxModel model;

        public InfluencerModel(LootBoxModel model)
        {
            this.model = model;
        }

        public void DoMakeVideo()
        {
            if (model.Consume(Units.VideoContent, VideoContentPerVideo))
            {
                model.Add(Units.Video, 1);
                model.Add(Units.Follower, Mathf.Max(1, model.Followers * 0.1f));
            }
        }

        public void Tick()
        {
            model.Add(Units.Money, model.Videos * model.Followers * MoneyPerFollowerPerVideoPerTick);
        }
    }
}
