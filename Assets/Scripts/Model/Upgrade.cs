﻿using Newtonsoft.Json;
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
            GetJob,
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
            AutoOpenBoxes,
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
            EnableAnalytics,
            EnableMicrotransactions,
            StartDistributionService,
            AddWeeklyRewards,
            AddDailyRewards,
            AddConstantRewards,
            ChargeMore,
            ChargeEvenMore,
            SellLimitedEditions,
            SellCollectorEditions,
            EliminateUnderperformingFranchises,
            AddGems,
            AddGoldCoins,
            AddCrystals,
            AddCards,
            AddGoldBoxes,
            AddSeasonalBoxes,
            AddLuteBoxes,
            AddSkinnerBoxes,
            BuffsInBoxes,
            CoreGameInBoxes,
            WholeGameInBoxes,
            MarketingCampaign,
            LootBoxPreOrders,


            //Public
            ExecuteIPO,
            Layoffs,
            ContractEmployees,
            ComputersMaintainGame,
            OptimizeRoAS,
            TargetedPersonalAds,
            EnforceWatchingAds,
            ReduceLootBoxOdds,
            ReduceLootBoxOddsToZero,
            EmitInaudibleSound,
            PurchaseBelovedStudio,
            TargetChildren,
            UseDataBreach,
            CauseDataBreach,
            TargetMinnows,
            DetermineDesires,
            LaunchMeshNetwork,
            UnlockLobbying,
            UnlockCPU,
            UnlockBioEngineering,
            IsolateMicrotransactionGene,
            ReduceFines,
            RollBackBan,
            DiscloseOdds,
            SellBuffsToOdds,

            //End game
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
        public string MessageOnUnlock;
    }
}
