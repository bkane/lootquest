using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ViewControllers
{
    public class UpgradeViewController : MonoBehaviour
    {
        public LootBoxModel Model;
        public UpgradeEntry UpgradeEntryPrefab;
        public GameObject LayoutParent;

        protected Dictionary<Upgrade, UpgradeEntry> entries;

        private void Awake()
        {
            entries = new Dictionary<Upgrade, UpgradeEntry>();
        }

        private void Update()
        {
            foreach(var kvp in Model.UpgradeManager.Upgrades)
            {
                Upgrade upgrade = kvp.Value;

                switch(upgrade.State)
                {
                    case Upgrade.EState.Hidden:
                        {
                            if (entries.ContainsKey(upgrade))
                            {
                                entries[upgrade].gameObject.SetActive(false);
                            }
                        }
                        break;
                    case Upgrade.EState.Visible:
                        {
                            //Add the entry if necessary
                            if (!entries.ContainsKey(upgrade))
                            {
                                UpgradeEntry entry = Instantiate(UpgradeEntryPrefab, LayoutParent.transform);
                                entry.SetData(upgrade);
                                entry.OnClick += () => { Model.UpgradeManager.PurchaseUpgrade(upgrade); };

                                entries.Add(upgrade, entry);
                            }

                            //Make sure it's enabled
                            entries[upgrade].gameObject.SetActive(true);

                            //TODO: set its state based on whether it can be afforded
                        }
                        break;
                    case Upgrade.EState.Purchased:
                        {
                            if (entries.ContainsKey(upgrade))
                            {
                                //For now, also just disable it. Could destroy but probably not worth being clean
                                entries[upgrade].gameObject.SetActive(false);
                            }
                        }
                        break;
                }

                if ((upgrade.State != Upgrade.EState.Hidden) && 
                    !entries.ContainsKey(upgrade))
                {
                    UpgradeEntry entry = Instantiate(UpgradeEntryPrefab, LayoutParent.transform);
                    entry.SetData(upgrade);
                    entry.OnClick += () => { Model.UpgradeManager.PurchaseUpgrade(upgrade); };

                    entries.Add(upgrade, entry);
                }
            }
        }
    }
}
