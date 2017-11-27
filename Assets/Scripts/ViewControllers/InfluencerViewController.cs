using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class InfluencerViewController : MonoBehaviour
    {
        //Labels
        public TextMeshProUGUI VideoContentText;
        public Slider VideoProgress;
        public TextMeshProUGUI FollowersText;
        public TextMeshProUGUI FollowersPerSecondText;
        public TextMeshProUGUI VideosText;
        public TextMeshProUGUI AdRevenuePerSecondText;

        //Buttons
        public Button MakeVideoButton;

        private void Awake()
        {
            MakeVideoButton.onClick.AddListener(LootBoxModel.Instance.Influencer.DoMakeVideoClick);
        }

        private void Update()
        {
            VideoContentText.text = string.Format("Unboxing Footage: {0}", LootBoxModel.Instance.VideoContent);
            VideoProgress.value = LootBoxModel.Instance.VideoProgress / 100f;
            FollowersText.text = string.Format("Followers: {0}", LootBoxModel.Instance.Followers);
            VideosText.text = string.Format("Published Videos: {0}", LootBoxModel.Instance.PublishedVideos);
            AdRevenuePerSecondText.text = string.Format("Ad Rev: ${0}/s", (BigNum)(LootBoxModel.Instance.Influencer.AdRevenuePerTick() * 30));
            FollowersPerSecondText.text = string.Format("Channel Growth: {0}/s", (BigNum)(LootBoxModel.Instance.Influencer.FollowersPerTick() * 30));

            FollowersPerSecondText.gameObject.SetActive(LootBoxModel.Instance.UpgradeManager.IsActive(Scripts.Model.Upgrade.EUpgradeType.ChannelGrowthAnalytics));
            AdRevenuePerSecondText.gameObject.SetActive(LootBoxModel.Instance.UpgradeManager.IsActive(Scripts.Model.Upgrade.EUpgradeType.GetPartnered));
        }
    }
}
