using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sprite[] bodyTypes;
    private BodyType _bodyType;
    private SpriteRenderer _spriteRenderer;
    private EnemyAnimator _enemyAnimator;

    public void RandomizeBodyType()
    {
        _bodyType = (BodyType)Random.Range(0, (int)BodyType.Miniboss);
        data.sprite = bodyTypes[(int)_bodyType];
    }
    
    public bool IsAlive => data.health > 0;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyAnimator.InitializeEnemy(data,EnemyAnimationLibrary.Instance);
    }
    
    public virtual AttackOption Attack()
    {
        var rnd = Random.Range(0, 3);
        var attack = data.attacks[rnd];
        audioSource.PlayOneShot(attack.attackVoiceLine);

        //_enemyAnimator.
        
        return attack;
    }
    
    public MasqueType GetMasqueType() => data.type;
    public EnemyData GetData() => data;

    public virtual bool TakeDamage(float damage)
    {
        data.health -= damage;
        if (data.health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    public virtual void Die()
    {
        // TODO: add death logic (probably animation)
    }
}
