﻿using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class MacGuffinQuestViewController : MonoBehaviour
    {
        //Labels
        public Slider GrindProgress;
        public TextMeshProUGUI GrindButtonText;
        //public TextMeshProUGUI MacGuffinsUnlockedText;
        public TextMeshProUGUI LootBoxText;
        public TextMeshProUGUI ItemsText;
        public TextMeshProUGUI BotAccountsText;

        public GameObject LootBoxPanel;
        public GameObject SellItemsPanel;
        public GameObject BotsPanel;

        //Buttons
        public Button GrindButton;
        public Button SellTrashItemButton;
        public Button BuyLootBoxButton;
        public Button OpenLootBoxButton;
        public Button BuyBotAccountButton;


        private void Awake()
        {
            GrindButton.onClick.AddListener(LootBoxModel.Instance.MacGuffinQuest.DoGrindClick);
            SellTrashItemButton.onClick.AddListener(LootBoxModel.Instance.MacGuffinQuest.SellTrashClick);
            BuyLootBoxButton.onClick.AddListener(LootBoxModel.Instance.MacGuffinQuest.BuyLootBoxClick);
            OpenLootBoxButton.onClick.AddListener(LootBoxModel.Instance.MacGuffinQuest.OpenLootBoxClick);
            BuyBotAccountButton.onClick.AddListener(LootBoxModel.Instance.MacGuffinQuest.BuyBotAccountClick);
        }

        private void Update()
        {
            GrindProgress.value = LootBoxModel.Instance.GrindProgress / 100f;

            LootBoxText.text = string.Format("{0}", LootBoxModel.Instance.LootBoxes);
            ItemsText.text = string.Format("{0}", LootBoxModel.Instance.TrashItems);
            BotAccountsText.text = string.Format("{0}", LootBoxModel.Instance.NumBotAccounts);
            //MacGuffinsUnlockedText.text = string.Format("MacGuffins Unlocked: {0}", LootBoxModel.Instance.MacGuffinUnlocked);

            BuyBotAccountButton.gameObject.SetActive(LootBoxModel.Instance.UpgradeManager.IsActive(Upgrade.EUpgradeType.AutoGrinder) && !LootBoxModel.Instance.MacGuffinQuest.HideBotButton);
            BotAccountsText.gameObject.SetActive(LootBoxModel.Instance.UpgradeManager.IsActive(Upgrade.EUpgradeType.AutoGrinder));
            BuyBotAccountButton.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("Buy Bot Account (${0})", LootBoxModel.Instance.MacGuffinQuest.CostPerBot());

            bool showLoot = LootBoxModel.Instance.TotalLootBoxes > 0;
            LootBoxPanel.SetActive(showLoot);
            
            bool showTrash = LootBoxModel.Instance.LootBoxesOpened > 0;
            SellItemsPanel.SetActive(showLoot);

            bool showBots = LootBoxModel.Instance.NumBotAccounts > 0;
            BotsPanel.SetActive(showBots);

            int actions = (int) LootBoxModel.Instance.MacGuffinQuest.ActionsPerClick();
            string actionsString = actions == 1 ? string.Empty : string.Format("(x{0})", actions);

            //TODO: have an indicator of actions count
            //GrindButtonText.text = string.Format("{0} {1}", LootBoxModel.Instance.GrindsCompleted > 5 ? "Grind" : "Play", actionsString);
            //OpenLootBoxButton.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("Open Loot Box {0}", actionsString);
            //SellTrashItemButton.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("Sell Trash Items {0}", actionsString);
        }
    }
}
