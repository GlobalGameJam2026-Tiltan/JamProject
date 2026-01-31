using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public MasqueType type;
    public float health, maxHealth;
    public Sprite sprite;
    public Sprite icon;
    public AttackOption[] attacks = new AttackOption[3];
    public BodyType bodyType;
    public AudioClip[] idleLines;
    public AudioClip deathLine;

    public void ResetEnemy()
    {
        health = maxHealth;
    }
}