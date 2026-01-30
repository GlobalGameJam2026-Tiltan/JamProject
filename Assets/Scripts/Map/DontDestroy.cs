using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        // This makes the GameObject this script is attached to persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }
}
