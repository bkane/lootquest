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

    public static string SAVE_FILENAME = "save.json";
    public static string OPTIONS_FILENAME = "settings.json";

    public static void Save(string filename, object obj)
    {
        Debug.LogFormat("{0} Saving {1}", DateTime.Now.ToLocalTime().ToString(), filename);

        try
        {
            var settings = JsonConvert.DefaultSettings();
            
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            string json = JsonConvert.SerializeObject(obj, settings);

            File.WriteAllText(Path.Combine(Application.persistentDataPath, filename), json);

            Debug.LogFormat("{0} Save {1} successful.", DateTime.Now.ToLocalTime().ToString(), filename);
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("Save {0} failed: {1}", filename, e.Message);
        }
    }

    public static bool LoadIntoObject(string filename, object obj)
    {

        Debug.LogFormat("{0} Loading game.", DateTime.Now.ToLocalTime().ToString());

        try
        {
            string json = File.ReadAllText(Path.Combine(Application.persistentDataPath, filename));

            JsonConvert.PopulateObject(json, obj);

            Logger.Instance.Clear();

            Debug.LogFormat("{0} Load {1} successful.", DateTime.Now.ToLocalTime().ToString(), filename);

            return true;
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("Load {0} failed: {1}", filename, e.Message);
            return false;
        }
    }
}

