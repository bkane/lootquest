using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class MacGuffinQuestViewController : MonoBehaviour
    {
        public LootBoxModel Model;

        //Labels
        public TextMeshProUGUI LootBoxText;

        //Buttons
        public Button BuyLootBoxButton;

        private void Awake()
        {
            BuyLootBoxButton.onClick.AddListener(() => { Model.MacGuffinQuest.BuyLootBox(); });
        }

        private void Update()
        {
            LootBoxText.text = string.Format("Loot Boxes: {0}", Model.LootBoxes);
        }
    }
}
