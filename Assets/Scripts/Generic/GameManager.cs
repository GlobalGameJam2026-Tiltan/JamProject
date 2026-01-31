using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections; // Required for IEnumerator

public class GameManager : MonoBehaviour
{
    public List<PlanetData> planets;
    private AudioSource audioSource;

    
    void Awake()
    {
        // This makes the GameObject this script is attached to persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlanetDefeated(string name)
    {
        // Changing state to defeated
        GetPlanet(name).ChangeState(PlanetState.Defeated);
        
        // Unlocking planets who depended on him
        foreach (var planet in planets.Where(planet => planet.previousPlanet == name))
        {
            planet.ChangeState(PlanetState.Open);
        }
    }

    public bool IsAllPlanetsDefeated()
    {
        return planets.All(planet => planet.state == PlanetState.Defeated);
    }
    
    // Public function to load a scene by name
    public void SwitchToLevel(string sceneName)
    {
        SceneFader.instance.LoadSceneWithFade(sceneName);
    }
    
    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
  
    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    
    private PlanetData GetPlanet(string name)
    {
        foreach (var planet in planets.Where(planet => planet.name == name))
        {
            return planet;
        }

        return null;
    }
}
