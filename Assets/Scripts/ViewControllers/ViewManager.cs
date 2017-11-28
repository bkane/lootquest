using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ViewManager : MonoBehaviour
    {
        public GameObject TitleScreen;

        public GameObject OptionsUI;
        public GameObject OptionsPanel;
        public GameObject ConfirmRestartPopup;

        public GameObject LifeView;
        public GameObject UpgradeView;

        public GameObject JobView;
        public GameObject MacGuffinQuestView;
        public GameObject InfluencerView;
        public GameObject StudioView;
        public GameObject PublicView;

        public GameObject EndGameView;

        private void Start()
        {
            foreach(var button in GameObject.FindObjectsOfType<Button>())
            {
                button.onClick.AddListener(() => { DarkTonic.MasterAudio.MasterAudio.PlaySound("click"); });
            }
        }

        public void Update()
        {
            //TODO: it's lazy to do this in Update
            LifeView.SetActive(LootBoxModel.Instance.Life.IsActive);
            UpgradeView.SetActive(LootBoxModel.Instance.UpgradeManager.IsActive);
            JobView.SetActive(LootBoxModel.Instance.Job.IsActive);
            MacGuffinQuestView.SetActive(LootBoxModel.Instance.MacGuffinQuest.IsActive);
            InfluencerView.SetActive(LootBoxModel.Instance.Influencer.IsActive);
            StudioView.SetActive(LootBoxModel.Instance.Studio.IsActive);
            PublicView.SetActive(LootBoxModel.Instance.Public.IsActive);
        }
    }
}
