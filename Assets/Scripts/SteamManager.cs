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

