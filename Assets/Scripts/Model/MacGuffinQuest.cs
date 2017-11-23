﻿namespace Assets.Scripts.Model
{
    /// <summary>
    /// The game-with-in-a-game
    /// </summary>
    public class MacGuffinQuest
    {
        public bool IsActive { get; set; }

        public LootBoxModel Model;

        public int MoneyPerLootBox = 5;
        public int GrindProgressPerLootBox = 100;

        //Selling TrashItems
        public int AutoSellTicks = 60;
        public BigNum ManualSellTrashEfficiency = 1;
        public BigNum AutoSellTrashEfficiency = 0.5f;

        public MacGuffinQuest(LootBoxModel model)
        {
            this.Model = model;
        }

        public void DoGrindClick()
        {
            BigNum amount = 5;

            if (Model.UpgradeManager.IsActive(Upgrade.EUpgradeType.SecondMouse))
            { 
                amount += 5;
            }

            if (Model.UpgradeManager.IsActive(Upgrade.EUpgradeType.TieFiveMiceTogether))
            {
                amount *= 5;
            }

            DoGrind(amount);
        }

        public void DoGrind(BigNum amount)
        {
            if (Model.UpgradeManager.IsActive(Upgrade.EUpgradeType.RemoveGameAnimations))
            {
                amount *= 2;
            }

            Model.Add(Units.GrindProgress, amount);

            if (Model.Consume(Units.GrindProgress, GrindProgressPerLootBox))
            {
                Model.Add(Units.LootBox, 1);
                Model.Add(Units.GrindCompleted, 1);
            }
        }

        public void BuyLootBox()
        {
            if (Model.Consume(Units.Money, MoneyPerLootBox))
            {
                Model.Add(Units.LootBox, 1);
            }
        }

        public void OpenLootBox()
        {
            if (Model.Consume(Units.LootBox, 1))
            {
                //TODO: this is opening a lootbox! This will be exciting!
                Model.Add(Units.TrashItem, 1);
                Model.Add(Units.LootBoxOpened, 1);

                if (Model.Influencer.IsActive)
                {
                    Model.Add(Units.VideoContent, 1);
                }
            }
        }

        public void SellTrash(bool isManualAction)
        {
            if (Model.Consume(Units.TrashItem, 1))
            {
                BigNum salePrice = 1;

                if (isManualAction)
                {
                    salePrice *= ManualSellTrashEfficiency;
                }
                else
                {
                    salePrice *= AutoSellTrashEfficiency;
                }

                Model.Add(Units.Money, salePrice);
                Model.Add(Units.TrashItemSold, 1);
            }
        }

        public void BuyBotAccount()
        {
            if (Model.Consume(Units.Money, 10))
            {
                Model.Add(Units.BotAccount, 1);
            }
        }

        public void Tick()
        {
            if (!IsActive) { return; }

            if (Model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AutoGrinder))
            {
                DoGrind(Model.NumBotAccounts);
            }

            if (Model.TickCount % AutoSellTicks == 0 &&
                Model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AutoSellTrashItems))
            {
                SellTrash(false);
            }
        }
    }
}
