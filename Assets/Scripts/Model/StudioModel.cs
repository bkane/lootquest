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
            BigNum baseHype = 100;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.MarketingCampaign))
            {
                baseHype *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.LootBoxPreOrders))
            {
                baseHype *= 2;
            }

            return baseHype * CostOfGameInDevHours();
        }

        public BigNum ActivePlayersDecayPerTick()
        {
            BigNum decay = model.ActivePlayers * 0.04f / 30f;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AddWeeklyRewards))
            {
                decay /= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AddDailyRewards))
            {
                decay /= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AddConstantRewards))
            {
                decay = 0;
            }

            return decay;
        }

        public BigNum PercentOfPlayersWhoMonetize()
        {
            if (!model.UpgradeManager.IsActive(Upgrade.EUpgradeType.EnableMicrotransactions))
            {
                return 0;
            }

            BigNum percent = 0.004f;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AddGoldBoxes))       { percent *= 2; }
            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AddSeasonalBoxes))   { percent *= 2; }
            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AddLuteBoxes))       { percent *= 2; }
            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AddSkinnerBoxes))    { percent *= 2; }
            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.BuffsInBoxes))       { percent *= 2; }
            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.CoreGameInBoxes))    { percent *= 2; }
            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.WholeGameInBoxes))   { percent *= 2; }

            return percent;
        }

        public BigNum MicrotransactionsPerTick()
        {
            return model.ActivePlayers * PercentOfPlayersWhoMonetize() / 30f;
        }

        public BigNum RevenuePerMicrotransaction()
        {
            BigNum amount = 0.25f;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AddGems))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AddGoldCoins))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AddCrystals))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AddCards))
            {
                amount *= 2;
            }

            return amount;
        }

        public BigNum MicrotransactionRevenuePerTick()
        {
            return MicrotransactionsPerTick() * RevenuePerMicrotransaction();
        }

        public BigNum CostOfGameInDevHours()
        {
            float pow = 1.25f;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.EliminateUnderperformingFranchises))
            {
                pow = 1.1f;
            }

            return 100 * Mathf.Pow(pow, model.ReleasedGames);
        }

        public BigNum RevenuePerUnitSold()
        {
            BigNum amount = 20;

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.ChargeMore))
            {
                amount += 20;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.ChargeEvenMore))
            {
                amount += 20;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.SellLimitedEditions))
            {
                amount += 20;
            }

            if (model.UpgradeManager.IsActive(Upgrade.EUpgradeType.SellCollectorEditions))
            {
                amount += 20;
            }

            if (!model.UpgradeManager.IsActive(Upgrade.EUpgradeType.StartDistributionService))
            {
                amount *= 0.7f;
            }

            return amount;
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

                Logger.Log(string.Format("Game {0} released and sold {1} copies for ${2} in revenue.", model.ReleasedGames, unitsSold, revenue));

                if (model.ReleasedGames == 5)
                {
                    Logger.Log("These games are getting expensive to make.");
                }
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
                Logger.Log(string.Format("Not enough money to pay everybody so {0} {1} quit!", model.Developers, model.Developers > 1 ? "developers" : "developer"));
                model.ConsumeExactly(Units.Developer, model.Developers); //Everybody quits!
                
                //Drain the bank account but don't make any progress on the game
                model.ConsumeExactly(Units.Money, model.Money);
            }

            //Customer Data production
            if (model.ConsumeExactly(Units.Money, DataAnalystCostPerTick()))
            {
                model.Add(Units.AnalyticsData, model.DataAnalysts * CustomerDataPerDataAnalystTick); //warning duplicated in Public
            }
            else
            {
                //Not enough money to pay everybody!
                Logger.Log(string.Format("Not enough money to pay everybody so {0} {1} quit!", model.DataAnalysts, model.DataAnalysts > 1 ? "data analysts" : "data analyst"));
                model.ConsumeExactly(Units.DataAnalyst, model.DataAnalysts); //Everybody quits!

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
