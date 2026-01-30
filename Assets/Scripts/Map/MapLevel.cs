using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.UI; // Make sure you include this

[Serializable]
public class LevelSprite
{
    public Sprite open;
    public Sprite locked;
    public Sprite defeated;
}

public class MapLevel : MonoBehaviour
{

    public string name;
    public string previousPlanetName;

    public PlanetState startingState;
    
    public LevelSprite sprites;

    public SceneAsset level;
    
    private PlanetState currentState;

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
        this.currentState = gameManager.GetPlanetState(this.name);
        this.ChangeToState(this.currentState);
    }
    
    public void GoToLevel()
    {
        if (this.currentState == PlanetState.Open)
        {
            this.gameManager.SwitchToLevel(this.level.name.ToString());
        }
    }

    private void ChangeToState(PlanetState state)
    {
        this.currentState = state;
        gameObject.GetComponent<Image>().sprite = state switch
        {
            PlanetState.Open => this.sprites.open,
            PlanetState.Locked => this.sprites.locked,
            PlanetState.Defeated => this.sprites.defeated
        };
    }
}
