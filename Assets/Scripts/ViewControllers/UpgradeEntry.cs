using Assets.Scripts.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class UpgradeEntry : MonoBehaviour
    {
        public Button Button;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Description;
        public TextMeshProUGUI Cost;

        public Action OnClick;

        protected Upgrade upgrade;

        public void Awake()
        {
            Button.onClick.AddListener(HandleClick);
            Button.interactable = false;
        }

        public void SetData(Upgrade upgrade)
        {
            this.upgrade = upgrade;
            this.name = "upgrade_" + upgrade.Name;
            Name.text = upgrade.Name;
            Description.text = upgrade.Description;

            string costStr = string.Empty;

            foreach(Resource cost in upgrade.Costs)
            {
                costStr += cost.ToString() + "  ";
            }

            Cost.text = costStr;
            RefreshState();
        }

        private void HandleClick()
        {
            if (OnClick != null)
            {
                OnClick();
            }
            LootBoxModel.Instance.Add(Units.Click, 1);
        }

        protected void RefreshState()
        {
            bool canAfford = LootBoxModel.Instance.UpgradeManager.CanAfford(upgrade.Type);

            if (Button.interactable != canAfford)
            {
                Button.interactable = canAfford;
            }
        }

        private void FixedUpdate()
        {
            RefreshState();
        }
    }
}
