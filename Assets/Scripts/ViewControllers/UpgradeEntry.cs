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

        public void Awake()
        {
            Button.onClick.AddListener(HandleClick);
        }

        public void SetData(Upgrade upgrade)
        {
            this.name = "upgrade_" + upgrade.Name;
            Name.text = upgrade.Name;
            Description.text = upgrade.Description;

            string costStr = string.Empty;

            foreach(Resource cost in upgrade.Costs)
            {
                costStr += cost.ToString() + "  ";
            }

            Cost.text = costStr;
        }

        private void HandleClick()
        {
            if (OnClick != null)
            {
                OnClick();
            }
            LootBoxModel.Instance.Add(Units.Click, 1);
        }
    }
}
