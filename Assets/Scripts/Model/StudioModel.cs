using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class StudioModel
    {
        public bool IsActive { get; set; }

        protected LootBoxModel model;

        public BigNum CostPerDeveloperPerTick = 1;
        public BigNum GameProgressPerDeveloperPerTick = 0.1f;

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

        public BigNum HypePerRelease()
        {
            return 100;
        }

        public BigNum HypeDecayPerTick()
        {
            return 0.1f;
        }

        public void Tick()
        {
            //Game production
            if (model.Consume(Units.Money, DevCostPerTick()))
            {
                model.Add(Units.GameProgress, model.Developers * GameProgressPerDeveloperPerTick);

                if (model.Consume(Units.GameProgress, 100))
                {
                    model.Add(Units.ReleasedGame, 1);
                    model.Add(Units.Hype, HypePerRelease());
                }
            }
            else
            {
                //Not enough money to pay everybody!
                //Drain the bank account but don't make any progress on the game
                model.Consume(Units.Money, model.Money);
            }

            //Unit Sales
            if (model.Hype >= 1)
            {
                model.Add(Units.CopySold, model.Hype);
            }

            //Hype Decay
            model.Consume(Units.Hype, HypeDecayPerTick());
        }
    }
}
