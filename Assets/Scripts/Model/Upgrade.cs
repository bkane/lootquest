using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Assets.Scripts.Model
{
    public class Upgrade
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum EUpgradeType
        {
            //Life
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
            StartStreaming,
            DoSponsoredVideos,
            CompletelySellOut,
            ChannelGrowthAnalytics,
            OptimizeContentForChannelGrowth,
            HireProVideoEditor,

            //Studio
            StartAStudio,
            EnableMicrotransactions,
            StartDistributionService,


            //Public
            ExecuteIPO,
            PurchaseMacGuffinQuestSourceCode
        }

        [JsonConverter(typeof(StringEnumConverter))]
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
        public List<Resource> UnlockThreshold = new List<Resource>();
        public List<EUpgradeType> Requirements = new List<EUpgradeType>();
        public string CommentOnBuy;
    }
}
