namespace Assets.Scripts.Model
{
    /// <summary>
    /// The game-with-in-a-game
    /// </summary>
    public class MacGuffinQuest
    {
        public bool IsActive { get; set; }

        public LootBoxModel Model;

        private int MoneyPerLootBox = 5;
        private int GrindProgressPerLootBox = 100;

        //Selling TrashItems
        private int AutoSellTicks = 60;

        public MacGuffinQuest(LootBoxModel model)
        {
            this.Model = model;
        }


        public void DoGrindClick()
        {
            DoGrind(ActionsPerClick() * 5);
        }

        public void OpenLootBoxClick()
        {
            OpenLootBox(ActionsPerClick());
        }

        public void SellTrashClick()
        {
            SellTrash(ActionsPerClick());
        }

        public BigNum ActionsPerClick()
        {
            BigNum amount = 1;

            if (Model.UpgradeManager.IsActive(Upgrade.EUpgradeType.SecondMouse))
            {
                amount += 1;
            }

            if (Model.UpgradeManager.IsActive(Upgrade.EUpgradeType.TieFiveMiceTogether))
            {
                amount *= 5;
            }

            return amount;
        }



        public void DoGrind(BigNum amount)
        {
            if (Model.UpgradeManager.IsActive(Upgrade.EUpgradeType.RemoveGameAnimations))
            {
                amount *= 2;
            }

            Model.Add(Units.GrindProgress, amount);

            //TODO: this will cap at 30/s, but we should switch after that point anyway
            if (Model.ConsumeExactly(Units.GrindProgress, GrindProgressPerLootBox))
            {
                Model.Add(Units.LootBox, 1);
                Model.Add(Units.GrindCompleted, 1);
            }
        }

        public void BuyLootBox()
        {
            if (Model.ConsumeExactly(Units.Money, MoneyPerLootBox))
            {
                Model.Add(Units.LootBox, 1);
            }
        }

        public void OpenLootBox(BigNum amount)
        {
            BigNum lootBoxesOpened = Model.ConsumeUpTo(Units.LootBox, amount);

            if (lootBoxesOpened > 0)
            {
                //TODO: this is opening a lootbox! This will be exciting!
                Model.Add(Units.TrashItem, lootBoxesOpened);
                Model.Add(Units.LootBoxOpened, lootBoxesOpened);

                if (Model.Influencer.IsActive)
                {
                    Model.Add(Units.VideoContent, lootBoxesOpened);
                }
            }
        }

        public void SellTrash(BigNum amount)
        {
            BigNum itemsSold = Model.ConsumeUpTo(Units.TrashItem, amount);
            if (itemsSold > 0)
            {
                BigNum salePrice = itemsSold * 1;

                Model.Add(Units.Money, salePrice);
                Model.Add(Units.TrashItemSold, itemsSold);
            }
        }

        public void BuyBotAccount()
        {
            if (Model.ConsumeExactly(Units.Money, 10))
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
                SellTrash(Model.NumBotAccounts);
            }
        }
    }
}
