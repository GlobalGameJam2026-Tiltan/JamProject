using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public MasqueType type;
    public float health;
    public Sprite sprite;
    public Sprite icon;
    public AttackOption[] attacks = new AttackOption[3];
    public AudioSource audioSource;
    public Sprite[][] AttackAnimations = new Sprite[3][];

    public void Attack()
    {
        var rnd = Random.Range(0, 3);
        audioSource.PlayOneShot(attacks[rnd].attackVoiceLine);
        // TODO: play animation AttackAnimations[rnd]
    }
}