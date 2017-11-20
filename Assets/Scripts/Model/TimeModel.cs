using System;

namespace Assets.Scripts.Model
{
    /// <summary>
    /// Time itself
    /// </summary>
    public class TimeModel
    {
        public LootBoxModel Model;

        protected DateTime startTime;
        protected float hoursPerTick = 1/30f; //initially zero
        protected float hoursSinceEpoch;

        public TimeModel(LootBoxModel model)
        {
            this.Model = model;
            startTime = DateTime.Now;
        }

        public void Tick(int tickCount)
        {
            hoursSinceEpoch += (hoursPerTick * tickCount);
        }

        public string GetTimeString()
        {
            DateTime time = startTime.AddHours(hoursSinceEpoch);
            return time.ToLocalTime().ToString();
        }
    }
}
