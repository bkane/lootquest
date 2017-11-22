using UnityEngine;

namespace Assets.Scripts
{
    public class ViewManager : MonoBehaviour
    {
        public LootBoxModel Model;

        public GameObject LifeView;
        public GameObject UpgradeView;

        public GameObject JobView;
        public GameObject MacGuffinQuestView;
        public GameObject InfluencerView;
        public GameObject StudioView;

        public void Update()
        {
            //TODO: it's lazy to do this in Update
            LifeView.SetActive(true);
            UpgradeView.SetActive(true);
            
            JobView.SetActive(Model.Job.IsActive);
            MacGuffinQuestView.SetActive(Model.MacGuffinQuest.IsActive);
            InfluencerView.SetActive(Model.Influencer.IsActive);
            StudioView.SetActive(Model.Studio.IsActive);
        }
    }
}
