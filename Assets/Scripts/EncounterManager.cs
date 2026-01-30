using System.Collections;
using Enemies;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public static EncounterManager Instance;

    private EnemyInstance _activeEnemy;
    private GameObject _activeEnemyGameObject;
    private PlayerCombat player;
    [SerializeField] private GameObject[] gruntEnemies;
    private bool _isBossEncounter;
    private bool _inBattle;
    public bool PlayerTurn { get; private set; }
    private int _enemiesLeft;
    private GameObject _enemySpawn;

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
    public EnemyInstance GetEnemy() => _activeEnemy;

    public void StartRandomEncounter()
    {
         _enemySpawn = GameObject.FindGameObjectWithTag("Enemy");
        _isBossEncounter = false;
        //Roll a random enemy
        _enemiesLeft = player.BossesKilled;
        SpawnRandomEnemy();

        StartBattle();
    }

    private void SpawnRandomEnemy()
    {
        var grunt = gruntEnemies[Random.Range(0, gruntEnemies.Length)];

        _activeEnemyGameObject = Instantiate(grunt, _enemySpawn.transform);
        _activeEnemyGameObject.transform.localPosition = Vector3.zero;
        _activeEnemy = _activeEnemyGameObject.GetComponent<EnemyInstance>();
        _activeEnemy.name = EnemyNames.GetRandomName();
        _activeEnemy.GetData().health = _activeEnemy.GetData().maxHealth;
        _activeEnemy.RandomizeSprite();
    }

    public void StartMiniBossEncounter(EnemyInstance enemy)
    {
        _enemySpawn = GameObject.FindGameObjectWithTag("Enemy");
        _isBossEncounter = false;
        _activeEnemy = enemy;
        _enemiesLeft = 0;
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
                _activeEnemy.TakeDamage(damage);
            }
        }

        var attackerName = target == EntityType.Enemy ? player.name : _activeEnemy.name;
        var targetName =
            _isBossEncounter ? "" :
            target == EntityType.Player ? player.name : _activeEnemy.name;
        ShowResultScreen(hit, attack.attackName, attackerName, targetName);

        if (target == EntityType.Enemy)
        {
            if(_activeEnemy.IsAlive)
                StartCoroutine(EnemyTurn());
            else
            {
                if (_enemiesLeft > 0)
                {
                    _enemiesLeft--;
                    SpawnRandomEnemy();
                }
            }
        }
    }

    private float CalculateDamage(EntityType target, int attackDamage)
    {
        //TODO - Calculate Damage
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
        
        var attack = _activeEnemy.Attack();
        
        AttackUsed(EntityType.Player,_activeEnemy.GetMasqueType(),attack);
        
        //now wait 1 second before returning to player turn
        yield return new WaitForSeconds(1.0f);
        PlayerTurn = true;
    }
}