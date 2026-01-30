using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required namespace

public class GameManager : MonoBehaviour
{

    
    private Dictionary<string, PlanetState> planets = new Dictionary<string, PlanetState>(){
        {"Bob1",  PlanetState.Open},
        {"Bob2",  PlanetState.Locked},
        {"Bob3",  PlanetState.Locked},
        {"Bob4",  PlanetState.Locked},
        {"Bob5",  PlanetState.Locked},
        {"Bob6",  PlanetState.Locked},
        {"Bob7",  PlanetState.Locked},
        {"Bob8",  PlanetState.Locked},
        {"Bob9",  PlanetState.Locked},
        {"Bob10", PlanetState.Locked},
        {"Bob11", PlanetState.Locked},
    };
    
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
    
    // public void ChangeToOpen()
    // {
    //     ChangeToState(PlanetState.Open);
    // }
    //
    // public void ChangeToClosed()
    // {
    //     ChangeToState(PlanetState.Locked);
    // }
    //
    // public void ChangeToDefeated()
    // {
    //     ChangeToState(PlanetState.Defeated);
    // }
    
    public PlanetState GetPlanetState(string planetName)
    {
        return planets[planetName];
    }
    
    // Public function to load a scene by name
    public void SwitchToLevel(string sceneName)
    {
        SceneFader.instance.LoadSceneWithFade(sceneName);
    }
}
