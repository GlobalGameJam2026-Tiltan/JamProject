using UnityEngine;
using System;
using UnityEditor; // Make sure you include this

[Serializable]
public class LevelTexture
{
    public Texture2D open;
    public Texture2D closed;
    public Texture2D defeated;
}

public class MapLevel : MonoBehaviour
{

    public enum State
    {
        Open,
        Closed,
        Defeated
    }

    public State startingState;
    
    public LevelTexture levelTexture;

    public SceneAsset level;
    
    private Renderer renderer;
    
    private State currentState = State.Closed;

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
        
        // Get the Renderer component and set its main texture
        this.renderer = GetComponent<Renderer>();
        this.ChangeToState(this.startingState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GoToLevel()
    {
        Debug.Log("Clicked to change scene!!!");
        this.gameManager.SwitchToLevel(this.level.name.ToString());
    }

    public void ChangeToOpen()
    {
        ChangeToState(State.Open);
    }

    public void ChangeToClosed()
    {
        ChangeToState(State.Closed);
    }

    public void ChangeToDefeated()
    {
        ChangeToState(State.Defeated);
    }

    private void ChangeToState(State state)
    {
        this.currentState = state;
        renderer.material.mainTexture = state switch
        {
            State.Open => this.levelTexture.open,
            State.Closed => this.levelTexture.closed,
            State.Defeated => this.levelTexture.defeated,
            _ => renderer.material.mainTexture
        };
    }
}
