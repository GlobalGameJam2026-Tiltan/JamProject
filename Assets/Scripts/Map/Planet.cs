using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.UI; // Make sure you include this

public class Planet : MonoBehaviour
{
    public PlanetData planet;
    
    public SceneAsset level;

    private GameManager gameManager;
    
    public bool isBossPlanet = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if (isBossPlanet)
        {
            if (gameManager.IsAllPlanetsDefeated()) this.planet.state = PlanetState.Open;
            else this.planet.state = PlanetState.Locked;
            
            if (this.planet.state == PlanetState.Open) gameObject.GetComponent<Image>().enabled = true;
            else  gameObject.GetComponent<Image>().enabled = false;
        }
        
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

        if (this.planet.state == PlanetState.Open) gameObject.GetComponent<Button>().interactable = true;
        else  gameObject.GetComponent<Button>().interactable = false;
    }

    public void GoToLevel()
    {
        if (this.planet.state == PlanetState.Open)
        {
            //this.gameManager.SwitchToLevel(this.level.name.ToString());
            if(!isBossPlanet)
                EncounterManager.Instance.StartRandomEncounter();
        }
    }
}
