﻿using Assets.Scripts.Model;
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
        public TextMeshProUGUI MoneyText;
        public TextMeshProUGUI JobProgressText;

        //Buttons
        public Button DoJobButton;

        private void Awake()
        {
            DoJobButton.onClick.AddListener(() => { Model.Job.DoJob(10); });
        }

        private void Update()
        {
            MoneyText.text = string.Format("Money: ${0}", Model.Money);
            JobProgressText.text = string.Format("Job Progress: {0}%", Model.JobProgress);
        }
    }
}
