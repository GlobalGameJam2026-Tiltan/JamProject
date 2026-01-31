using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sprite[] bodyTypes;
    private BodyType _bodyType;

    public void RandomizeBodyType()
    {
        _bodyType = (BodyType)Random.Range(0, (int)BodyType.Miniboss);
        data.sprite = bodyTypes[(int)_bodyType];
    }
    
    public virtual void Attack()
    {
        var rnd = Random.Range(0, 3);
        audioSource.PlayOneShot(data.attacks[rnd].attackVoiceLine);
        // TODO: play animation data.AttackAnimations[rnd]
    }

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
