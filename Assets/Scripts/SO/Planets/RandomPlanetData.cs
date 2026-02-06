using Enemies;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomPlanetData", menuName = "Scriptable Objects/RandomPlanetData")]
public class RandomPlanetData : ScriptableObject
{
    public PlanetLocation location;
    public EncounterType encounterType;
    public string planetName;
    public Sprite sprite;
    [CanBeNull] public Sprite overlaySprite;
    [CanBeNull] public Sprite hoverSprite;
    
    public void InitializePlanetData(PlanetLocation loc)
    {
        location = loc;
        planetName = EnemyNames.GetRandomName();
    }
}
