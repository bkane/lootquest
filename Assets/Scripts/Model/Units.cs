using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Assets.Scripts.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Units
    {
        Invalid,

        //Life
        Click,

        //Job
        JobProgress,
        JobCompleted,
        Money,
        TotalMoneyEarned,

        //MacGuffin Quest
        GrindProgress,
        GrindCompleted,
        LootBox,
        TotalLootBoxes,
        LootBoxOpened,
        TrashItem,
        TrashItemSold,
        BotAccount,
        MacGuffinUnlocked,


        //Influencer Career
        VideoContent,
        Follower,
        VideoProgress,
        PublishedVideo,


        //Studio
        Developer,
        DevHour,
        DataAnalyst,
        AnalyticsData,
        TotalAnalyticsData,
        ReleasedGame,
        Hype,
        CopySold,
        Microtransaction,
        ActivePlayer,


        //Public
        LootBoxType,
        Marketer,
        Lobbyist,
        Favor,
        CPU,
        Cycle,
        Bioengineer,
        GenomeData,
        OperatingCost
    }
}
