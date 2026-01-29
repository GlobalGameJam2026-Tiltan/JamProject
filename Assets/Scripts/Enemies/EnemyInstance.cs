using UnityEngine;

public class EnemyInstance : MonoBehaviour
{
    [SerializeField] private EnemyData data;

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
