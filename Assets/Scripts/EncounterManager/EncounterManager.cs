using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance;

    [SerializeField] private EncounterSo encounterSo;
    [SerializeField] private GameObject[] gruntEnemies;
    [SerializeField] private GameObject strength;
    [SerializeField] private GameObject intelligence;
    [SerializeField] private GameObject charisma;
    [SerializeField] private GameObject boss;
    [SerializeField] private float damageModifier = 10.0f;
    [SerializeField] private float strengthMultiplier = 1.5f;
    [SerializeField] private float weaknessMultiplier = 0.5f;
    private PlayerCombat player;
    public bool PlayerTurn => encounterSo.playerTurn;
    private GameObject _enemySpawn;
    private GameManager _gameManager;

    private Dictionary<MasqueType,MasqueType> winners = new()
    {
        {MasqueType.Strength, MasqueType.Intelligence},
        {MasqueType.Intelligence, MasqueType.Charisma},
        {MasqueType.Charisma, MasqueType.Strength},
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public bool IsMiniBossBattle()
    {
        return encounterSo.encounterType != EncounterType.Random && encounterSo.encounterType != EncounterType.Boss;
    }

    public bool IsMidBattle()
    {
        return encounterSo.inBattle;
    }

    public void PlayerAction()
    {
        encounterSo.playerTurn = false;
    }

    public void FindPlayer()
    {
        player = FindFirstObjectByType<PlayerCombat>();
    }

    public PlayerCombat GetPlayer() => player;
    public EnemyInstance GetEnemy() => encounterSo.activeEnemyInstance;

    public void SetEncounter(EncounterType encounterType)
    {
        encounterSo.encounterType = encounterType;
        SceneFader.Instance.LoadSceneWithFade("Encounter");
    }
    public void StartEncounter()
    {
        _enemySpawn = GameObject.FindGameObjectWithTag("Enemy");
        switch(encounterSo.encounterType)
        {
            case EncounterType.Random:
                //Roll a random enemy
                SpawnRandomEnemy();
                encounterSo.remainingEnemies = player.BossesKilled;
                break;
            case EncounterType.Strength:
                encounterSo.activeEnemy = Instantiate(strength, _enemySpawn.transform);
                encounterSo.remainingEnemies = 0;
            break;
            case EncounterType.Intelligence:
                encounterSo.activeEnemy = Instantiate(intelligence, _enemySpawn.transform);
                encounterSo.remainingEnemies = 0;
                break;
            case EncounterType.Charisma:
                encounterSo.activeEnemy = Instantiate(charisma, _enemySpawn.transform);
                encounterSo.remainingEnemies = 0;
                break;
            case EncounterType.Boss:
                encounterSo.activeEnemy = Instantiate(boss, _enemySpawn.transform);
                encounterSo.remainingEnemies = 0;
                break;
        }
        
        encounterSo.activeEnemy.transform.localPosition = Vector3.zero;
        encounterSo.activeEnemyInstance = encounterSo.activeEnemy.GetComponent<EnemyInstance>();
        encounterSo.activeEnemyInstance.GetData().ResetEnemy();
        StartBattle();
    }

    private void SpawnRandomEnemy()
    {
        var grunt = gruntEnemies[Random.Range(0, gruntEnemies.Length)];

        encounterSo.activeEnemy = Instantiate(grunt, _enemySpawn.transform);
        encounterSo.activeEnemy.transform.localPosition = Vector3.zero;
        encounterSo.activeEnemyInstance = encounterSo.activeEnemy.GetComponent<EnemyInstance>();
        encounterSo.activeEnemyInstance.name = EnemyNames.GetRandomName();
        encounterSo.activeEnemyInstance.GetData().ResetEnemy();
        encounterSo.activeEnemyInstance.RandomizeBodyType();
        encounterSo.playerTurn = true;
    }

    private void StartBattle()
    {
        encounterSo.inBattle = true;
        encounterSo.playerTurn = true;
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
                if (player.IsBlocking)
                {
                    player.Unblock();
                    damage /= 2;
                }
                var died = player.TakeDamage(damage);
                if(died)
                    SceneFader.Instance.LoadSceneWithFade("GameOver");
            }
            else
            {
                var enemyType = encounterSo.activeEnemyInstance.GetMasqueType();
                if(encounterSo.activeEnemyInstance.TakeDamage(damage))
                    player.UpgradeStat(enemyType);
            }
        }

        var attackerName = target == EntityType.Enemy ? player.name : encounterSo.activeEnemyInstance.name;
        var targetName = target == EntityType.Player ? player.name : encounterSo.activeEnemyInstance.name;
        ShowResultScreen(hit, attack.attackName, attackerName, targetName);

        if (target == EntityType.Enemy)
        {
            if (encounterSo.activeEnemyInstance.IsAlive)
                StartCoroutine(EnemyTurn());
            else
            {
                var reward = encounterSo.activeEnemyInstance.RewardMasque;
                if (encounterSo.remainingEnemies > 0)
                {
                    encounterSo.remainingEnemies--;
                    SpawnRandomEnemy();
                }
                else
                {
                    _gameManager.PlanetDefeated();
                    if (encounterSo.encounterType != EncounterType.Random)
                    {
                        player.BossKilled(reward);
                    }
                }
            }
        }
    }

    private float CalculateDamage(EntityType target, int attackDamage)
    {
        float modifier;
        //each point adds 10% damage
        if (target == EntityType.Player)
        {
            modifier = ( damageModifier + player.GetPlayerDifficulty() ) / damageModifier;
        }
        else
        {
            modifier = ( damageModifier + player.GetPlayerStrength(player.GetActiveMasque().type) ) / damageModifier;
        }
        
        return attackDamage * modifier * GetMultiplier(target);
    }

    private float GetMultiplier(EntityType target)
    {
        var playerType = player.GetActiveMasque().type;
        var enemyType = encounterSo.activeEnemyInstance.GetData().type;
        
        if(playerType == enemyType)
            return 1.0f;

        return target == EntityType.Enemy ? 
            winners[playerType] == enemyType ? strengthMultiplier : weaknessMultiplier :
            winners[enemyType] == playerType ? strengthMultiplier : weaknessMultiplier;
    }

    private void ShowResultScreen(bool hit, string attackName, string attackerName, string targetName)
    {
        //TODO - Handle Result Screen
    }

    public void SwappedMasque()
    {
        //TODO - Show Masque Swapped Popup
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        //first wait 1 second
        yield return new WaitForSeconds(1.5f);

        var attack = encounterSo.activeEnemyInstance.Attack();

        AttackUsed(EntityType.Player, encounterSo.activeEnemyInstance.GetMasqueType(), attack);

        //now wait 1 second before returning to player turn
        yield return new WaitForSeconds(1.5f);
        encounterSo.playerTurn = true;
    }
}