﻿using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class Upgrade
    {
        public enum EUpgradeType
        {
            //Life
            EnergyDrinks,

            //Job
            JobAutomationScript,

            //MacGuffinQuest
            PurchaseMacGuffinQuest,
            AutoGrinder,
            AutoSellTrashItems,
            //pc upgrades, water cooling, gpu sli, etc

            //Influencer
            HireVideoEditor
        }

        public enum EState
        {
            Hidden,
            Visible,
            Purchased
        }

        public EUpgradeType Type;
        public string Name;
        public string Description;
        public List<Resource> Costs;
        public EState State;
    }
}
