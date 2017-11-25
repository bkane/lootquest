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
        public TextMeshProUGUI ClicksText;
        public TextMeshProUGUI RateText;

        //Buttons


#if DEBUG
        BigNum rate = 0;
        long lastTicks;
        BigNum lastMoney;
#endif

        private void Awake()
        {
        }

        private void Update()
        {
            TimeText.text = string.Format("Time: {0}", LootBoxModel.Instance.Time.GetTimeString());
            MoneyText.text = string.Format("${0}", LootBoxModel.Instance.Money);
            ClicksText.text = string.Format("Clicks: {0}", LootBoxModel.Instance.Clicks);

#if DEBUG
            long curTicks = LootBoxModel.Instance.TickCount;
            if (curTicks - lastTicks >= 30)
            {
                rate = (LootBoxModel.Instance.TotalMoneyEarned - lastMoney) * 60;
                lastTicks = curTicks;
                lastMoney = LootBoxModel.Instance.TotalMoneyEarned;
            }
            RateText.text = string.Format("Earning: ${0}/min", rate);
#endif
        }
    }
}
