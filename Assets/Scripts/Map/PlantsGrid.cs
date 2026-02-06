using UnityEngine;

public class PlantsGrid : MonoBehaviour
{
    [SerializeField] private GameObject randomPlanetPrefab;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        
        //generate the Planet Instances
        foreach (var planet in _gameManager.Planets)
        {
            var newPlanet = Instantiate(randomPlanetPrefab, transform);
            var randomPlanet = newPlanet.GetComponent<RandomPlanet>();
            randomPlanet.SetPlanetLocation(planet.location);
        }
    }
}