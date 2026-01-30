using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.UI; // Make sure you include this

public class MapLevel : MonoBehaviour
{
    public PlanetData planet;
    
    public SceneAsset level;

    private GameManager gameManager;
    
    
    void Awake()
    {
        // This makes the GameObject this script is attached to persist across scenes
        DontDestroyOnLoad(this.gameObject);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        // Match sprite to state
        this.planet.currentSprite = this.planet.state switch
        {
            PlanetState.Open => this.planet.baseSprite,
            PlanetState.Locked => this.planet.lockedSprite,
            PlanetState.Defeated => this.planet.defeatedSprite,
            _ => this.planet.currentSprite
        };
        // Change current sprite to correct one
        gameObject.GetComponent<Image>().sprite = this.planet.currentSprite;
    }

    public void GoToLevel()
    {
        if (this.planet.state == PlanetState.Open)
        {
            this.gameManager.SwitchToLevel(this.level.name.ToString());
        }
    }
}
