using UnityEngine;

/// <summary>
/// Handles all initialization
/// </summary>
public class LoadingManager : MonoBehaviour
{
    private void Start()
    {
        Time.fixedDeltaTime = 1 / 30f;
    }
}
