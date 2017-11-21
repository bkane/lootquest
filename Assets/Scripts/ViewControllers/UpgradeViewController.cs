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

        private void Start()
        {
            RefreshEntries();
        }

        private void RefreshEntries()
        {
            for(int i = 0; i < Model.UpgradeManager.Upgrades.Count; i++)
            {
                Upgrade upgrade = Model.UpgradeManager.Upgrades[i];

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
