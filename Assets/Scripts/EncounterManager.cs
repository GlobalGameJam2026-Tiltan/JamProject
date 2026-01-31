using System.Collections;
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
    private PlayerCombat player;
    public bool PlayerTurn => encounterSo.playerTurn;
    private GameObject _enemySpawn;
    private GameManager _gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Awake()
    {
        Instance = this;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    public void StartRandomEncounter()
    {
        _enemySpawn = GameObject.FindGameObjectWithTag("Enemy");
        encounterSo.encounterType = EncounterType.Random;
        //Roll a random enemy
        encounterSo.remainingEnemies = player.BossesKilled;
        SpawnRandomEnemy();

        StartBattle();
    }

    private void SpawnRandomEnemy()
    {
        var grunt = gruntEnemies[Random.Range(0, gruntEnemies.Length)];

        encounterSo.activeEnemy = Instantiate(grunt, _enemySpawn.transform);
        encounterSo.activeEnemy.transform.localPosition = Vector3.zero;
        encounterSo.activeEnemyInstance = encounterSo.activeEnemy.GetComponent<EnemyInstance>();
        encounterSo.activeEnemyInstance.name = EnemyNames.GetRandomName();
        encounterSo.activeEnemyInstance.GetData().health = encounterSo.activeEnemyInstance.GetData().maxHealth;
        encounterSo.activeEnemyInstance.RandomizeSprite();
    }

    public void StartMiniBossEncounter(EncounterType encounterType)
    {
        _enemySpawn = GameObject.FindGameObjectWithTag("Enemy");
        encounterSo.encounterType = encounterType;
        encounterSo.activeEnemy = encounterSo.encounterType switch
        {
            EncounterType.Strength => Instantiate(strength, _enemySpawn.transform),
            EncounterType.Intelligence => Instantiate(intelligence, _enemySpawn.transform),
            EncounterType.Charisma => Instantiate(charisma, _enemySpawn.transform)
        };
        encounterSo.activeEnemy.transform.localPosition = Vector3.zero;
        encounterSo.activeEnemyInstance = encounterSo.activeEnemy.GetComponent<EnemyInstance>();
        encounterSo.activeEnemyInstance.GetData().health = encounterSo.activeEnemyInstance.GetData().maxHealth;
        encounterSo.remainingEnemies = 0;
        StartBattle();
    }

    private void StartBattle()
    {
        encounterSo.inBattle = true;
        encounterSo.playerTurn = true;
        SceneFader.instance.LoadSceneWithFade("Encounter");
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
                encounterSo.activeEnemyInstance.TakeDamage(damage);
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
                if (encounterSo.remainingEnemies > 0)
                {
                    encounterSo.remainingEnemies--;
                    SpawnRandomEnemy();
                }
                else
                {
                    //_gameManager.PlanetDefeated();
                }
            }
        }
    }

    private float CalculateDamage(EntityType target, int attackDamage)
    {
        //each point adds 10% damage
        if (target == EntityType.Player)
        {
            //player.
        }
        
        return attackDamage;
    }

    private void ShowResultScreen(bool hit, string attackName, string attackerName, string targetName)
    {
        //TODO - Handle Result Screen
    }

    public void PlayAnimation(EntityType entityType, Sprite[] sprites)
    {
    }

    public void SwappedMasque()
    {
        //TODO - Show Masque Swapped Popup
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        //first wait 1 second
        yield return new WaitForSeconds(1.0f);

        //var attack = _activeEnemy.Attack();

        //AttackUsed(EntityType.Player, _activeEnemy.GetMasqueType(), attack);

        //now wait 1 second before returning to player turn
        yield return new WaitForSeconds(1.0f);
        //PlayerTurn = true;
    }
}