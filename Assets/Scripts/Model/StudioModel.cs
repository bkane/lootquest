using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class StudioModel
    {
        public bool IsActive { get; set; }

        protected LootBoxModel model;

        public BigNum CostPerDeveloperPerTick = 1;
        public static BigNum DevHourPerDeveloperTick = 0.1f;

        public BigNum CostPerDataAnalystPerTick = 1;
        public static BigNum CustomerDataPerDataAnalystTick = 0.1f;

        public StudioModel(LootBoxModel model)
        {
            this.model = model;
        }

        public void HireDeveloper()
        {
            if (model.ConsumeExactly(Units.Money, 10))
            {
                model.Add(Units.Developer, 1);
            }
        }

        public void FireDeveloper()
        {
            model.ConsumeExactly(Units.Developer, 1);
        }

        public void HireDataAnalyst()
        {
            if (model.ConsumeExactly(Units.Money, 10))
            {
                model.Add(Units.DataAnalyst, 1);
            }
        }

        public void FireDataAnalyst()
        {
            model.ConsumeExactly(Units.DataAnalyst, 1);
        }

        public BigNum DevCostPerTick()
        {
            return model.Developers * CostPerDeveloperPerTick;
        }

        public BigNum DataAnalystCostPerTick()
        {
            return model.DataAnalysts * CostPerDataAnalystPerTick;
        }

        public BigNum HypePerRelease()
        {
            return 100;
        }

        public BigNum ActivePlayersDecayPerTick()
        {
            return model.ActivePlayers * 0.005f / 30f;
        }

        public BigNum PercentOfPlayersWhoMonetize()
        {
            return 0.01f;
        }

        public BigNum MicrotransactionsPerTick()
        {
            return model.ActivePlayers * PercentOfPlayersWhoMonetize() / 30f;
        }

        public BigNum RevenuePerMicrotransaction()
        {
            return 0.25f;
        }

        public BigNum MicrotransactionRevenuePerTick()
        {
            return MicrotransactionsPerTick() * RevenuePerMicrotransaction();
        }

        public BigNum CostOfGameInDevHours()
        {
            return 100;
        }

        public BigNum RevenuePerUnitSold()
        {
            return 50;
        }

        public void ReleaseGame()
        {
            if (model.ConsumeExactly(Units.DevHour, CostOfGameInDevHours()))
            {
                model.Add(Units.ReleasedGame, 1);
                model.Add(Units.Hype, HypePerRelease());

                BigNum unitsSold = model.Hype;
                BigNum revenue = unitsSold * RevenuePerUnitSold();
                model.Add(Units.CopySold, unitsSold);
                model.Add(Units.ActivePlayer, unitsSold);
                model.Add(Units.Money, revenue);

                Logger.Log(string.Format("Game {0} released and sold {1} copies for ${2}", model.ReleasedGames, unitsSold, revenue));
            }
        }

        public void Tick()
        {
            if (!IsActive) { return; }

            //Game production
            if (model.ConsumeExactly(Units.Money, DevCostPerTick()))
            {
                model.Add(Units.DevHour, model.Developers * DevHourPerDeveloperTick); //warning duplicated in Public
            }
            else
            {
                //Not enough money to pay everybody!
                //Drain the bank account but don't make any progress on the game
                model.ConsumeExactly(Units.Money, model.Money);
            }

            //Customer Data production
            if (model.ConsumeExactly(Units.Money, DataAnalystCostPerTick()))
            {
                model.Add(Units.CustomerData, model.DataAnalysts * CustomerDataPerDataAnalystTick); //warning duplicated in Public
            }
            else
            {
                //Not enough money to pay everybody!
                //Drain the bank account but don't accrue any customer data
                model.ConsumeExactly(Units.Money, model.Money);
            }

            //Microtransactions
            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.EnableMicrotransactions))
            {
                model.Add(Units.Microtransaction, MicrotransactionsPerTick());
                model.Add(Units.Money, MicrotransactionRevenuePerTick());
            }

            //Playerbase decay
            if (model.ActivePlayers > 0)
            {
                model.ConsumeExactly(Units.ActivePlayer, ActivePlayersDecayPerTick());
            }
        }
    }
}
