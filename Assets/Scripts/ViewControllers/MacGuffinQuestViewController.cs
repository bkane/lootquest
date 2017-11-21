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

        //Buttons
        public Button GrindButton;
        public Button BuyLootBoxButton;
        public Button OpenLootBoxButton;

        private void Awake()
        {
            GrindButton.onClick.AddListener(() => { Model.MacGuffinQuest.DoGrind(); });
            BuyLootBoxButton.onClick.AddListener(() => { Model.MacGuffinQuest.BuyLootBox(); });
            OpenLootBoxButton.onClick.AddListener(() => { Model.MacGuffinQuest.OpenLootBox(); });
        }

        private void Update()
        {
            GrindProgressText.text = string.Format("Grind: {0}", Model.GrindProgress);
            LootBoxText.text = string.Format("Loot Boxes: {0}", Model.LootBoxes);
            ItemsText.text = string.Format("Trash Items: {0}", Model.TrashItems);
        }
    }
}
