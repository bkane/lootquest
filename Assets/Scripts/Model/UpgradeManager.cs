using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class UpgradeManager
    {
        public LootBoxModel Model;
        public Dictionary<Upgrade.EUpgradeType, Upgrade> Upgrades;

        private BigNum MCGCost = 30;

        public UpgradeManager(LootBoxModel model)
        {
            this.Model = model;
            Upgrades = new Dictionary<Upgrade.EUpgradeType, Upgrade>();



            #region Life upgrades
            
            Upgrades.Add(Upgrade.EUpgradeType.EnergyDrinks, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.EnergyDrinks,
                Name = "Switch to Energy Drinks",
                Description = "Ditch coffee and go straight for the nectar of the gods.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.LearnToCode, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.LearnToCode,
                Name = "Learn to code",
                Description = "Looks like computers might be sticking around for a while. I should learn to talk to them.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.TotalMoneyEarned, 100)
                }
            });

            #endregion


            #region Job upgrades

            Upgrades.Add(Upgrade.EUpgradeType.WorkSmarter, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.WorkSmarter,
                Name = "Buy a book on life hacks",
                Description = "Work smarter not harder! Get more work done with every click.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.JobCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.DressForSuccess, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.DressForSuccess,
                Name = "Dress for Success",
                Description = "Dress for the job you want, and then get that job. Promotion means more money!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.JobCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.JobAutomationScript, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.JobAutomationScript,
                Name = "Write Script to Automate Job",
                Description = "It's going to happen anyway. Might as well do it myself while the paycheck still goes to me.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.LearnToCode }
            });

            Upgrades.Add(Upgrade.EUpgradeType.SecondJob, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.SecondJob,
                Name = "Get a second job",
                Description = "I work from home anyway. Who's going to notice?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.JobCompleted, 3)
                }
            });

            #endregion


            #region MacGuffinQuest upgrades

            Upgrades.Add(Upgrade.EUpgradeType.PurchaseMacGuffinQuest, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.PurchaseMacGuffinQuest,
                Name = "Pre-Order MacGuffin Quest",
                Description = "Holy crap I can't wait.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, MCGCost)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.PurchaseMacGuffinQuestLimitedEdition, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.PurchaseMacGuffinQuestLimitedEdition,
                Name = "Buy MacGuffin Quest Deluxe Limited Edition",
                Description = "Well it's more expensive but at least it's in stock.",
                CommentOnBuy = "YESSSSS! Finally! Time to play MacGuffin Quest 2!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 80)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.PurchaseMacGuffinQuest }
            });

            Upgrades.Add(Upgrade.EUpgradeType.RemoveGameAnimations, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.RemoveGameAnimations,
                Name = "Hack MacGuffin Quest and remove all animations",
                Description = "Use your newfound computer talents to remove those pesky animations slowing things down. Improves grinding efficiency.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.GrindCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.SecondMouse, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.SecondMouse,
                Name = "Buy a second mouse",
                Description = "If I use both hands, I can grind twice as fast! I mean, have twice as much fun.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.GrindCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.TieFiveMiceTogether, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.TieFiveMiceTogether,
                Name = "Tape Five Mice Together",
                Description = "I read about how, with a few minor adjustments, I can turn a regular mouse into five mice!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.GrindCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AutoGrinder, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AutoGrinder,
                Name = "Write Auto-Grind Script",
                Description = "Use your newfound scripting talents to have your computer play MacGuffin Quest for you! Automatic fun!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.GrindCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AutoSellTrashItems, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AutoSellTrashItems,
                Name = "Write Auto-Auction Script",
                Description = "Selling items on the auction house feels like work and 'round here? We automate work. Gets the job done but doesn't get as good a return.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.TrashItemSold, 3)
                }
            });


            #endregion






            #region Influencer Upgrades

            Upgrades.Add(Upgrade.EUpgradeType.UnlockInfluencerCareer, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.UnlockInfluencerCareer,
                Name = "Buy a Capture Card",
                Description = "Hey I bet people would watch me opening all these loot boxes.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.LootBoxOpened, 5)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.HireVideoEditor, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.HireVideoEditor,
                Name = "Hire Video Editor",
                Description = "Hire an editor to convert your raw footage into compelling content. Your editor assures you they're worth every penny, given what they have to work with.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 1)
                }
            });

            #endregion


            #region Studio Upgrades

            Upgrades.Add(Upgrade.EUpgradeType.StartAStudio, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.StartAStudio,
                Name = "Start a studio",
                Description = "You know, I could probably do this myself.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Follower, 10)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.EnableMicrotransactions, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.EnableMicrotransactions,
                Name = "Add Microtransactions",
                Description = "Just for cosmetic items.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ReleasedGame, 1)
                }
            });
            #endregion


            #region Public Upgrades

            Upgrades.Add(Upgrade.EUpgradeType.ExecuteIPO, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ExecuteIPO,
                Name = "Execute IPO",
                Description = "Time to take this to the next level.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.PurchaseMacGuffinQuestSourceCode, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.PurchaseMacGuffinQuestSourceCode,
                Name = "Purchase MacGuffin Quest source code",
                Description = "I just need to flip a zero to a one and then I can finally play as MacGuffin.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Customer, 7.6f)
                }
            });

            #endregion
        }

        public void Unlock(Upgrade.EUpgradeType type)
        {
            if (Upgrades[type].State == Upgrade.EState.Hidden)
            {
                Upgrades[type].State = Upgrade.EState.Visible;
                Debug.LogFormat("Upgrade {0} now available", Upgrades[type].Name);
            }
        }

        public bool IsActive(Upgrade.EUpgradeType type)
        {
            return Upgrades[type].State == Upgrade.EState.Purchased;
        }

        public bool PurchaseUpgrade(Upgrade upgrade)
        {
            if (upgrade.State == Upgrade.EState.Visible)
            {
                if (Model.Consume(upgrade.Costs))
                {
                    DoUpgrade(upgrade);
                    return true;
                }
            }
            else
            {
                Debug.LogErrorFormat("Tried to purchase upgrade {0} but state is {1}", upgrade.Name, upgrade.State);
            }

            return false;
        }

        protected void DoUpgrade(Upgrade upgrade)
        {
            Debug.LogFormat("Activated upgrade: {0}", upgrade.Name);
            upgrade.State = Upgrade.EState.Purchased;

            if (!string.IsNullOrEmpty(upgrade.CommentOnBuy))
            {
                Logger.Log(upgrade.CommentOnBuy);
            }

            switch(upgrade.Type)
            {
                case Upgrade.EUpgradeType.PurchaseMacGuffinQuest:
                    {
                        Logger.Log("Sorry! MacGuffin Quest Regular Edition pre-orders are all sold out! We do still have Limited Editions available ironically.");

                        Logger.Log("How is a pre-order out of stock?");

                        Model.Add(Units.Money, MCGCost); //money back, since it wasn't actually purchased
                    }
                    break;
                case Upgrade.EUpgradeType.PurchaseMacGuffinQuestLimitedEdition:
                    {
                        Model.MacGuffinQuest.IsActive = true;
                    }
                    break;
                case Upgrade.EUpgradeType.UnlockInfluencerCareer:
                    {
                        Model.Influencer.IsActive = true;
                    }
                    break;

                case Upgrade.EUpgradeType.StartAStudio:
                    {
                        Model.Job.IsActive = false;
                        Model.Studio.IsActive = true;
                    }
                    break;
                case Upgrade.EUpgradeType.ExecuteIPO:
                    {
                        Model.Influencer.IsActive = false;
                        Model.Studio.IsActive = false;
                        Model.Public.IsActive = true;
                    }
                    break;
                case Upgrade.EUpgradeType.AutoGrinder:
                    {
                        Model.Add(Units.BotAccount, 1);
                    }
                    break;
                case Upgrade.EUpgradeType.PurchaseMacGuffinQuestSourceCode:
                    {
                        Debug.Log("Game over!");
                    }
                    break;
            }
        }

        public void Tick()
        {
            foreach(var kvp in Upgrades)
            {
                if (kvp.Value.State == Upgrade.EState.Hidden)
                {
                    if (IsUnlockThresholdMet(kvp.Value))
                    {
                        Unlock(kvp.Value.Type);
                    }
                }
            }
        }

        protected bool IsUnlockThresholdMet(Upgrade upgrade)
        {
            bool unlockThresholdMet = true;

            if (unlockThresholdMet)
            {
                //Check pre-req upgrades
                for (int i = 0; i < upgrade.Requirements.Count; i++)
                {
                    if (!IsActive(upgrade.Requirements[i]))
                    {
                        unlockThresholdMet = false;
                        break;
                    }
                }
            }

            if (unlockThresholdMet)
            {
                //Check resources threshold
                for (int i = 0; i < upgrade.UnlockThreshold.Count; i++)
                {
                    Resource resource = upgrade.UnlockThreshold[i];

                    if (Model.Resources[resource.Type].Amount < resource.Amount)
                    {
                        unlockThresholdMet = false;
                        break;
                    }
                }
            }

            return unlockThresholdMet;
        }
    }
}
