using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class Util
{
    public static IEnumerator DelayCall(float delayInSeconds, Action onComplete)
    {
        yield return new WaitForSeconds(delayInSeconds);
        onComplete();
    }

    private static string SAVE_FILENAME = "save.json";

    public static void Save()
    {
        LootBoxModel model = LootBoxModel.Instance;

        Debug.LogFormat("{0} Saving game.", DateTime.Now.ToLocalTime().ToString());

        try
        {
            var settings = JsonConvert.DefaultSettings();
            
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            string json = JsonConvert.SerializeObject(model, settings);

            File.WriteAllText(Path.Combine(Application.persistentDataPath, SAVE_FILENAME), json);

            Debug.LogFormat("{0} Saving successful.", DateTime.Now.ToLocalTime().ToString());
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("Save failed: {0}", e.Message);
        }
    }

    public static bool Load()
    {

        Debug.LogFormat("{0} Loading game.", DateTime.Now.ToLocalTime().ToString());

        try
        {
            string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, SAVE_FILENAME));

            JsonConvert.PopulateObject(json, LootBoxModel.Instance);

            Logger.Instance.Clear();

            Debug.LogFormat("{0} Load successful.", DateTime.Now.ToLocalTime().ToString());

            return true;
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("Load failed: {0}", e.Message);
            return false;
        }
    }
}

