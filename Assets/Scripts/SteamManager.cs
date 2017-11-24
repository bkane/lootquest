using Facepunch.Steamworks;
using UnityEngine;

public class SteamManager : MonoBehaviour
{
    public static SteamManager Instance;

    public static Client Client;

    private void Awake()
    {
        if (Instance != null) { Debug.LogError("Tried to instantiate a second SteamManager!"); }
        Instance = this;
    }

    void OnEnable()
    {
        Config.ForUnity(Application.platform.ToString());
        Client = new Facepunch.Steamworks.Client(220); // 220 = half-life 2 appid

        if (!Client.IsValid)
        {
            Client.Dispose();
            throw new System.Exception("Couldn't init Steam - is Steam running? Do you own Half-Life 2? Is steam_appid.txt in your project folder?");
        }

        Debug.Log("Hello " + Client.Username);
        Debug.Log("Your SteamID is " + Client.SteamId);

    }

    void OnDisable()
    {
        if (Client != null)
        {
            Client.Dispose();
            Client = null;
        }
    }
}

