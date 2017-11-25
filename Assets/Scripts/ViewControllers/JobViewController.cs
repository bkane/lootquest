using Assets.Scripts.Model;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class JobViewController : MonoBehaviour
    {
        //Labels
        public TextMeshProUGUI JobProgressText;
        public TextMeshProUGUI WageText;

        //Buttons
        public Button DoJobButton;
        public Slider JobProgress;

        private void Awake()
        {
            DoJobButton.onClick.AddListener(LootBoxModel.Instance.Job.DoJobClick);
        }

        private void Update()
        {
            JobProgress.value = LootBoxModel.Instance.JobProgress / 100f;
            WageText.text = string.Format("Wage: ${0}", LootBoxModel.Instance.Job.MoneyPerJobCompleted());
        }
    }
}
