using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Assets.Scripts.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Units
    {
        Invalid,

        //Life

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
        ReleasedGame,
        Hype,
        CopySold,
        Microtransaction,
        ActivePlayer,


        //Public
        Customer,
        CustomerData,
        Marketer,
        Lobbyist,
        Favor,
        CPU,
        Cycle,
        Bioengineer,
        GenomeData
    }
}
