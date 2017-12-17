using Assets.Scripts.Model;
using Facepunch.Steamworks;
using UnityEngine;

public class SteamManager : MonoBehaviour
{
    public static SteamManager Instance;

    public static Client Client;

    public static readonly string ACH_CLICK_250     = "ACH_CLICK_250";
    public static readonly string ACH_CLICK_1000    = "ACH_CLICK_1000";
    public static readonly string ACH_CLICK_5000    = "ACH_CLICK_5000";

    public static readonly string ACH_EARN_1K       = "ACH_EARN_1K";
    public static readonly string ACH_EARN_1M = "ACH_EARN_1M";
    public static readonly string ACH_EARN_CANADA = "ACH_EARN_CANADA";

    public static readonly string ACH_FOLLOWERS_1000 = "ACH_FOLLOWERS_1000";
    public static readonly string ACH_FOLLOWERS_100K = "ACH_FOLLOWERS_100K";
    public static readonly string ACH_FOLLOWERS_1M = "ACH_FOLLOWERS_1M";

    public static readonly string ACH_GAMES_10 = "ACH_GAMES_10";
    public static readonly string ACH_GAMES_25 = "ACH_GAMES_25";
    public static readonly string ACH_GAMES_50 = "ACH_GAMES_50";

    public static readonly string ACH_QUITTERS = "ACH_QUITTERS";

    public static readonly string ACH_PRIDE = "ACH_PRIDE";
    public static readonly string ACH_ACCOMPLISHMENT = "ACH_ACCOMPLISHMENT";

    private void Awake()
    {
        if (Instance != null) { Debug.LogError("Tried to instantiate a second SteamManager!"); }
        Instance = this;
    }

    void OnEnable()
    {
        Config.ForUnity(Application.platform.ToString());
        Client = new Facepunch.Steamworks.Client(758500);

        if (Client.IsValid)
        {
            Debug.LogFormat("Successfully started Steam service. Username: {0} ID: {1}", Client.Username, Client.SteamId);
        }
        else
        { 
            Client.Dispose();
            Debug.Log("Unable to start Steam service.");
        }
    }


    public void UnlockAchievement(string id)
    {
        if (Client.IsValid)
        {
            Achievement ach = Client.Achievements.Find(id);

            if (ach != null && !ach.State)
            {
                Client.Achievements.Trigger(id);
            }
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Reset Achievements")]
    public void ResetAchievements()
    {
        foreach(var ach in Client.Achievements.All)
        {
            ach.Reset();
        }
    }
#endif

    public void CheckAchievements()
    {
        Debug.Log("Checking achievements...");

        if (!Client.IsValid)
        {
            Debug.Log("Steam connection is not valid at the moment. Achievement unlock is not possible. Are you logged in to Steam?");
        }

        LootBoxModel model = LootBoxModel.Instance;

        //Money
        if (model.Resources[Units.TotalMoneyEarned].Amount >= 1000)
        {
            Debug.LogFormat("TotalMoneyEarned: {0} >= 1000. Awarding ACH_EARN_1K", model.Resources[Units.TotalMoneyEarned].Amount);
            UnlockAchievement(ACH_EARN_1K);
        }

        if (model.Resources[Units.TotalMoneyEarned].Amount >= 1000000)
        {
            Debug.LogFormat("TotalMoneyEarned: {0} >= 1000000. Awarding ACH_EARN_1M", model.Resources[Units.TotalMoneyEarned].Amount);
            UnlockAchievement(ACH_EARN_1M);
        }

        if (model.Resources[Units.TotalMoneyEarned].Amount >= 1.3e12f)
        {
            Debug.LogFormat("TotalMoneyEarned: {0} >= 1.3e12f. Awarding ACH_EARN_CANADA", model.Resources[Units.TotalMoneyEarned].Amount);
            UnlockAchievement(ACH_EARN_CANADA);
        }


        //Clicks
        if (model.Resources[Units.Click].Amount >= 250)
        {
            Debug.LogFormat("Clicks: {0} >= 250. Awarding ACH_CLICK_250", model.Resources[Units.Click].Amount);
            UnlockAchievement(ACH_CLICK_250);
        }

        if (model.Resources[Units.Click].Amount >= 1000)
        {
            Debug.LogFormat("Clicks: {0} >= 1000. Awarding ACH_CLICK_1000", model.Resources[Units.Click].Amount);
            UnlockAchievement(ACH_CLICK_1000);
        }

        if (model.Resources[Units.Click].Amount >= 5000)
        {
            Debug.LogFormat("Clicks: {0} >= 5000. Awarding ACH_CLICK_5000", model.Resources[Units.Click].Amount);
            UnlockAchievement(ACH_CLICK_5000);
        }


        //Followers
        if (model.Resources[Units.Follower].Amount >= 1000)
        {
            Debug.LogFormat("Followers: {0} >= 1000. Awarding ACH_FOLLOWERS_1000", model.Resources[Units.Follower].Amount);
            UnlockAchievement(ACH_FOLLOWERS_1000);
        }

        if (model.Resources[Units.Follower].Amount >= 100000)
        {
            Debug.LogFormat("Followers: {0} >= 100000. Awarding ACH_FOLLOWERS_100K", model.Resources[Units.Follower].Amount);
            UnlockAchievement(ACH_FOLLOWERS_100K);
        }

        if (model.Resources[Units.Follower].Amount >= 1000000)
        {
            Debug.LogFormat("Followers: {0} >= 1000000. Awarding ACH_FOLLOWERS_1M", model.Resources[Units.Follower].Amount);
            UnlockAchievement(ACH_FOLLOWERS_1M);
        }


        //Games
        if (model.Resources[Units.ReleasedGame].Amount >= 10)
        {
            Debug.LogFormat("ReleasedGames: {0} >= 10. Awarding ACH_GAMES_10", model.Resources[Units.ReleasedGame].Amount);
            UnlockAchievement(ACH_GAMES_10);
        }

        if (model.Resources[Units.ReleasedGame].Amount >= 25)
        {
            Debug.LogFormat("ReleasedGames: {0} >= 25. Awarding ACH_GAMES_25", model.Resources[Units.ReleasedGame].Amount);
            UnlockAchievement(ACH_GAMES_25);
        }

        if (model.Resources[Units.ReleasedGame].Amount >= 50)
        {
            Debug.LogFormat("ReleasedGames: {0} >= 50. Awarding ACH_GAMES_50", model.Resources[Units.ReleasedGame].Amount);
            UnlockAchievement(ACH_GAMES_50);
        }

        //Quitters
        if (model.Studio.DidDevsQuit)
        {
            Debug.Log("DevsQuit: true. Awarding ACH_QUITTERS");
            UnlockAchievement(ACH_QUITTERS);
        }

        //Pride
        model.UpgradeManager.CheckForAllUpgradeAchievement();

        //Accomplishment
        if (model.UpgradeManager.IsPurchased(Upgrade.EUpgradeType.PurchaseMacGuffinQuestSourceCode))
        {
            UnlockAchievement(ACH_ACCOMPLISHMENT);
        }

        Debug.Log("Achievement check complete.");
    }

    void OnDisable()
    {
        if (Client != null)
        {
            if (Client.IsValid)
            {
                Client.Music.Pause();
            }

            Client.Dispose();
            Client = null;
        }
    }
}

