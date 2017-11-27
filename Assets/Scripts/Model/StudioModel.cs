using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class StudioModel
    {
        public bool IsActive { get; set; }

        protected LootBoxModel model;

        public static BigNum DevHourPerDeveloperTick = 0.1f;
        public static BigNum CustomerDataPerDataAnalystTick = 0.1f;

        public StudioModel(LootBoxModel model)
        {
            this.model = model;
        }

        public void HireDeveloper(int num)
        {
            model.Add(Units.Developer, num);
            model.Add(Units.Click, 1);
        }

        public void FireDeveloper(int num)
        {
            model.ConsumeUpTo(Units.Developer, num);
            model.Add(Units.Click, 1);
        }

        public void HireDataAnalyst(int num)
        {
            model.Add(Units.DataAnalyst, num);
            model.Add(Units.Click, 1);
        }

        public void FireDataAnalyst(int num)
        {
            model.ConsumeUpTo(Units.DataAnalyst, num);
            model.Add(Units.Click, 1);
        }

        public BigNum GetCostPerDeveloperPerTick()
        {
            return 100;// * Mathf.Pow(1.7f, model.Developers);
        }

        public BigNum GetCostPerDataAnalystPerTick()
        {
            return 100;// * Mathf.Pow(1.7f, model.DataAnalysts);
        }

        public BigNum DevCostPerTick()
        {
            return model.Developers * GetCostPerDeveloperPerTick();
        }

        public BigNum DataAnalystCostPerTick()
        {
            return model.DataAnalysts * GetCostPerDataAnalystPerTick();
        }

        public BigNum HypePerRelease()
        {
            BigNum baseHype = 100000;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.MarketingCampaign))
            {
                baseHype *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.LootBoxPreOrders))
            {
                baseHype *= 2;
            }

            return baseHype * Mathf.Log10(CostOfGameInDevHours());
        }

        public BigNum ActivePlayersDecayPerTick()
        {
            BigNum decay = model.ActivePlayers * 0.02f / 30f;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AddWeeklyRewards))
            {
                decay /= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AddDailyRewards))
            {
                decay /= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AddConstantRewards))
            {
                decay = 0;
            }

            return decay;
        }

        public BigNum PercentOfPlayersWhoMonetize()
        {
            if (!model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.EnableMicrotransactions))
            {
                return 0;
            }

            BigNum percent = 0.01f;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AddGoldBoxes))       { percent *= 2; }
            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AddSeasonalBoxes))   { percent *= 2; }
            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AddLuteBoxes))       { percent *= 2; }
            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AddSkinnerBoxes))    { percent *= 2; }
            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.BuffsInBoxes))       { percent *= 2; }
            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.CoreGameInBoxes))    { percent *= 2; }
            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.WholeGameInBoxes))   { percent = 1; }

            return percent;
        }

        public BigNum MicrotransactionsPerTick()
        {
            return model.ActivePlayers * PercentOfPlayersWhoMonetize() / 30f;
        }

        public BigNum RevenuePerMicrotransaction()
        {
            BigNum amount = 1.99f;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AddGems))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AddGoldCoins))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AddCrystals))
            {
                amount *= 2;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AddCards))
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

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.EliminateUnderperformingFranchises))
            {
                pow = 1.1f;
            }

            return 500 * Mathf.Pow(pow, model.ReleasedGames);
        }

        public BigNum RevenuePerUnitSold()
        {
            BigNum amount = 2;

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.ChargeMore))
            {
                amount *= 1.25f;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.ChargeEvenMore))
            {
                amount *= 1.25f;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.SellLimitedEditions))
            {
                amount *= 1.25f;
            }

            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.SellCollectorEditions))
            {
                amount *= 1.25f;
            }

            if (!model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.StartDistributionService))
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

                BigNum unitsSold = HypePerRelease();
                BigNum revenue = unitsSold * RevenuePerUnitSold();
                model.Add(Units.CopySold, unitsSold);
                model.Add(Units.Money, revenue);

                //prev: model.Add(Units.ActivePlayer, unitsSold);
                model.Resources[Units.ActivePlayer].Amount = unitsSold;

                Logger.Log(string.Format("Game {0} released and sold {1} copies for ${2} in profit.", model.ReleasedGames, unitsSold, revenue));

                if (model.ReleasedGames == 5)
                {
                    Logger.Log("These games are getting expensive to make.");
                }
            }

            model.Add(Units.Click, 1);
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
            if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.EnableMicrotransactions))
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
