using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class Logger : MonoBehaviour
{
    public static Logger Instance;

    public TextMeshProUGUI LogText;

    public Queue<string> Messages = new Queue<string>();

    void Awake()
    {
        if (Instance != null) { Debug.LogError("Tried to instantiate a second logger!"); }
        Instance = this;
        Clear();
    }

    public void Clear()
    {
        LogText.text = string.Empty;
        Messages = new Queue<string>();
    }

    public static void Log(string message)
    {
        Debug.LogFormat("Logger: \"{0}\"", message);

        if (Instance.Messages.Count() >= 5)
        {
            Instance.Messages.Dequeue();
        }

        Instance.Messages.Enqueue(message);

        Instance.LogText.text = string.Join("\n", Instance.Messages.Reverse().ToArray());
    }

    public static void Log(float delay, string message)
    {
        Instance.StartCoroutine(Util.DelayCall(delay, () => { Log(message); }));
    }
}