using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Model
{
    /// <summary>
    /// The game-with-in-a-game
    /// </summary>
    public class MacGuffinQuest
    {
        public LootBoxModel Model;

        public MacGuffinQuest(LootBoxModel model)
        {
            this.Model = model;
        }

        public void BuyLootBox()
        {
            if (Model.Consume(Units.Money, 10))
            {
                Model.Add(Units.LootBox, 1);
            }
        }
    }
}
