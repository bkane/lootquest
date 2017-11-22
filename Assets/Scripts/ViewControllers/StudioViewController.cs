using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class StudioViewController : MonoBehaviour
    {
        public LootBoxModel Model;

        //Labels
        public TextMeshProUGUI DeveloperText;
        public TextMeshProUGUI DevCostText;
        public TextMeshProUGUI GameProgressText;

        //Buttons
        public Button HireDeveloper;
        public Button FireDeveloper;

        private void Awake()
        {
            HireDeveloper.onClick.AddListener(Model.Studio.HireDeveloper);
            FireDeveloper.onClick.AddListener(Model.Studio.FireDeveloper);
        }

        private void Update()
        {
            DeveloperText.text = string.Format("Devs: {0}", Model.Developers);
            DevCostText.text = string.Format("Dev Cost: ${0}/s", Model.Studio.DevCostPerTick() * 30);
            GameProgressText.text = string.Format("GameProgress: {0}%", Model.GameProgress);
        }
    }
}

