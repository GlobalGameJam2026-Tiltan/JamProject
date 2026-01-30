using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private AudioSource audioSource;
    
    public void Attack()
    {
        var rnd = Random.Range(0, 3);
        audioSource.PlayOneShot(data.attacks[rnd].attackVoiceLine);
        // TODO: play animation data.AttackAnimations[rnd]
    }

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
