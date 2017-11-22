using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class StudioModel
    {
        public bool IsActive { get; set; }

        protected LootBoxModel model;

        public BigNum CostPerDeveloperPerTick = 1;
        public BigNum GameProgressPerDeveloperPerTick = 1;

        public StudioModel(LootBoxModel model)
        {
            this.model = model;
        }

        public void HireDeveloper()
        {
            if (model.Consume(Units.Money, 10))
            {
                model.Add(Units.Developer, 1);
            }
        }

        public void FireDeveloper()
        {
            model.Consume(Units.Developer, 1);
        }

        public BigNum DevCostPerTick()
        {
            return model.Developers * CostPerDeveloperPerTick;
        }

        public void Tick()
        {
            if (model.Consume(Units.Money, DevCostPerTick()))
            {
                model.Add(Units.GameProgress, model.Developers * GameProgressPerDeveloperPerTick);

                if (model.Consume(Units.GameProgress, 100))
                {
                    model.Add(Units.ReleasedGame, 1);
                }
            }
            else
            {
                //Not enough money to pay everybody!
                //Drain the bank account but don't make any progress on the game
                model.Consume(Units.Money, model.Money);
            }
        }
    }
}
