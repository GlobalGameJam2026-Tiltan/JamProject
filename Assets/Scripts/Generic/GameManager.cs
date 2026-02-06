using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<RandomPlanetData> planets = new();
    private PlanetLocation _currentLocation;

    [SerializeField] private int width = 10;
    [SerializeField] private int height = 6;
    [SerializeField] private Sprite[] planetSprites;
    [SerializeField] private Sprite[] overlaySprites;
    [SerializeField] public Sprite playerHoverSprite;

    public List<RandomPlanetData> Planets => planets;

    private void Awake()
    {
        // This makes the GameObject this script is attached to persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    private void CreatePlanet(PlanetLocation location, Sprite[] sprites, Sprite[] overlays)
    {
        var planet = ScriptableObject.CreateInstance<RandomPlanetData>();
        planet.InitializePlanetData(location);
        planet.sprite = sprites[Random.Range(0, sprites.Length)];
        if(Random.value > .5f)
            planet.overlaySprite = overlays[Random.Range(0, overlays.Length)];
        planet.encounterType = EncounterType.Random;
        planets.Add(planet);
    }

    public RandomPlanetData GetPlanet(PlanetLocation location)
    {
        return planets.Find(planet => planet.location == location);
    }

    public void SetCurrentLocation(PlanetLocation location)
    {
        _currentLocation = location;
        planets.Where(x => x.hoverSprite is not null).ToList().ForEach(x => x.hoverSprite = null);
        planets.Find(x => x.location == _currentLocation).hoverSprite = playerHoverSprite;
    }

    public void PlanetDefeated()
    {
        //TODO Move to new planet
        SceneFader.Instance.LoadSceneWithFade("Map");
    }

    public void ResetGame()
    {
        //First destroy all existing planets if any
        foreach (var planet in planets)
        {
            Destroy(planet);
        }

        planets.Clear();

        //now generate new planets
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                CreatePlanet(new PlanetLocation(i, j), planetSprites, overlaySprites);
            }
        }
        
        //set the player at a random planet
        planets[Random.Range(0,planets.Count)].hoverSprite = playerHoverSprite;
    }
}