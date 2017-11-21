using System;

namespace Assets.Scripts.Model
{
    /// <summary>
    /// Time itself
    /// </summary>
    public class TimeModel
    {
        private LootBoxModel model;

        protected DateTime startTime;
        protected float hoursPerTick = 1/30f; //initially zero
        protected float hoursSinceEpoch;

        public TimeModel(LootBoxModel model)
        {
            this.model = model;
            startTime = DateTime.Now;
        }

        public void Tick()
        {
            hoursSinceEpoch += hoursPerTick;
        }

        public string GetTimeString()
        {
            DateTime time = startTime.AddHours(hoursSinceEpoch);
            return time.ToLocalTime().ToString();
        }
    }
}
