using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public MasqueType type;
    public float health, maxHealth;
    public Sprite sprite;
    public Sprite icon;
    public AttackOption[] attacks = new AttackOption[3];
    public Sprite[][] AttackAnimations = new Sprite[3][];
    public Sprite[] lightAttack = new Sprite[3], mediumAttack = new Sprite[3], hardAttack = new Sprite[3];
    public Sprite[] lightAttackMasque = new Sprite[3], mediumAttackMasque = new Sprite[3], hardAttackMasque = new Sprite[3];
    public AudioClip[] idleLines;
    public AudioClip deathLine;

    public void ResetEnemy()
    {
        health = maxHealth;
    }
}