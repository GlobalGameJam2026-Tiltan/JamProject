using UnityEngine;

public class EncounterManager : MonoBehaviour
{

    public enum EntityType
    {
        Player,
        Enemy
    }
    
    public static EncounterManager Instance;

    private ScriptableObject _activeEnemy;
    private PlayerCombat player;
    [SerializeField] private ScriptableObject[] grunts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    public void FindPlayer()
    {
       player = FindFirstObjectByType<PlayerCombat>();
    }

    public PlayerCombat GetPlayer() => player;

    public void StartRandomEncounter()
    {
        //Roll a random enemy
        
    }
    
    public void StartMiniBossEncounter(ScriptableObject enemy)
    {
        
    }

    private void StartBattle(ScriptableObject enemy)
    {
        
    }

    public void PlayAnimation(EntityType entityType,Sprite[]  sprites)
    {
        
    }
}
