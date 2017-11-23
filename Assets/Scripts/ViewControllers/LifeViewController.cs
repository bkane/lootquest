using Assets.Scripts.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class LifeViewController : MonoBehaviour
    {
        //Labels
        public TextMeshProUGUI TimeText;
        public TextMeshProUGUI MoneyText;

        //Buttons


        private void Awake()
        {
        }

        private void Update()
        {
            TimeText.text = string.Format("Time: {0}", LootBoxModel.Instance.Time.GetTimeString());
            MoneyText.text = string.Format("Money: ${0}", LootBoxModel.Instance.Money);
        }
    }
}
