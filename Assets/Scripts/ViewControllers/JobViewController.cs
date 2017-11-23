using Assets.Scripts.Model;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ViewControllers
{
    public class JobViewController : MonoBehaviour
    {
        public LootBoxModel Model;

        //Labels
        public TextMeshProUGUI JobProgressText;
        public TextMeshProUGUI WageText;

        //Buttons
        public Button DoJobButton;

        private void Awake()
        {
            DoJobButton.onClick.AddListener(Model.Job.DoJobClick);
        }

        private void Update()
        {
            JobProgressText.text = string.Format("Job Progress: {0}%", Model.JobProgress);
            WageText.text = string.Format("Wage: ${0}", Model.Job.MoneyPerJobCompleted());
        }
    }
}
