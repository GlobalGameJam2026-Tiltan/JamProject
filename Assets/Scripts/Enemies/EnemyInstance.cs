using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sprite[] bodyTypes;
    private BodyType _bodyType;
    private SpriteRenderer _spriteRenderer;

    public void RandomizeBodyType()
    {
        _bodyType = (BodyType)Random.Range(0, (int)BodyType.Miniboss);
        data.sprite = bodyTypes[(int)_bodyType];
    }
    
    public virtual AttackOption Attack()
    public bool IsAlive => data.health > 0;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    public AttackOption Attack()
    {
        var rnd = Random.Range(0, 3);
        var attack = data.attacks[rnd];
        audioSource.PlayOneShot(attack.attackVoiceLine);
        // TODO: play animation data.AttackAnimations[rnd]
        return attack;
    }
    
    public MasqueType GetMasqueType() => data.type;
    public EnemyData GetData() => data;

    public virtual void TakeDamage(float damage)
    {
        data.health -= damage;
        if (data.health <= 0) Die();
    }

    public virtual void Die()
    {
        // TODO: add death logic (probably animation)
    }
}
