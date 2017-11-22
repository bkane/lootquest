using System.Collections.Generic;

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
            SecondJob,

            //MacGuffinQuest
            PurchaseMacGuffinQuest,
            AutoGrinder,
            AutoSellTrashItems,
            //pc upgrades, water cooling, gpu sli, etc

            //Influencer
            UnlockInfluencerCareer,
            HireVideoEditor,


            //Studio
            StartAStudio,
            EnableMicrotransactions,


            //Public
            ExecuteIPO
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
        public List<Resource> Costs = new List<Resource>();
        public EState State = Upgrade.EState.Hidden;
        public List<Resource> UnlockThreshold = new List<Resource>();
    }
}
