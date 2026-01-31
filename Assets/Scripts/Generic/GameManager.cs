using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections; // Required for IEnumerator

public class GameManager : MonoBehaviour
{
    public List<PlanetData> planets;
    private AudioSource _audioSource;
    private PlanetData _currentPlanet;

    
    void Awake()
    {
        // This makes the GameObject this script is attached to persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void PlanetDefeated()
    {
        _currentPlanet.ChangeState(PlanetState.Defeated);
        
        // Unlocking planets who depended on him
        foreach (var planet in planets.Where(planet => planet.previousPlanet == _currentPlanet.planetName))
        {
            planet.ChangeState(PlanetState.Open);
        }
        
        SceneFader.instance.LoadSceneWithFade("Map");
    }

    public void ResetGame()
    {
        foreach (var planet in planets.Where(planet => planet.previousPlanet == _currentPlanet.planetName))
        {
            planet.ChangeState(PlanetState.Locked);
        }
        GetPlanet("StartPlanet").ChangeState(PlanetState.Open);
        GetPlanet("El Gigolo De Goma").ChangeState(PlanetState.Open);
    }

    public bool IsAllPlanetsDefeated()
    {
        return planets.All(planet => planet.state == PlanetState.Defeated);
    }
    
    // Public function to load a scene by name
    public void MoveToPlanet(Planet newPlanet)
    {
        _currentPlanet = newPlanet.planet;
        EncounterManager.Instance.SetEncounter(newPlanet.encounterType);
    }
    
    public void PlayMusic()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }
  
    public void StopMusic()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
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
