using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    
    private EnemyInstance[] _enemies;
    private EnemyInstance _activeEnemy;

    private void Awake()
    {
        Instance = this;
    }
    
    public void Start()
    {
        FindEnemies();
        _activeEnemy = _enemies[0];
    }

    public void FindEnemies()
    {
        _enemies = new EnemyInstance[] { };
        _enemies = FindObjectsByType<EnemyInstance>(FindObjectsSortMode.None);
    }

    public void NextActiveEnemy()
    {
        var index = System.Array.IndexOf(_enemies, _activeEnemy);
        index++;

        _activeEnemy = (index % 3) switch
        {
            0 => _enemies[0],
            1 => _enemies[1],
            2 => _enemies[2],
            _ => _activeEnemy
        };
    }

    public int GetEnemyCount()
    {
        return _enemies.Length;
    }
}
