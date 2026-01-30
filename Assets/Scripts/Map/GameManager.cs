using UnityEngine;
using UnityEngine.SceneManagement; // Required namespace

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        // This makes the GameObject this script is attached to persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Public function to load a scene by name
    public void SwitchToLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
