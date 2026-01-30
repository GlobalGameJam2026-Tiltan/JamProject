using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sprite[] alternateSprites;
    private SpriteRenderer _spriteRenderer;
    
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

    public void RandomizeSprite()
    {
        _spriteRenderer.sprite = alternateSprites[Random.Range(0, alternateSprites.Length)];
    }
    
    public MasqueType GetMasqueType() => data.type;
    public EnemyData GetData() => data;

    public void TakeDamage(float damage)
    {
        data.health -= damage;
        if (data.health <= 0) Die();
    }

    private void Die()
    {
        // TODO: add death logic (probably animation)
    }
}
