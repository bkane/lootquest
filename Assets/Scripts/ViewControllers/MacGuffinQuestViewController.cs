using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class MacGuffinQuestViewController : MonoBehaviour
    { 
        //Labels
        public TextMeshProUGUI GrindProgressText;
        public TextMeshProUGUI MacGuffinsUnlockedText;
        public TextMeshProUGUI LootBoxText;
        public TextMeshProUGUI ItemsText;
        public TextMeshProUGUI BotAccountsText;

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
            GrindProgressText.text = string.Format("Grind: {0}%", LootBoxModel.Instance.GrindProgress);
            LootBoxText.text = string.Format("Loot Boxes: {0}", LootBoxModel.Instance.LootBoxes);
            ItemsText.text = string.Format("Emotes and Skins: {0}", LootBoxModel.Instance.TrashItems);
            BotAccountsText.text = string.Format("Bot Accounts: {0}", LootBoxModel.Instance.NumBotAccounts);
            MacGuffinsUnlockedText.text = string.Format("MacGuffins Unlocked: {0}", LootBoxModel.Instance.MacGuffinUnlocked);

            BuyBotAccountButton.gameObject.SetActive(LootBoxModel.Instance.UpgradeManager.IsActive(Upgrade.EUpgradeType.AutoGrinder) && !LootBoxModel.Instance.MacGuffinQuest.HideBotButton);
            BotAccountsText.gameObject.SetActive(LootBoxModel.Instance.UpgradeManager.IsActive(Upgrade.EUpgradeType.AutoGrinder));
            BuyBotAccountButton.GetComponentInChildren<Text>().text = string.Format("Buy Bot Account (${0})", LootBoxModel.Instance.MacGuffinQuest.CostPerBot());

            bool showLoot = LootBoxModel.Instance.TotalLootBoxes > 0;
            LootBoxText.gameObject.SetActive(showLoot);
            OpenLootBoxButton.gameObject.SetActive(showLoot);

            bool showTrash = LootBoxModel.Instance.LootBoxesOpened > 0;
            ItemsText.gameObject.SetActive(showTrash);
            SellTrashItemButton.gameObject.SetActive(showTrash);

            int actions = (int) LootBoxModel.Instance.MacGuffinQuest.ActionsPerClick();
            string actionsString = actions == 1 ? string.Empty : string.Format("(x{0})", actions);
            GrindButton.GetComponentInChildren<Text>().text = string.Format("{0} {1}", LootBoxModel.Instance.GrindsCompleted > 5 ? "Grind" : "Play", actionsString);
            OpenLootBoxButton.GetComponentInChildren<Text>().text = string.Format("Open Loot Box {0}", actionsString);
            SellTrashItemButton.GetComponentInChildren<Text>().text = string.Format("Sell Trash Items on Auction House {0}", actionsString);
        }
    }
}
