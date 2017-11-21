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
            Upgrades.Add(Upgrade.EUpgradeType.EnergyDrinks, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.EnergyDrinks,
                Name = "Switch to Energy Drinks",
                Description = "Ditch coffee and go straight for the nectar of the gods.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                State = Upgrade.EState.Visible
            });

            Upgrades.Add(Upgrade.EUpgradeType.SleepApp, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.SleepApp,
                Name = "Buy Sleep App",
                Description = "Never miss out on sleep again with this sleep scheduler!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                State = Upgrade.EState.Visible
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
                State = Upgrade.EState.Visible
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
                State = Upgrade.EState.Visible
            });
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
                case Upgrade.EUpgradeType.EnergyDrinks:
                    {
                        //do something
                    }
                    break;
            }
        }
    }
}
