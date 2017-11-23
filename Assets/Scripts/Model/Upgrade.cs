using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class Upgrade
    {
        public enum EUpgradeType
        {
            //Life
            EnergyDrinks,
            LearnToCode,

            //Job
            WorkSmarter,
            DressForSuccess,
            JobAutomationScript,
            SecondJob,
            FasterComputer,
            WatercooledComputer,

            //MacGuffinQuest
            PurchaseMacGuffinQuest,
            PurchaseMacGuffinQuestLimitedEdition,
            RemoveGameAnimations,
            SecondMouse,
            TieFiveMiceTogether,
            AutoGrinder,
            AutoSellTrashItems,
            //pc upgrades, water cooling, gpu sli, etc

            //Influencer
            UnlockInfluencerCareer,
            BuyVideoEditingSoftware,
            MakeVideoIntro,
            GetPartnered,
            HireVideoEditor,


            //Studio
            StartAStudio,
            EnableMicrotransactions,


            //Public
            ExecuteIPO,
            PurchaseMacGuffinQuestSourceCode
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
        public List<EUpgradeType> Requirements = new List<EUpgradeType>();
        public string CommentOnBuy;
    }
}
