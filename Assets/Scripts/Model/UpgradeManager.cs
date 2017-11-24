using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UpgradeManager
    {
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
                Name = "Learn to code",
                Description = "Looks like computers might be sticking around for a while. I should learn to talk to them.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.TotalMoneyEarned, 100)
                }
            });

            #endregion


            #region Job upgrades

            Upgrades.Add(Upgrade.EUpgradeType.WorkSmarter, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.WorkSmarter,
                Name = "Buy a book on life hacks",
                Description = "Work smarter not harder! Get more work done with every click.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
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
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.JobCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.JobAutomationScript, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.JobAutomationScript,
                Name = "Write Script to Automate Job",
                Description = "It's going to happen anyway. Might as well do it myself while the paycheck still goes to me.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.LearnToCode }
            });

            Upgrades.Add(Upgrade.EUpgradeType.SecondJob, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.SecondJob,
                Name = "Get a second job",
                Description = "I work from home anyway. Who's going to notice?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.JobCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.FasterComputer, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.FasterComputer,
                Name = "Buy a better computer",
                Description = "Invest in better hardware since it's the one doing all the work anyway.",
                CommentOnBuy = "Gotta have the fastest rig for these spreadsheets!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.JobCompleted, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.JobAutomationScript }
            });

            Upgrades.Add(Upgrade.EUpgradeType.WatercooledComputer, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.WatercooledComputer,
                Name = "Get Water-cooling and overclock your rig",
                Description = "Hardly seems worth it for the nominal increase in speed but who am I to judge?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.FasterComputer }
            });

            #endregion


            #region MacGuffinQuest upgrades

            Upgrades.Add(Upgrade.EUpgradeType.PurchaseMacGuffinQuest, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.PurchaseMacGuffinQuest,
                Name = "Pre-Order MacGuffin Quest",
                Description = "Holy crap I can't wait.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, MCGCost)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.PurchaseMacGuffinQuestLimitedEdition, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.PurchaseMacGuffinQuestLimitedEdition,
                Name = "Buy MacGuffin Quest Deluxe Limited Edition",
                Description = "Well it's more expensive but at least it's in stock.",
                CommentOnBuy = "YESSSSS! Finally! Time to play MacGuffin Quest 2!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 80)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.PurchaseMacGuffinQuest }
            });

            Upgrades.Add(Upgrade.EUpgradeType.RemoveGameAnimations, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.RemoveGameAnimations,
                Name = "Hack MacGuffin Quest and remove all animations",
                Description = "Use your newfound computer talents to remove those pesky animations slowing things down. Improves grinding efficiency.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.GrindCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.SecondMouse, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.SecondMouse,
                Name = "Buy a second mouse",
                Description = "If I use both hands, I can grind twice as fast! I mean, have twice as much fun.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
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
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.GrindCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AutoGrinder, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AutoGrinder,
                Name = "Write Auto-Grind Script",
                Description = "Use your newfound scripting talents to have your computer play MacGuffin Quest for you! Automatic fun!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.GrindCompleted, 3)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AutoSellTrashItems, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AutoSellTrashItems,
                Name = "Write Auto-Auction Script",
                Description = "Selling items on the auction house feels like work and 'round here? We automate work. Gets the job done but doesn't get as good a return.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.TrashItemSold, 3)
                }
            });


            #endregion






            #region Influencer Upgrades

            Upgrades.Add(Upgrade.EUpgradeType.UnlockInfluencerCareer, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.UnlockInfluencerCareer,
                Name = "Buy a Capture Card",
                Description = "Hey I bet people would watch me opening all these loot boxes.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.LootBoxOpened, 5)
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
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Follower, 100)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.BuyVideoEditingSoftware, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.BuyVideoEditingSoftware,
                Name = "Buy Video Editing Software",
                Description = "The software that came free with my cereal is lacking a few features. Reduce the work needed to produce videos.",
                CommentOnBuy = "Whoa! This video editing program supports using a mouse! This'll make things a lot easier.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 2)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.MakeVideoIntro, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.MakeVideoIntro,
                Name = "Make re-useable video intros",
                Description = "More re-usable footage means less effort to make videos.",
                CommentOnBuy = "Now I don't have to say \"What's up guys!\" all the time.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 5)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.BuyVideoEditingSoftware }
            });

            Upgrades.Add(Upgrade.EUpgradeType.HireVideoEditor, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.HireVideoEditor,
                Name = "Hire Video Editor",
                Description = "Hire an editor to convert your raw footage into compelling content. Your editor assures you they're worth every penny, given what they have to work with.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.GetPartnered }
            });

            Upgrades.Add(Upgrade.EUpgradeType.StartStreaming, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.StartStreaming,
                Name = "Start live-streaming",
                Description = "Live-stream the unboxings before you turn them into videos. More opportunities to gain followers!",
                CommentOnBuy = "I wonder what this will do to my sleep schedule...",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.GetPartnered }
            });

            Upgrades.Add(Upgrade.EUpgradeType.DoSponsoredVideos, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.DoSponsoredVideos,
                Name = "Do Sponsored Videos",
                Description = "Get more revenue per video. ",
                CommentOnBuy = "People <i>need</i> mattresses. Why shouldn't I get paid to give my opinion?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Follower, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.GetPartnered }
            });

            Upgrades.Add(Upgrade.EUpgradeType.CompletelySellOut, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.CompletelySellOut,
                Name = "Completely Sell Out",
                Description = "Sponsor anything and everything.",
                CommentOnBuy = "Okay, maybe not <i>everybody</i> needs novelty glasses, but I need more money for loot boxes. Feed the beast!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Follower, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.DoSponsoredVideos }
            });

            Upgrades.Add(Upgrade.EUpgradeType.ChannelGrowthAnalytics, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ChannelGrowthAnalytics,
                Name = "Track Channel Growth",
                Description = "Do some analysis and figure out where all these followers are coming from.",
                CommentOnBuy = "",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.PublishedVideo, 10)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.OptimizeContentForChannelGrowth, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.OptimizeContentForChannelGrowth,
                Name = "Optimize Video Content for Channel Growth",
                Description = "Perform exhaustive research and experimentation to determine the best ways to drive engagement to your channel.",
                CommentOnBuy = "Smash that Like button!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                Name = "Start a studio",
                Description = "You know, I could probably do this myself.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.Follower, 10)
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
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ReleasedGame, 1)
                },
            });

            Upgrades.Add(Upgrade.EUpgradeType.EnableMicrotransactions, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.EnableMicrotransactions,
                Name = "Add Microtransactions",
                Description = "Just for cosmetic items.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.EnableAnalytics }
            });


            Upgrades.Add(Upgrade.EUpgradeType.StartDistributionService, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.StartDistributionService,
                Name = "Start our own Digital Distribution Services",
                Description = "Cut out the middle man and the middle man's cut is ours! It'll need a catchy name, like Game Shovel, only not so stupid.",
                CommentOnBuy = "Happy Game Shovel launch day everyone!",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.SellLimitedEditions }
            });


            Upgrades.Add(Upgrade.EUpgradeType.EliminateUnderperformingFranchises, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.EliminateUnderperformingFranchises,
                Name = "Eliminate Under-performing Franchises",
                Description = "Reduce development cost of releasing new games.",
                CommentOnBuy = "If it doesn't have the potential to be exploited every year, I don't want to hear about it.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ReleasedGame, 3)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.MarketingCampaign }
            });

            Upgrades.Add(Upgrade.EUpgradeType.MarketingCampaign, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.MarketingCampaign,
                Name = "Run Marketing Campaigns",
                Description = "More people need to know about our game! Increases the number of sales when releasing a game.",
                CommentOnBuy = "I bet we could get a celebrity to endorse our game. Actually, that might not be a good idea.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ReleasedGame, 1)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.LootBoxPreOrders, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.LootBoxPreOrders,
                Name = "Sell Pre-Orders For Loot Boxes",
                Description = "Why are we waiting until release to monetize players? Increases the number of sales when releasing a game.",
                CommentOnBuy = "Pre-orders for plays on a virtual slot machine. I didn't think people would buy it, but here we are.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ReleasedGame, 1)
                },
                Requirements = new List<Upgrade.EUpgradeType>() {  Upgrade.EUpgradeType.EnableMicrotransactions, Upgrade.EUpgradeType.MarketingCampaign }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddGems, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddGems,
                Name = "Add Premium Currency: Gems",
                Description = "Players will just <i>love</i> buying these shiny gems! Increases microtransaction spend.",
                CommentOnBuy = "Not sure gems really fit in our games thematically, but I suppose it's an abstract concept.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AddSeasonalBoxes }
            });

            Upgrades.Add(Upgrade.EUpgradeType.AddSkinnerBoxes, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.AddSkinnerBoxes,
                Name = "Add Skinner Boxes",
                Description = "No sense pretending anymore.",
                CommentOnBuy = "They're surprisingly effective, even when players know what they are.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AddLuteBoxes }
            });

            Upgrades.Add(Upgrade.EUpgradeType.BuffsInBoxes, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.BuffsInBoxes,
                Name = "Put Game Altering Buffs in Loot Boxes",
                Description = "It's not pay to win. But paying does make winning a heck of a lot easier.",
                CommentOnBuy = "When did video games become a cruel reflection of society?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.AddGoldBoxes }
            });

            Upgrades.Add(Upgrade.EUpgradeType.CoreGameInBoxes, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.CoreGameInBoxes,
                Name = "Put Core Game Elements in Loot Boxes",
                Description = "Games these days are like slot machines. Nice to look at, but they don't do much until you pull the arm.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
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
                    new Resource(Units.Money, 10)
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
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.Layoffs, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.Layoffs,
                Name = "Regularly Scheduled Layoffs",
                Description = "The end of a project doesn't <i>have</i> to mean the end of a job. But we'll save a bundle if it does!",
                CommentOnBuy = "And yet for some reason this industry is considered in demand.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.OperatingCost, 10)
                }
            });

            Upgrades.Add(Upgrade.EUpgradeType.ContractEmployees, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ContractEmployees,
                Name = "Use Contract Employees instead of Full Time",
                Description = "Less pay and benefits means more bottom line for us. And it's somehow legal!",
                CommentOnBuy = "It's as if we automated the layoff process.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.OperatingCost, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.Layoffs }
            });

            Upgrades.Add(Upgrade.EUpgradeType.PurchaseBelovedStudio, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.PurchaseBelovedStudio,
                Name = "Purchase Beloved Independent Studio",
                Description = "We've saturated our current customer base. Time to acquire new fans by acquiring a studio, hollowing it out, and discarding the husk that remains.",
                CommentOnBuy = "Well, as long as we treat the source material with love and respect.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ExecuteIPO }
            });

            Upgrades.Add(Upgrade.EUpgradeType.TargetMinnows, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.TargetMinnows,
                Name = "Target the Minnows",
                Description = "Once all the whales have been hunted, it only makes sense to go after the smaller fish.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.PurchaseBelovedStudio }
            });

            Upgrades.Add(Upgrade.EUpgradeType.TargetChildren, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.TargetChildren,
                Name = "Incentivize Diminished-Age Purchasers",
                Description = "Target children with microtransactions. They play a lot and don't know the value of money yet.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
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
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.PurchaseBelovedStudio }
            });


            Upgrades.Add(Upgrade.EUpgradeType.CauseDataBreach, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.CauseDataBreach,
                Name = "Arrange a Security Breach",
                Description = "That last security breach turned out really well for us. It'd be a shame if all our competitors experienced similar incidents.",
                CommentOnBuy = "Security breaches are inevitable. We're just giving the process a little boost.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
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
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.ExecuteIPO }
            });

            Upgrades.Add(Upgrade.EUpgradeType.UnlockCPU, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.UnlockCPU,
                Name = "Start up Machine Learning Division",
                Description = "Nobody seems quite sure what machine learning can be used for, but if it can make us more money then I want it.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.UnlockLobbying }
            });

            Upgrades.Add(Upgrade.EUpgradeType.ComputersMaintainGame, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.ComputersMaintainGame,
                Name = "Teach Machines to Maintain the Game",
                Description = "Remove the remaining operating costs by putting machines in charge of running the servers.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10),
                    new Resource(Units.Cycle, 10),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.UnlockCPU }
            });

            Upgrades.Add(Upgrade.EUpgradeType.OptimizeRoAS, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.OptimizeRoAS,
                Name = "Optimize Return on Ad Spend",
                Description = "Artificial Intelligence Marketing platform will significantly reduce churn and continuously optimize integrated player experiences based on the entire player data ecosystem.",
                CommentOnBuy = "Wait, what exactly does this do?",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10),
                    new Resource(Units.Cycle, 10),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.UnlockCPU }
            });

            Upgrades.Add(Upgrade.EUpgradeType.TargetedPersonalAds, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.TargetedPersonalAds,
                Name = "Automatic Targeted Ad Personalization",
                Description = "By running millions of variations of slogans and creatives automatically, we can acquire users much more effectively.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10),
                    new Resource(Units.Cycle, 10),
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.OptimizeRoAS }
            });

            Upgrades.Add(Upgrade.EUpgradeType.UnlockBioEngineering, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.UnlockBioEngineering,
                Name = "Start up Bio-Engineering Division",
                Description = "There are still some holdouts around the world. We need to figure out how to bring them in.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.UnlockCPU }
            });

            Upgrades.Add(Upgrade.EUpgradeType.DetermineDesires, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.DetermineDesires,
                Name = "Determine Individual's Deepest Desires",
                Description = "Manipulate customers by understanding them at a fundamental level.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10),
                    new Resource(Units.GenomeData, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.UnlockBioEngineering, Upgrade.EUpgradeType.TargetMinnows, Upgrade.EUpgradeType.TargetChildren, Upgrade.EUpgradeType.PurchaseBelovedStudio, Upgrade.EUpgradeType.CauseDataBreach, Upgrade.EUpgradeType.UseDataBreach }
            });


            Upgrades.Add(Upgrade.EUpgradeType.IsolateMicrotransactionGene, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.IsolateMicrotransactionGene,
                Name = "Isolate Microtransaction Gene",
                Description = "Our engineers feel they can isolate what makes humans buy loot boxes at a genetic level. This could prove useful.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10),
                    new Resource(Units.GenomeData, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 10)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.DetermineDesires }
            });

            Upgrades.Add(Upgrade.EUpgradeType.LaunchMeshNetwork, new Upgrade()
            {
                Type = Upgrade.EUpgradeType.LaunchMeshNetwork,
                Name = "Launch Satellite Mesh Network",
                Description = "Launch a network of micro-satellites to cover the entire globe and signal the microtransaction receptors in the remaining unmonetized population.",
                CommentOnBuy = "No turning back from this one. But we have a duty to our shareholders.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10),
                    new Resource(Units.GenomeData, 10),
                    new Resource(Units.Cycle, 10)
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
                Name = "Purchase MacGuffin Quest source code",
                Description = "I just need to flip a zero to a one and then I can finally play as MacGuffin.",
                Costs = new List<Resource>()
                {
                    new Resource(Units.Money, 10)
                },
                UnlockThreshold = new List<Resource>()
                {
                    new Resource(Units.ActivePlayer, 7e9f)
                },
                Requirements = new List<Upgrade.EUpgradeType>() { Upgrade.EUpgradeType.LaunchMeshNetwork }
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

        public bool IsActive(Upgrade.EUpgradeType type)
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
                case Upgrade.EUpgradeType.PurchaseMacGuffinQuest:
                    {
                        Logger.Log("Sorry! MacGuffin Quest Regular Edition pre-orders are all sold out! We do still have Limited Editions available ironically.");

                        Logger.Log(5, "How is a pre-order out of stock anyway?");

                        Model.Add(Units.Money, MCGCost); //money back, since it wasn't actually purchased
                    }
                    break;
                case Upgrade.EUpgradeType.PurchaseBelovedStudio:
                    {
                        Logger.Log(3, "Oh. Guess not.");
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
                        Debug.Log("Game over!");
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
                    if (!IsActive(upgrade.Requirements[i]))
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
