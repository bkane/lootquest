using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UpgradeManager
    {
        public bool IsActive;
        public LootBoxModel Model;
        public Dictionary<Upgrade.EUpgradeType, Upgrade> Upgrades;

        [JsonProperty]
        public Dictionary<Upgrade.EUpgradeType, Upgrade.EState> UpgradeStates;

        private BigNum MCGCost = 30;

        public UpgradeManager(LootBoxModel model)
        {
            this.Model = model;
            Upgrades = new Dictionary<Upgrade.EUpgradeType, Upgrade>();
            UpgradeStates = new Dictionary<Upgrade.EUpgradeType, Upgrade.EState>();



            #region Life upgrades

            Upgrades.Add(Upgrade.EUpgradeType.LearnToCode, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.LearnToCode,
                Name = "Learn to Code",
                Description = "Looks like computers might be sticking around for a while. I should learn to talk to them.",
                CommentOnBuy = "Now I get all those binary jokes!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 40)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.TotalMoneyEarned, 30)
                }
            });

            #endregion


            #region Job upgrades

            Upgrades.Add(Upgrade.EUpgradeType.GetJob, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.GetJob,
                Name = "Get a Job",
                Description = "Money can be exchanged for goods and services. If I want to play <i>MacGuffin Quest 2</i>, I'll need to become un-unemployed.",
                CommentOnBuy = "Getting a job was super easy! Suspiciously easy. I hope working doesn't suck.",
                Costs = new List<Resource>()
                { 
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.JobCompleted, 50) //to prevent auto-unlock
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.WorkSmarter, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.WorkSmarter,
                Name = "Buy a Book on Life Hacks",
                Description = "Work smarter not harder! Get more work done with every click.",
                CommentOnBuy = "I can save time by eating all my meals in the shower. Nice!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 16)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.JobCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.DressForSuccess, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.DressForSuccess,
                Name = "Dress for Success",
                Description = "Dress for the job you want, and then get that job. Promotion means more money!",
                CommentOnBuy = "Jogging pants might be holding me back professionally.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 25)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.JobCompleted, 10)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.JobAutomationScript, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.JobAutomationScript,
                Name = "Write Script to Automate Job",
                Description = "It's going to happen anyway. Might as well do it myself while the paycheck still goes to me.",
                CommentOnBuy = "while(true) { doJob(); } //This was way easier than I thought",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 40)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.LearnToCode }
            });

            Upgrades.Add(Upgrade.EUpgradeType.SecondJob, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.SecondJob,
                Name = "Get a Second Job",
                Description = "I work from home anyway. Who's going to notice?",
                CommentOnBuy = "I'll have to put my cat's name down as a reference and hope they don't check.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 250)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.JobCompleted, 50)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.JobAutomationScript }
            });

            Upgrades.Add(Upgrade.EUpgradeType.FasterComputer, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.FasterComputer,
                Name = "Buy a Better Computer",
                Description = "Invest in better hardware since it's the one doing all the work anyway.",
                CommentOnBuy = "Gotta have the fastest rig for these spreadsheets!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 200)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.JobAutomationScript }
            });

            Upgrades.Add(Upgrade.EUpgradeType.WatercooledComputer, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.WatercooledComputer,
                Name = "Get Liquid-cooling and Overclock your Rig",
                Description = "Hardly seems worth it for the nominal increase in speed but who am I to judge?",
                CommentOnBuy = "I can never look at documents at less than 4K 120Hz ever again.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 500)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.FasterComputer }
            });

            #endregion


            #region MacGuffinQuest upgrades

            Upgrades.Add(Upgrade.EUpgradeType.PurchaseMacGuffinQuest, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.PurchaseMacGuffinQuest,
                Name = "Pre-Order <i>MacGuffin Quest 2</i>",
                Description = "Holy crap I can't wait.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, MCGCost)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Money, 10) //just to prevent auto-unlocking
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.PurchaseMacGuffinQuestLimitedEdition, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.PurchaseMacGuffinQuestLimitedEdition,
                Name = "Buy <i>MacGuffin Quest 2</i> Deluxe Limited Edition",
                Description = "Well it's more expensive but at least it's in stock.",
                CommentOnBuy = "YESSSSS! Finally! Time to play <i>MacGuffin Quest 2</i>!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 80)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.PurchaseMacGuffinQuest }
            });

            Upgrades.Add(Upgrade.EUpgradeType.SecondMouse, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.SecondMouse,
                Name = "Become Ambidextrous",
                Description = "If I buy another mouse and use both hands, I can grind twice as fast! I mean, have twice as much fun.",
                CommentOnBuy = "If I can just get one of those drink hat things then I'll be set for life.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 50)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.GrindCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.TieFiveMiceTogether, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.TieFiveMiceTogether,
                Name = "Tape Five Mice Together",
                Description = "I read about how, with a few minor adjustments, I can turn a regular mouse into five mice!",
                CommentOnBuy = "Wait, or is it five mice into one mice? And I already had two, so it's really ten.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 400)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.GrindCompleted, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.SecondMouse }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AutoGrinder, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AutoGrinder,
                Name = "Write Auto-Grind Script",
                Description = "Use your newfound scripting talents to have your computer play <i>MacGuffin Quest 2</i> for you! Automatic fun!",
                CommentOnBuy = "I'll come back and play just as soon as the script gets the MacGuffin unlock in one of these loot boxes. Shouldn't take long.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 1000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.GrindCompleted, 25)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.LearnToCode, Upgrade.EUpgradeType.WatercooledComputer }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AutoSellTrashItems, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AutoSellTrashItems,
                Name = "Write Auto-Auction Script",
                Description = "Selling items on the auction house feels like work and 'round here? We automate work.",
                CommentOnBuy = "It's just bots selling to other bots. Humans have no place here.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 1500)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.TrashItemSold, 20)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AutoGrinder }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AutoOpenBoxes, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AutoOpenBoxes,
                Name = "Write Auto-Open Loot Boxes Script",
                Description = "Loot boxes can be openly more efficiently by machine.",
                CommentOnBuy = "It pains me to automate this since opening loot boxes is <i>so much fun</i>, but I'll come back to <i>MacGuffin Quest 2</i> the instant MacGuffin is unlocked.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 5000)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AutoGrinder, Upgrade.EUpgradeType.AutoSellTrashItems }
            });
            
            Upgrades.Add(Upgrade.EUpgradeType.RemoveGameAnimations, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.RemoveGameAnimations,
                Name = "Hack <i>MacGuffin Quest 2</i> and Remove All Animations",
                Description = "Use your newfound computer talents to remove those pesky animations slowing things down. Improves grinding efficiency.",
                CommentOnBuy = "Sorry artists but sacrifices must be made. For loot boxes.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 2000)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.LearnToCode, Upgrade.EUpgradeType.AutoGrinder }
            });

            #endregion


            #region Influencer Upgrades

            Upgrades.Add(Upgrade.EUpgradeType.UnlockInfluencerCareer, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.UnlockInfluencerCareer,
                Name = "Buy a Capture Card",
                Description = "Hey, I bet people would watch me opening all these loot boxes.",
                CommentOnBuy = "What's up folks! Welcome to my loot box unboxing channel. Hit that subscribe button! DAE like loot boxes? Leave a comment and let me know!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 2000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.LootBoxOpened, 100)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.GetPartnered, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.GetPartnered,
                Name = "Get Partnered",
                Description = "Start monetizing your content. Unlocks ad revenue.",
                CommentOnBuy = "Not sure why I had to pay to become a partner but I'm sure it'll be a worthwhile investment.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 2000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Follower, 1000)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.BuyVideoEditingSoftware, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.BuyVideoEditingSoftware,
                Name = "Buy Video Editing Software",
                Description = "The software that came free with my cereal is lacking a few features. Reduce the work needed to produce videos.",
                CommentOnBuy = "Whoa! This video editing program supports more than 256 colors! This'll make things a lot easier.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 3500)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 2)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.MakeVideoIntro, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.MakeVideoIntro,
                Name = "Make Re-useable Video Intros",
                Description = "More re-usable footage means less effort to make videos.",
                CommentOnBuy = "Now I don't have to say \"What's up folks!\" all the time.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 5000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.BuyVideoEditingSoftware }
            });

            Upgrades.Add(Upgrade.EUpgradeType.HireVideoEditor, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.HireVideoEditor,
                Name = "Hire Video Editor",
                Description = "Hire an editor to convert your raw footage into compelling content. Your editor assures you they're worth every penny, given what they have to work with.",
                CommentOnBuy = "I can't believe nobody wanted to do the job in exchange for exposure. They all wanted money!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.MakeVideoIntro }
            });

            Upgrades.Add(Upgrade.EUpgradeType.StartStreaming, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.StartStreaming,
                Name = "Start Live-Streaming",
                Description = "Live-stream the unboxings before you turn them into videos. More opportunities to gain followers!",
                CommentOnBuy = "I wonder what this will do to my sleep schedule...",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Follower, 5000)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.GetPartnered }
            });

            Upgrades.Add(Upgrade.EUpgradeType.DoSponsoredVideos, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.DoSponsoredVideos,
                Name = "Do Sponsored Videos",
                Description = "Get more revenue per video.",
                CommentOnBuy = "People <i>need</i> mattresses. Why shouldn't I get paid to give my opinion?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Follower, 10000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Follower, 5000)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.GetPartnered }
            });

            Upgrades.Add(Upgrade.EUpgradeType.CompletelySellOut, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.CompletelySellOut,
                Name = "Completely Sell Out",
                Description = "Sponsor anything and everything.",
                CommentOnBuy = "Okay, maybe not <i>everybody</i> needs custom toilets, but I need more money for loot boxes. Feed the beast!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Follower, 200000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Follower, 100000)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.DoSponsoredVideos, Upgrade.EUpgradeType.StartStreaming }
            });

            Upgrades.Add(Upgrade.EUpgradeType.ChannelGrowthAnalytics, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ChannelGrowthAnalytics,
                Name = "Track Channel Growth",
                Description = "Do some analysis and figure out where all these followers are coming from.",
                CommentOnBuy = "Without data, I'm just fumbling around in the dark.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 1000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.GetPartnered }
            });

            Upgrades.Add(Upgrade.EUpgradeType.OptimizeContentForChannelGrowth, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.OptimizeContentForChannelGrowth,
                Name = "Optimize Video Content for Channel Growth",
                Description = "Perform exhaustive research and experimentation to determine the best ways to drive engagement to your channel.",
                CommentOnBuy = "Huh, turns out that saying, \"Smash that Like button!\" really <i>is</i> the most effective thing to do.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 4000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ChannelGrowthAnalytics }
            });

            Upgrades.Add(Upgrade.EUpgradeType.HireProVideoEditor, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.HireProVideoEditor,
                Name = "Hire a Pro Video Editor",
                Description = "I know someone who can churn out videos in half the time.",
                CommentOnBuy = "Okay so the video quality isn't quite as good, but it's clips of a bot opening loot boxes. What do you expect?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 30000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 50)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.HireVideoEditor }
            });

            #endregion


            #region Studio Upgrades

            Upgrades.Add(Upgrade.EUpgradeType.StartAStudio, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.StartAStudio,
                Name = "Start a Game Dev Studio",
                Description = "You know, I could probably do this myself.",
                CommentOnBuy = "Now that I'm in charge, I vow never to go down the dark road of microtransactions. Unless I really need to.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 250000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Follower, 100000)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.EnableAnalytics, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.EnableAnalytics,
                Name = "Enable Analytics",
                Description = "Start gathering data about how players are playing our games. So we can improve the experience of course.",
                CommentOnBuy = "No harm in keeping track of a few other things while we're at it. Might come in handy.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 25000),
                    new Resource(Units.DevHour, 200)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.StartAStudio }
            });

            Upgrades.Add(Upgrade.EUpgradeType.EnableMicrotransactions, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.EnableMicrotransactions,
                Name = "Add Microtransactions",
                Description = "Just for cosmetic items of course.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 25000),
                    new Resource(Units.DevHour, 300)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.EnableAnalytics }
            });


            Upgrades.Add(Upgrade.EUpgradeType.StartDistributionService, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.StartDistributionService,
                Name = "Start our own Digital Distribution Service",
                Description = "Cut out the middle man and the middle man's cut is ours! It'll need a catchy name, like Game Shovel, only not so stupid.",
                CommentOnBuy = "Happy Game Shovel launch day everyone!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 100000000),
                    new Resource(Units.DevHour, 50000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ReleasedGame, 10)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddWeeklyRewards, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddWeeklyRewards,
                Name = "Add Weekly Rewards",
                Description = "Our research shows we can keep players coming back to our games if we give them a token reward every now and then.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 50000),
                    new Resource(Units.DevHour, 500),
                    new Resource(Units.AnalyticsData, 100),
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.AnalyticsData, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.EnableMicrotransactions }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddDailyRewards, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddDailyRewards,
                Name = "Add Daily Rewards",
                Description = "Further research reveals player retention can be improved with more frequent rewards.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 250000),
                    new Resource(Units.DevHour, 2500),
                    new Resource(Units.AnalyticsData, 500),
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.AddWeeklyRewards }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddConstantRewards, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddConstantRewards,
                Name = "Add Constant Rewards",
                Description = "Optimize the reward drip so completely that nobody ever stops playing. Even if they want to.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 2500000),
                    new Resource(Units.DevHour, 25000),
                    new Resource(Units.AnalyticsData, 5000),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AddDailyRewards }
            });

            Upgrades.Add(Upgrade.EUpgradeType.ChargeMore, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ChargeMore,
                Name = "Charge More For New Games",
                Description = "Games are getting expensive to make, so we need to charge more to stay afloat.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 50000),
                    new Resource(Units.AnalyticsData, 500)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.TotalAnalyticsData, 1000)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.ChargeEvenMore, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ChargeEvenMore,
                Name = "Charge Even More for New Games",
                Description = "When we raised prices, we made more money. I recommend we de-undermonetize by doing that again.",
                CommentOnBuy = "If you factor for inflation, it's actually still really expensive.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 100000),
                    new Resource(Units.AnalyticsData, 1000)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ChargeMore }
            });

            Upgrades.Add(Upgrade.EUpgradeType.SellLimitedEditions, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.SellLimitedEditions,
                Name = "Sell Limited Editions",
                Description = "Market research shows people <i>want</i> to pay more, so we should offer more SKUs and let them.",
                CommentOnBuy = "What does \"Limited\" refer to for a digital product anyway?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 300000),
                    new Resource(Units.AnalyticsData, 2000)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ChargeEvenMore }
            });

            Upgrades.Add(Upgrade.EUpgradeType.SellCollectorEditions, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.SellCollectorEditions,
                Name = "Sell Collector's Editions",
                Description = "Bespoke, hand-craft, artisinal digital game keys.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 9000000),
                    new Resource(Units.AnalyticsData, 10000)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.SellLimitedEditions }
            });

            Upgrades.Add(Upgrade.EUpgradeType.MarketingCampaign, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.MarketingCampaign,
                Name = "Run Marketing Campaigns",
                Description = "More people need to know about our game! Increases the number of sales when releasing a game.",
                CommentOnBuy = "I bet we could get a celebrity to endorse our game. Actually, that might not be a good idea.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 50000)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ReleasedGame, 2)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.EliminateUnderperformingFranchises, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.EliminateUnderperformingFranchises,
                Name = "Eliminate Under-performing Franchises",
                Description = "Reduce development cost of releasing new games.",
                CommentOnBuy = "If it doesn't have the potential to be exploited every year, I don't want to hear about it.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 2000000),
                    new Resource(Units.AnalyticsData, 30000),
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ReleasedGame, 7)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.MarketingCampaign }
            });

            Upgrades.Add(Upgrade.EUpgradeType.LootBoxPreOrders, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.LootBoxPreOrders,
                Name = "Sell Pre-Orders For Loot Boxes",
                Description = "Why are we waiting until release to monetize players? Increases the number of sales when releasing a game.",
                CommentOnBuy = "Pre-orders for plays on a virtual slot machine. I didn't think people would buy it, but here we are.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 50000),
                    new Resource(Units.DevHour, 3000),
                    new Resource(Units.AnalyticsData, 3000)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.EnableMicrotransactions, Upgrade.EUpgradeType.MarketingCampaign }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddGems, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddGems,
                Name = "Add Premium Currency: Gems",
                Description = "Players will just <i>love</i> buying these shiny gems! Increases microtransaction spend.",
                CommentOnBuy = "Gems don't really fit in our games thematically, but I suppose it's an abstract concept.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 50000),
                    new Resource(Units.DevHour, 500),
                    new Resource(Units.AnalyticsData, 100)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.EnableMicrotransactions }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddGoldCoins, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddGoldCoins,
                Name = "Add Premium Currency: Gold Coins",
                Description = "Players will like buying these shiny gold coins! Increases microtransaction spend.",
                CommentOnBuy = "Gold coins feels a bit on the nose, don't you think?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 100000),
                    new Resource(Units.DevHour, 1000),
                    new Resource(Units.AnalyticsData, 500)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.AddGems }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddCrystals, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddCrystals,
                Name = "Add Premium Currency: Crystals",
                Description = "Players will tolerate buying these crystals! Increases microtransaction spend.",
                CommentOnBuy = "On the plus side, I don't think players even consciously acknowledge how out of place this stuff is anymore.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 400000),
                    new Resource(Units.DevHour, 2000),
                    new Resource(Units.AnalyticsData, 5000)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AddGoldCoins }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddCards, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddCards,
                Name = "Add Premium Currency: Cards",
                Description = "Players will very reluctantly buy these cards! Increases microtransaction spend.",
                CommentOnBuy = "A digital representation of an already random collectible. Might as well put a slot machine on the card and go full meta.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 1600000),
                    new Resource(Units.DevHour, 4000),
                    new Resource(Units.AnalyticsData, 50000)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AddCrystals }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddGoldBoxes, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddGoldBoxes,
                Name = "Add Gold Loot Boxes",
                Description = "A whole new variety of purchasable loot boxes! This completely different product will entice more players to monetize.",
                CommentOnBuy = "Gold loot boxes do sound pretty novel.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 50e3f),
                    new Resource(Units.DevHour, 500),
                    new Resource(Units.AnalyticsData, 500)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.EnableMicrotransactions }
            });


            Upgrades.Add(Upgrade.EUpgradeType.AddSeasonalBoxes, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddSeasonalBoxes,
                Name = "Add Seasonal Loot Boxes",
                Description = "Holiday loot boxes! Birthday loot boxes! Tuesday Afternoon loot boxes! More to buy means more will buy.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 500e3f),
                    new Resource(Units.DevHour, 2500),
                    new Resource(Units.AnalyticsData, 2500)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AddGoldBoxes }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddLuteBoxes, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddLuteBoxes,
                Name = "Add Lute Boxes",
                Description = "Collectable lutes are all the rage. Well they will be once the marketing campaign kicks in.",
                CommentOnBuy = "Players will applaud our creativity.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 5e6f),
                    new Resource(Units.DevHour, 12500),
                    new Resource(Units.AnalyticsData, 12500)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AddSeasonalBoxes }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddSkinnerBoxes, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddSkinnerBoxes,
                Name = "Add Skinner Boxes",
                Description = "No sense pretending anymore.",
                CommentOnBuy = "Skinner Boxes are surprisingly effective, even when players know what they are.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 50e6f),
                    new Resource(Units.DevHour, 62500),
                    new Resource(Units.AnalyticsData, 62500)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AddLuteBoxes, Upgrade.EUpgradeType.AddCards }
            });

            Upgrades.Add(Upgrade.EUpgradeType.BuffsInBoxes, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.BuffsInBoxes,
                Name = "Put Game Altering Buffs in Loot Boxes",
                Description = "It's not pay to win. But paying does make winning a heck of a lot easier.",
                CommentOnBuy = "When did video games become a cruel reflection of society?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 200e3f),
                    new Resource(Units.DevHour, 20e3f),
                    new Resource(Units.AnalyticsData, 50e3f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AddGoldBoxes }
            });

            Upgrades.Add(Upgrade.EUpgradeType.CoreGameInBoxes, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.CoreGameInBoxes,
                Name = "Put Core Game Elements in Loot Boxes",
                Description = "Games these days are like slot machines. Nice to look at, but they don't do much until you pull the arm.",
                CommentOnBuy = "It's like a normal progression system in a game, but the reward for player effort is entirely random! And thus more fun.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10e6f),
                    new Resource(Units.DevHour, 40e3f),
                    new Resource(Units.AnalyticsData, 100e3f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.BuffsInBoxes, Upgrade.EUpgradeType.AddLuteBoxes }
            });

            Upgrades.Add(Upgrade.EUpgradeType.WholeGameInBoxes, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.WholeGameInBoxes,
                Name = "Put Entire Game in Loot Boxes",
                Description = "What if you had a random chance to play at all? Then everybody would have to pay!",
                CommentOnBuy = "I'd say we've gone to far, but people keep giving us money.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 500e6f),
                    new Resource(Units.DevHour, 20e3f),
                    new Resource(Units.AnalyticsData, 50e3f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.CoreGameInBoxes, Upgrade.EUpgradeType.AddSkinnerBoxes }
            });

            #endregion


            #region Public Upgrades

            Upgrades.Add(Upgrade.EUpgradeType.ExecuteIPO, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ExecuteIPO,
                Name = "Execute IPO",
                Description = "Time to take this to the next level.",
                CommentOnBuy = "We need to focus on our bottom line and allocate resources to effectively stimulate company growth and industry dominance.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10e9f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.WholeGameInBoxes }
            });

            Upgrades.Add(Upgrade.EUpgradeType.Layoffs, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.Layoffs,
                Name = "Regularly Scheduled Layoffs",
                Description = "The end of a project doesn't <i>have</i> to mean the end of a job. But we'll save a bundle if it does!",
                CommentOnBuy = "And yet for some reason this industry is considered in demand.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 50e9f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ExecuteIPO }
            });

            Upgrades.Add(Upgrade.EUpgradeType.ContractEmployees, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ContractEmployees,
                Name = "Use Contract Employees instead of Full Time",
                Description = "Less pay and benefits means more bottom line for us. And it's somehow legal!",
                CommentOnBuy = "It's as if we automated the layoff process.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 200e9f)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.OperatingCost, 100e6f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.Layoffs }
            });

            Upgrades.Add(Upgrade.EUpgradeType.PurchaseBelovedStudio, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.PurchaseBelovedStudio,
                Name = "Purchase Beloved Independent Studio",
                Description = "We're saturating our current customer base. Time to acquire new fans by acquiring a studio, hollowing it out, and discarding the husk that remains.",
                CommentOnBuy = "Well, as long as we treat the newly acquired IP with love and respect.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 100e9f)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 5e6f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ExecuteIPO }
            });

            Upgrades.Add(Upgrade.EUpgradeType.TargetMinnows, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.TargetMinnows,
                Name = "Target the Minnows",
                Description = "Once all the whales have been hunted, it only makes sense to go after the smaller fish.",
                CommentOnBuy = "Our tools are so refined and powerful at this point that the hunting metaphor is misleading. 'Obliterating from orbit' is closer to reality.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 500e12f)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 31e6f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.PurchaseBelovedStudio }
            });

            Upgrades.Add(Upgrade.EUpgradeType.TargetChildren, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.TargetChildren,
                Name = "Incentivize Diminished-Age Purchasers",
                Description = "Target children with microtransactions. They play a lot and don't know the value of money yet.",
                CommentOnBuy = "Don't feel bad. It's not their money they're spending.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 1e15f)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 100e6f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.TargetMinnows }
            });

            Upgrades.Add(Upgrade.EUpgradeType.UseDataBreach, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.UseDataBreach,
                Name = "Acquire Customer Data from Security Breach",
                Description = "One of our competitors was careless with security. All of the data has been leaked anyway. Might as well use it!",
                CommentOnBuy = "Reminder: don't re-use your password on multiple sites.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 50e15f),
                    new Resource(Units.Favor, 100),
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Favor, 10),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.TargetChildren }
            });


            Upgrades.Add(Upgrade.EUpgradeType.CauseDataBreach, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.CauseDataBreach,
                Name = "Arrange a Security Breach",
                Description = "That last security breach turned out really well for us. It'd be a shame if all our competitors experienced similar incidents.",
                CommentOnBuy = "Security breaches are inevitable. We're just giving the process a little boost.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 100e15f),
                    new Resource(Units.Favor, 500),
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Favor, 100)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.UseDataBreach }
            });

            Upgrades.Add(Upgrade.EUpgradeType.UnlockLobbying, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.UnlockLobbying,
                Name = "Start up Government Lobbying Division",
                Description = "We're going to need to change some policies.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 5e15f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ReduceLootBoxOddsToZero }
            });

            Upgrades.Add(Upgrade.EUpgradeType.UnlockCPU, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.UnlockCPU,
                Name = "Start up Machine Learning Division",
                Description = "Nobody seems quite sure what machine learning can be used for, but if it can make us more money then I want it.",
                CommentOnBuy = "Customers are ultimately just buckets of statistics. Computers are really good at dealing with numbers.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10e15f)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 500e6f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ReduceFines }
            });

            Upgrades.Add(Upgrade.EUpgradeType.ComputersMaintainGame, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ComputersMaintainGame,
                Name = "Teach Machines to Maintain the Game",
                Description = "Remove the remaining operating costs by putting machines in charge of running the servers.",
                CommentOnBuy = "All creative work has long since faded away or been taken over by machines. This day was inevitable.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 60e15f),
                    new Resource(Units.Cycle, 1e12f),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.UnlockCPU, Upgrade.EUpgradeType.ContractEmployees }
            });

            Upgrades.Add(Upgrade.EUpgradeType.OptimizeRoAS, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.OptimizeRoAS,
                Name = "Optimize Return on Ad Spend",
                Description = "Artificial Intelligence Marketing platform will significantly reduce churn and continuously optimize integrated player experiences based on the entire player data ecosystem.",
                CommentOnBuy = "Wait, what exactly does this do?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 100e15f),
                    new Resource(Units.Cycle, 2.5e12f),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.UnlockCPU }
            });

            Upgrades.Add(Upgrade.EUpgradeType.TargetedPersonalAds, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.TargetedPersonalAds,
                Name = "Automatic Targeted Ad Personalization",
                Description = "By running millions of variations of slogans and creatives automatically, we can monetize users much more effectively.",
                CommentOnBuy = "The machines know people better than the people know themselves. And the machines can manipulate things they know.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 500e15f),
                    new Resource(Units.Cycle, 10e12f),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.OptimizeRoAS }
            });


            Upgrades.Add(Upgrade.EUpgradeType.EnforceWatchingAds, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.EnforceWatchingAds,
                Name = "Force Customers to Actually Watch Ads",
                Description = "Our facial recognition can now tell if somebody is paying attention to an ad. People will definitely pay to remove this feature!",
                CommentOnBuy = "This one is definitely going to happen in real life :(",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 5e18f),
                    new Resource(Units.Cycle, 50e12f),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.TargetedPersonalAds }
            });

            Upgrades.Add(Upgrade.EUpgradeType.ReduceLootBoxOdds, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ReduceLootBoxOdds,
                Name = "Reduce Loot Box Odds",
                Description = "It's so obvious! If customers as less likely to get what they want in a loot box, then they'll buy more!",
                CommentOnBuy = "It's not like slot machines have good odds. You'd never know if they did anyway.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 2e12f),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.PurchaseBelovedStudio }
            });

            Upgrades.Add(Upgrade.EUpgradeType.ReduceLootBoxOddsToZero, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ReduceLootBoxOddsToZero,
                Name = "Reduce Loot Box Odds to Zero",
                Description = "What... what if we just make the odds of getting what you want zero?",
                CommentOnBuy = "Technically there's no law against it...",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 1e15f),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ReduceLootBoxOdds }
            });


            Upgrades.Add(Upgrade.EUpgradeType.ReduceFines, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ReduceFines,
                Name = "Lobby for Reduced Fines",
                Description = "We're just a struggling company trying to stay afloat in a very competitive industry.",
                CommentOnBuy = "An industry that both gets bigger and has fewer competitors every day.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10e15f),
                    new Resource(Units.Favor, 100),
                },
                UnlockThreshold = new List<Resource>()
                {
                      new Resource(Units.Favor, 10),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ReduceLootBoxOddsToZero }
            });

            Upgrades.Add(Upgrade.EUpgradeType.DiscloseOdds, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.DiscloseOdds,
                Name = "Disclose Loot Box Odds",
                Description = "Submit to regulation and disclose loot box odds to get a repreive from some of the bigger fines.",
                CommentOnBuy = "Thankfully humans have a very poor intuitive understanding of statistics, so knowing the odds changes nothing.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 1e18f),
                    new Resource(Units.Favor, 1000),
                },
                UnlockThreshold = new List<Resource>()
                {
                      new Resource(Units.Favor, 100),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ReduceFines }
            });

            Upgrades.Add(Upgrade.EUpgradeType.SellBuffsToOdds, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.SellBuffsToOdds,
                Name = "Sell Buffs for Loot Box Odds",
                Description = "Now the customers know the odds, we can exploit their desire to see bigger numbers.",
                CommentOnBuy = "Gambler's Fallacy is going to win Employee of the Month.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 2.5e18f),
                    new Resource(Units.Favor, 2000),
                },
                UnlockThreshold = new List<Resource>()
                {
                      new Resource(Units.Favor, 500),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.DiscloseOdds }
            });

            Upgrades.Add(Upgrade.EUpgradeType.RollBackBan, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.RollBackBan,
                Name = "Roll Back the Loot Box Ban",
                Description = "It'll take some doing, but we can get this ban overturned. Something about a free marketplace ought to work.",
                CommentOnBuy = "Now that we've got the government in our pocket, let's see what else we can get away with.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10e18f),
                    new Resource(Units.Favor, 5000),
                },
                UnlockThreshold = new List<Resource>()
                {
                      new Resource(Units.Favor, 1000),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.DiscloseOdds }
            });

            Upgrades.Add(Upgrade.EUpgradeType.UnlockBioEngineering, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.UnlockBioEngineering,
                Name = "Start up Bio-Engineering Division",
                Description = "There are still some holdouts around the world. We need to figure out how to bring them in.",
                CommentOnBuy = "A video game company starting a bio-engineering division should be cause for concern. Thankfully nobody is paying attention.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 200e18f),
                    new Resource(Units.Favor, 10e3f),
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 500e6f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.UnlockCPU }
            });

            Upgrades.Add(Upgrade.EUpgradeType.DetermineDesires, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.DetermineDesires,
                Name = "Determine Individual's Deepest Desires",
                Description = "Manipulate customers by understanding them at a fundamental level.",
                CommentOnBuy = "Same thing we've been doing all along. Just a little more direct.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 200e18f),
                    new Resource(Units.Favor, 15e3f),
                    new Resource(Units.Cycle, 100e12f),
                    new Resource(Units.GenomeData, 100)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.UnlockBioEngineering, Upgrade.EUpgradeType.TargetMinnows, Upgrade.EUpgradeType.TargetChildren, Upgrade.EUpgradeType.PurchaseBelovedStudio, Upgrade.EUpgradeType.CauseDataBreach, Upgrade.EUpgradeType.UseDataBreach }
            });

            Upgrades.Add(Upgrade.EUpgradeType.EmitInaudibleSound, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.EmitInaudibleSound,
                Name = "Emit an Inaudible Sound When Not Monetizing",
                Description = "If we can't make it more pleasant to buy loot boxes, we can certainly make it more unpleasant to <i>not</i> be buying them.",
                CommentOnBuy = "My dog is going nuts.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 600e18f),
                    new Resource(Units.Favor, 25e3f),
                    new Resource(Units.Cycle, 250e12f),
                    new Resource(Units.GenomeData, 250)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.DetermineDesires }
            });

            Upgrades.Add(Upgrade.EUpgradeType.IsolateMicrotransactionGene, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.IsolateMicrotransactionGene,
                Name = "Isolate Microtransaction Gene",
                Description = "Our engineers feel they can isolate what makes humans buy loot boxes at a genetic level.",
                CommentOnBuy = "This could prove useful.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 5e21f),
                    new Resource(Units.Cycle, 500e12f),
                    new Resource(Units.GenomeData, 500)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.EmitInaudibleSound }
            });

            Upgrades.Add(Upgrade.EUpgradeType.LaunchMeshNetwork, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.LaunchMeshNetwork,
                Name = "Launch Satellite Mesh Network",
                Description = "Launch a network of micro-satellites to cover the entire globe and signal the microtransaction receptors in the remaining unmonetized population.",
                CommentOnBuy = "No turning back from this one. But we have a duty to our shareholders.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10e21f),
                    new Resource(Units.Cycle, 1e15f),
                    new Resource(Units.GenomeData, 1000),
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.IsolateMicrotransactionGene }
            });


            Upgrades.Add(Upgrade.EUpgradeType.PurchaseMacGuffinQuestSourceCode, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.PurchaseMacGuffinQuestSourceCode,
                Name = "Modify <i>MacGuffin Quest 2</i> source code",
                Description = "I just need to flip a zero to a one and then I can finally play as MacGuffin.",
                CommentOnBuy = "Turning off the bots so I can play again. I hope I remember how.",
                Costs = new List<Resource>() {  },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 999e9f) //so that it doesn't unlock
                }
            });


            #endregion

            foreach (var upgrade in Upgrades.Values)
            {
                UpgradeStates.Add(upgrade.Type, Upgrade.EState.Hidden);
            }
        }

        public void Unlock(Upgrade.EUpgradeType type)
        {
            if (UpgradeStates[type] == Upgrade.EState.Hidden)
            {
                UpgradeStates[type] = Upgrade.EState.Visible;
                Debug.LogFormat("Upgrade {0} now available", Upgrades[type].Name);
            }
        }

        public bool IsPurchased(Upgrade.EUpgradeType type)
        {
            if (UpgradeStates.ContainsKey(type))
            {
                return UpgradeStates[type] == Upgrade.EState.Purchased;
            }
            else
            {
                return false;
            }
        }

        public bool CanAfford(Upgrade.EUpgradeType type)
        {
            bool canAfford = true;

            if (UpgradeStates[type] == Upgrade.EState.Visible)
            {
                foreach(var cost in Upgrades[type].Costs)
                {
                    if (Model.Resources[cost.Type].Amount < cost.Amount)
                    {
                        canAfford = false;
                        break;
                    }
                }
            }

            return canAfford;
        }

        public bool PurchaseUpgrade(Upgrade upgrade)
        {
            if (UpgradeStates[upgrade.Type] == Upgrade.EState.Visible)
            {
                if (Model.ConsumeExactly(upgrade.Costs))
                {
                    DoUpgrade(upgrade);
                    return true;
                }
            }
            else
            {
                Debug.LogErrorFormat("Tried to purchase upgrade {0} but state is {1}", upgrade.Name, UpgradeStates[upgrade.Type]);
            }

            return false;
        }

        protected void DoUpgrade(Upgrade upgrade)
        {
            Debug.LogFormat("Activated upgrade: {0}", upgrade.Name);
            UpgradeStates[upgrade.Type] = Upgrade.EState.Purchased;

            if (!string.IsNullOrEmpty(upgrade.CommentOnBuy))
            {
                Logger.Log(upgrade.CommentOnBuy);
            }

            switch(upgrade.Type)
            {
                case Upgrade.EUpgradeType.GetJob:
                    {
                        Model.Job.IsActive = true;
                    }
                    break;
                case Upgrade.EUpgradeType.PurchaseMacGuffinQuest:
                    {
                        Logger.Log("Sorry! <i>MacGuffin Quest 2</i> Regular Edition pre-orders are all sold out! We do still have Limited Editions available ironically.");

                        Logger.Log(5, "How is a pre-order out of stock anyway?");

                        Model.Add(Units.Money, MCGCost); //money back, since it wasn't actually purchased
                    }
                    break;
                case Upgrade.EUpgradeType.PurchaseBelovedStudio:
                    {
                        Logger.Log(3, "Oh. Guess not.");
                    }
                    break;
                case Upgrade.EUpgradeType.ReduceLootBoxOddsToZero:
                    {
                        Logger.Log(3, "<i>The Government has just declared loot boxes to be a form of gambling and thus subject to regulation.</i>");
                        Logger.Log(6, "Uh oh. We're going to be paying some serious fines until we can take care of this.");
                    }
                    break;
                case Upgrade.EUpgradeType.PurchaseMacGuffinQuestLimitedEdition:
                    {
                        Model.MacGuffinQuest.IsActive = true;
                    }
                    break;
                case Upgrade.EUpgradeType.UnlockInfluencerCareer:
                    {
                        Model.Influencer.IsActive = true;
                    }
                    break;

                case Upgrade.EUpgradeType.StartAStudio:
                    {
                        Model.Job.IsActive = false;
                        Model.Studio.IsActive = true;
                    }
                    break;
                case Upgrade.EUpgradeType.ExecuteIPO:
                    {
                        Model.Influencer.IsActive = false;
                        Model.Studio.IsActive = false;
                        Model.Public.IsActive = true;
                    }
                    break;
                case Upgrade.EUpgradeType.AutoGrinder:
                    {
                        Model.Add(Units.BotAccount, 1);
                    }
                    break;
                case Upgrade.EUpgradeType.PurchaseMacGuffinQuestSourceCode:
                    {
                        Model.Add(Units.MacGuffinUnlocked, 1);

                        Game.Instance.OnMacGuffinQuestSourcePurchase();
                    }
                    break;
            }
        }

        public void Tick()
        {
            foreach(var upgrade in Upgrades.Values)
            {
                if (UpgradeStates[upgrade.Type] == Upgrade.EState.Hidden)
                {
                    if (IsUnlockThresholdMet(upgrade))
                    {
                        Unlock(upgrade.Type);

                        if (!string.IsNullOrEmpty(upgrade.MessageOnUnlock))
                        {
                            Logger.Log(upgrade.MessageOnUnlock);
                        }
                    }
                }
            }
        }

        protected bool IsUnlockThresholdMet(Upgrade upgrade)
        {
            bool unlockThresholdMet = true;

            if (unlockThresholdMet)
            {
                //Check pre-req upgrades
                for (int i = 0; i < upgrade.Requirements.Count; i++)
                {
                    if (!IsPurchased(upgrade.Requirements[i]))
                    {
                        unlockThresholdMet = false;
                        break;
                    }
                }
            }

            if (unlockThresholdMet)
            {
                //Check resources threshold
                for (int i = 0; i < upgrade.UnlockThreshold.Count; i++)
                {
                    Resource resource = upgrade.UnlockThreshold[i];

                    if (Model.Resources[resource.Type].Amount < resource.Amount)
                    {
                        unlockThresholdMet = false;
                        break;
                    }
                }
            }

            return unlockThresholdMet;
        }
    }
}
