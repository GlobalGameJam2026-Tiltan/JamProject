using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance;

    private List<EnemyInstance> _activeEnemy = new();
    private PlayerCombat player;
    [SerializeField] private EnemyInstance[] grunts;
    private bool _isBossEncounter;
    private bool _inBattle;
    public bool PlayerTurn { get; private set; }
    private bool _enemyTurn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Awake()
    {
        Instance = this;
        
        //TODO - For Testing
        FindPlayer();
        StartRandomEncounter();
    }

    protected void Update()
    {
        if (_inBattle)
        {
            //now check if an animation is in progress
        }
    }

    public void PlayerAction()
    {
        PlayerTurn = false;
    }

    public void FindPlayer()
    {
        player = FindFirstObjectByType<PlayerCombat>();
    }

    public PlayerCombat GetPlayer() => player;

    public void StartRandomEncounter()
    {
        _isBossEncounter = false;
        _activeEnemy.Clear();
        //Roll a random enemy
        for (var i = 0; i <= player.BossesKilled; i++)
        {
            _activeEnemy.Add(grunts[Random.Range(0, grunts.Length)]);
        }

        StartBattle();
    }

    public void StartMiniBossEncounter(EnemyInstance enemy)
    {
        _isBossEncounter = false;
        _activeEnemy.Clear();
        _activeEnemy.Add(enemy);
        StartBattle();
    }

    private void StartBattle()
    {
        _inBattle = true;
        PlayerTurn = true;
        //TODO - ???
    }

    public void AttackUsed(EntityType target, MasqueType attackType, AttackOption attack)
    {
        //did the attack hit?
        var hit = Random.Range(0.0f, 1.0f) <= attack.hitChance;
        if (hit)
        {
            var damage = CalculateDamage(target, attack.damage);
            if (target == EntityType.Player)
            {
                player.TakeDamage(damage);
            }
            else
            {
                _activeEnemy.First().TakeDamage(damage);
            }
        }

        var attackerName = target == EntityType.Enemy ? player.name : _activeEnemy.First().name;
        var targetName =
            _isBossEncounter ? "" :
            target == EntityType.Player ? player.name : _activeEnemy.First().name;
        ShowResultScreen(hit, attack.attackName, attackerName, targetName);
    }

    private float CalculateDamage(EntityType target, int attackDamage)
    {
        //TODO - Calculate Damage
        return attackDamage;
    }

    private void ShowResultScreen(bool hit, string attackName, string attackerName, string targetName)
    {
        //TODO - Handle Result Screen
        _enemyTurn = true;
    }

    public void PlayAnimation(EntityType entityType, Sprite[] sprites)
    {
    }

    public void SwappedMasque()
    {
        //TODO - Show Masque Swapped Popup
        _enemyTurn = true;
    }
}