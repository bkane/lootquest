using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class MacGuffinQuestViewController : MonoBehaviour
    {
        public LootBoxModel Model;

        //Labels
        public TextMeshProUGUI GrindProgressText;
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
            GrindButton.onClick.AddListener(() => { Model.MacGuffinQuest.DoGrind(10); });
            SellTrashItemButton.onClick.AddListener(() => { Model.MacGuffinQuest.SellTrash(true); });
            BuyLootBoxButton.onClick.AddListener(() => { Model.MacGuffinQuest.BuyLootBox(); });
            OpenLootBoxButton.onClick.AddListener(() => { Model.MacGuffinQuest.OpenLootBox(); });
            BuyBotAccountButton.onClick.AddListener(() => { Model.MacGuffinQuest.BuyBotAccount(); });
        }

        private void Update()
        {
            GrindProgressText.text = string.Format("Grind: {0}%", Model.GrindProgress);
            LootBoxText.text = string.Format("Loot Boxes: {0}", Model.LootBoxes);
            ItemsText.text = string.Format("Trash Items: {0}", Model.TrashItems);
            BotAccountsText.text = string.Format("Bot Accounts: {0}", Model.NumBotAccounts);

            BuyBotAccountButton.gameObject.SetActive(Model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AutoGrinder));
            BotAccountsText.gameObject.SetActive(Model.UpgradeManager.IsActive(Upgrade.EUpgradeType.AutoGrinder));
        }
    }
}
