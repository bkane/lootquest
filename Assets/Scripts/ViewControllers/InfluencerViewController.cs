using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class InfluencerViewController : MonoBehaviour
    {
        public LootBoxModel Model;

        //Labels
        public TextMeshProUGUI VideoContentText;
        public TextMeshProUGUI VideoProgressText;
        public TextMeshProUGUI FollowersText;
        public TextMeshProUGUI FollowersPerSecondText;
        public TextMeshProUGUI VideosText;
        public TextMeshProUGUI AdRevenuePerSecondText;

        //Buttons
        public Button MakeVideoButton;

        private void Awake()
        {
            MakeVideoButton.onClick.AddListener(Model.Influencer.DoMakeVideo);
        }

        private void Update()
        {
            VideoContentText.text = string.Format("Unboxing Footage: {0}", Model.VideoContent);
            VideoProgressText.text = string.Format("Video Progress: {0}%", Model.VideoProgress);
            FollowersText.text = string.Format("Followers: {0}", Model.Followers);
            VideosText.text = string.Format("Published Videos: {0}", Model.PublishedVideos);
            AdRevenuePerSecondText.text = string.Format("Ad Rev: ${0}/s", Model.Influencer.AdRevenuePerTick() * 30);
            FollowersPerSecondText.text = string.Format("Channel Growth: {0}/s", Model.Influencer.FollowersPerTick() * 30);

            FollowersPerSecondText.gameObject.SetActive(Model.UpgradeManager.IsActive(Scripts.Model.Upgrade.EUpgradeType.ChannelGrowthAnalytics));
            AdRevenuePerSecondText.gameObject.SetActive(Model.UpgradeManager.IsActive(Scripts.Model.Upgrade.EUpgradeType.GetPartnered));
        }
    }
}
