namespace Assets.Scripts.Model
{
    /// <summary>
    /// The game-with-in-a-game
    /// </summary>
    public class MacGuffinQuest
    {
        public LootBoxModel Model;

        public int MoneyPerLootBox = 5;
        public int GrindProgressPerLootBox = 10;

        public MacGuffinQuest(LootBoxModel model)
        {
            this.Model = model;
        }

        public void DoGrind(BigNum amount)
        {
            Model.Add(Units.GrindProgress, amount);

            if (Model.Consume(Units.GrindProgress, GrindProgressPerLootBox))
            {
                Model.Add(Units.LootBox, 1);
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
                Model.Add(Units.TrashItems, 1);
            }
        }

        public void Tick()
        {
            if (Model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AutoGrinder))
            {
                DoGrind(0.1f);
            }
        }
    }
}
