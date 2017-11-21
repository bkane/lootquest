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
        public TextMeshProUGUI VideosText;

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
        }
    }
}
