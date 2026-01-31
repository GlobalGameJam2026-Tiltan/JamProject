using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Encounter", menuName = "Scriptable Objects/Encounter")]
public class EncounterSo : ScriptableObject
{
    [SerializeField] public EncounterType encounterType;

    [SerializeField] public GameObject activeEnemy;

    [SerializeField] public EnemyInstance activeEnemyInstance;
    
    [SerializeField] public int remainingEnemies;
    [SerializeField] public bool inBattle;
    [SerializeField] public bool playerTurn;
}