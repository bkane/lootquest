using Assets.Scripts.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ViewControllers
{
    public class UpgradeViewController : MonoBehaviour
    {
        public UpgradeEntry UpgradeEntryPrefab;
        public GameObject LayoutParent;

        protected Dictionary<Upgrade.EUpgradeType, UpgradeEntry> entries;

        private void Awake()
        {
            entries = new Dictionary<Upgrade.EUpgradeType, UpgradeEntry>();
        }

        private void Update()
        {
            foreach(var kvp in LootBoxModel.Instance.UpgradeManager.UpgradeStates)
            {
                Upgrade.EUpgradeType type = kvp.Key;
                Upgrade upgrade = LootBoxModel.Instance.UpgradeManager.Upgrades[type];
                Upgrade.EState state = kvp.Value;

                switch(state)
                {
                    case Upgrade.EState.Hidden:
                        {
                            if (entries.ContainsKey(type))
                            {
                                entries[type].gameObject.SetActive(false);
                            }
                        }
                        break;
                    case Upgrade.EState.Visible:
                        {
                            //Add the entry if necessary
                            if (!entries.ContainsKey(type))
                            {
                                UpgradeEntry entry = Instantiate(UpgradeEntryPrefab, LayoutParent.transform);
                                entry.SetData(upgrade);
                                entry.OnClick += () => { LootBoxModel.Instance.UpgradeManager.PurchaseUpgrade(upgrade); };

                                entries.Add(type, entry);
                            }

                            //Make sure it's enabled
                            entries[type].gameObject.SetActive(true);

                            //TODO: set its state based on whether it can be afforded
                        }
                        break;
                    case Upgrade.EState.Purchased:
                        {
                            if (entries.ContainsKey(type))
                            {
                                //For now, also just disable it. Could destroy but probably not worth being clean
                                entries[type].gameObject.SetActive(false);
                            }
                        }
                        break;
                }

                if ((state != Upgrade.EState.Hidden) && 
                    !entries.ContainsKey(type))
                {
                    UpgradeEntry entry = Instantiate(UpgradeEntryPrefab, LayoutParent.transform);
                    entry.SetData(upgrade);
                    entry.OnClick += () => { LootBoxModel.Instance.UpgradeManager.PurchaseUpgrade(upgrade); };

                    entries.Add(type, entry);
                }
            }
        }
    }
}
