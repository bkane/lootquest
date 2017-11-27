using UnityEngine;

namespace Assets.Scripts.Model
{
    /// <summary>
    /// The game-with-in-a-game
    /// </summary>
    public class MacGuffinQuest
    {
        public bool IsActive { get; set; }

        public LootBoxModel Model;

        private int GrindProgressPerLootBox = 100;
        private int TicksPerSell = 10;
        private int TicksPerOpen = 10;

        public bool HideBotButton;
        public int NumPlays;
        public bool GrindStart;

        public bool EndGameStart;
        public bool EndGameFinished;

        public MacGuffinQuest(LootBoxModel model)
        {
            this.Model = model;
        }

        public static void DoGrindClick()
        {
            LootBoxModel.Instance.MacGuffinQuest.DoGrind(LootBoxModel.Instance.MacGuffinQuest.ActionsPerClick() * 5);
            LootBoxModel.Instance.Add(Units.Click, 1);
        }

        public static void OpenLootBoxClick()
        {
            LootBoxModel.Instance.MacGuffinQuest.OpenLootBox(LootBoxModel.Instance.MacGuffinQuest.ActionsPerClick());
            LootBoxModel.Instance.Add(Units.Click, 1);
        }

        public static void SellTrashClick()
        {
            LootBoxModel.Instance.MacGuffinQuest.SellTrash(LootBoxModel.Instance.MacGuffinQuest.ActionsPerClick());
            LootBoxModel.Instance.Add(Units.Click, 1);
        }

        public static void BuyBotAccountClick()
        {
            if (LootBoxModel.Instance.NumBotAccounts < LootBoxModel.Instance.Resources[Units.BotAccount].MaxValue)
            {
                if (LootBoxModel.Instance.ConsumeExactly(Units.Money, LootBoxModel.Instance.MacGuffinQuest.CostPerBot()))
                {
                    LootBoxModel.Instance.Add(Units.BotAccount, 1);
                    LootBoxModel.Instance.Add(Units.Click, 1);
                }
            }
            else
            {
                Logger.Log("The <i>MacGuffin Quest 2</i> developers have gotten wise and are banning any additional bot accounts.");
                LootBoxModel.Instance.MacGuffinQuest.HideBotButton = true;
            }
        }

        public static void BuyLootBoxClick()
        {
            Logger.Log("I refuse to sacrifice my sense of accomplishment by taking the easy way out.");
            LootBoxModel.Instance.Add(Units.Click, 1);
        }


        protected void OnPlayComplete()
        {
            string[] messages = new string[]
            {
                "Hey wait, I don't get to play as MacGuffin??",
                "What the heck! This is <u>MacGuffin's</u> quest!",
                "I can only play as Bumblo, the bumbling sidekick?",
                "Bumblo can't even jump!",
                "\"Earn loot boxes for a chance to unlock and play as MacGuffin!\"",
                "Fair enough. I'm sure this won't take long, and it'll feel good to have earned it.... probably."
            };

            int index = NumPlays - 1;

            if (index >= 0 && index < messages.Length)
            {
                Logger.Log(messages[index]);
            }
            
            if (index >= messages.Length - 1)
            {
                GrindStart = true;
            }
        }

        protected void OnPlayCompleteWithMacGuffin()
        {
            string[] messages = new string[]
            {
                "...",
                "...",
                "... ... ...",
                "Eh, this is alright.",
                "...",
                "...",
                "Not as good as the first <i>MacGuffin Quest</i>.",
                "...",
                "Annnnd...",
                "Done.",
            };

            int index = NumPlays - 1;

            if (index >= 0 && index < messages.Length)
            {
                Logger.Log(messages[index]);
            }

            if (index >= messages.Length - 1)
            {
                if (!EndGameFinished)
                {
                    EndGameFinished = true;
                    Game.Instance.OnMacGuffinQuestFinished();
                }
            }
        }

        public BigNum ActionsPerClick()
        {
            if (EndGameStart)
            {
                return 1;
            }
            else
            {
                BigNum amount = 1;

                if (Model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.SecondMouse))
                {
                    amount += 1;
                }

                if (Model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.TieFiveMiceTogether))
                {
                    amount *= 5;
                }

                return amount;
            }
        }

        public void DoGrind(BigNum amount)
        {
            if (Model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.RemoveGameAnimations))
            {
                amount *= 2;
            }

            Model.Add(Units.GrindProgress, amount);

            //TODO: this will cap at 30/s, but we should switch after that point anyway
            if (Model.ConsumeExactly(Units.GrindProgress, GrindProgressPerLootBox))
            {
                if (EndGameStart)
                {
                    NumPlays++;
                    OnPlayCompleteWithMacGuffin();
                }
                else if (!GrindStart)
                {
                    NumPlays++;
                    OnPlayComplete();
                }
                else
                {
                    Model.Add(Units.LootBox, 1);
                    Model.Add(Units.GrindCompleted, 1);
                }
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
                BigNum salePrice = itemsSold * 5;

                Model.Add(Units.Money, salePrice);
                Model.Add(Units.TrashItemSold, itemsSold);
            }
        }

        public BigNum CostPerBot()
        {
            float pow = 1.7f;

            return 80 + 100 * Mathf.Pow(pow, Model.NumBotAccounts);
        }

        public void DoEndGame()
        {
            EndGameStart = true;
            NumPlays = 0;
            Model.Resources[Units.GrindProgress].Amount = 0;
        }

        public void Tick()
        {
            if (!IsActive) { return; }

            if (!EndGameStart)
            {
                if (Model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AutoGrinder))
                {
                    DoGrind(Model.NumBotAccounts);
                }

                if (Model.TickCount % TicksPerSell == 0 &&
                    Model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AutoSellTrashItems))
                {
                    SellTrash(Model.NumBotAccounts);
                }

                if (Model.TickCount % TicksPerOpen == 0 &&
                    Model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.AutoOpenBoxes))
                {
                    OpenLootBox(Model.NumBotAccounts);
                }
            }
        }
    }
}
