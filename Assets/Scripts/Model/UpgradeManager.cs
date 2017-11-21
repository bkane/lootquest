using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class UpgradeManager
    {
        public LootBoxModel Model;
        public Dictionary<Upgrade.EUpgradeType, Upgrade> Upgrades;

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

            #endregion


            #region Job upgrades

            Upgrades.Add(Upgrade.EUpgradeType.JobAutomationScript, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.JobAutomationScript,
                Name = "Write Script to Automate Job",
                Description = "It's going to happen anyway. Might as well do it myself while the paycheck still goes to me.",
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
                    new Resource(Units.Money, 10)
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

            switch(upgrade.Type)
            {
                case Upgrade.EUpgradeType.PurchaseMacGuffinQuest:
                    {
                        Model.MacGuffinQuest.IsActive = true;
                    }
                    break;
                case Upgrade.EUpgradeType.UnlockInfluencerCareer:
                    {
                        Model.Influencer.IsActive = true;
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

            for (int i = 0; i < upgrade.UnlockThreshold.Count; i++)
            {
                Resource resource = upgrade.UnlockThreshold[i];

                if (Model.Resources[resource.Type].Amount < resource.Amount)
                {
                    unlockThresholdMet = false;
                    break;
                }
            }

            //TODO: also implement pre-req upgrades

            return unlockThresholdMet;
        }
    }
}
