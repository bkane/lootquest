using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class UpgradeManager
    {
        public LootBoxModel Model;
        public List<Upgrade> Upgrades;

        public UpgradeManager(LootBoxModel model)
        {
            this.Model = model;
            Upgrades = new List<Upgrade>();
            Upgrades.Add(new Upgrade()
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
            upgrade.State = Upgrade.EState.Purchased;

            switch(upgrade.Type)
            {
                case Upgrade.EUpgradeType.EnergyDrinks:
                    {
                        Debug.Log("Energy drink upgrade purchased");
                    }
                    break;
            }
        }
    }
}
