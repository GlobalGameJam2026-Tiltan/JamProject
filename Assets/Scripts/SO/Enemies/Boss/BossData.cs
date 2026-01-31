using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "Scriptable Objects/BossData")]
public class BossData : ScriptableObject
{
    public float[] healths = new float[3];
    public float[] maxHealths = new float[3];
    public Sprite sprite;
    public Sprite strIcon;
    public Sprite intIcon;
    public Sprite chaIcon;
    public AttackOption[] phase1Attacks = new AttackOption[9];
    public AttackOption[] phase2Attacks = new AttackOption[9];
    public AttackOption[] phase3Attacks = new AttackOption[9];
    public Sprite[] punchAnimation;
    public Sprite[] grabAnimation;
    public AudioClip[] idleLines;
    public AudioClip deathLine;

    public void ResetBoss()
    {
        for (var i = 0; i < healths.Length; i++)
        {
            healths[i] = maxHealths[i];
        }
    }
}