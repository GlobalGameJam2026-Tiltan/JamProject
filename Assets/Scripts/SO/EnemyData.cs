using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public MaskType type;
    public float health;
    public Sprite sprite;
    public AttackOption[] attacks = new AttackOption[3];

    public void Attack()
    {
        attacks[Random.Range(0, 3)].Attack();
    }
}