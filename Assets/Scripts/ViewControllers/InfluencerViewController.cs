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
            VideoContentText.text = string.Format("Video Content: {0}", Model.VideoContent);
            FollowersText.text = string.Format("Followers: {0}", Model.Followers);
            VideosText.text = string.Format("Published Videos: {0}", Model.Videos);
        }
    }
}
