using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public MasqueType type;
    public float health;
    public Sprite sprite;
    public Sprite icon;
    public AttackOption[] attacks = new AttackOption[3];
    public Sprite[][] AttackAnimations = new Sprite[3][];
}