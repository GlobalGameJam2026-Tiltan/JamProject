using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "Scriptable Objects/PlanetData")]
public class PlanetData : ScriptableObject
{
    public string planetName;
    [CanBeNull] public string previousPlanet;
    public PlanetState state;
    public Sprite currentSprite;
    public Sprite baseSprite;
    public Sprite lockedSprite;
    public Sprite defeatedSprite;
    public bool isStart;

    public void ChangeState(PlanetState newState)
    {
        state = newState;
        currentSprite = newState switch
        {
            PlanetState.Locked => lockedSprite,
            PlanetState.Defeated => defeatedSprite,
            _ => baseSprite
        };
    }

    public void ResetPlanet()
    {
        if (isStart)
        {
            state = PlanetState.Open;
            currentSprite = baseSprite;
        }
        state = PlanetState.Locked;
        currentSprite = lockedSprite;
    }
}