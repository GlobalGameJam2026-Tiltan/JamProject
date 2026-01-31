using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "MasqueData", menuName = "Scriptable Objects/MasqueData")]
public class MasqueData : ScriptableObject
{
    public MasqueType type;
    public float durability, maxDurability;
    public Sprite sprite;
    public Sprite icon;
    public Sprite swapIcon;
    public Color color;
    public AttackOption[] attacks = new AttackOption[3];
    public Sprite[] lightAttackAnim, mediumAttackAnim, hardAttackAnim;

    public void ResetMasqueData()
    {
        durability = maxDurability;
    }
    
    public void TakeDamage(float damage = 1f)
    {
        durability -= damage;
    }

    public void Heal()
    {
        durability = maxDurability;
    }

    public void LightAttack(AudioSource audioSource)
    {
        audioSource.PlayOneShot(attacks[0].attackVoiceLine);
    }

    public void MediumAttack(AudioSource audioSource)
    {
        audioSource.PlayOneShot(attacks[1].attackVoiceLine);
    }

    public void HeavyAttack(AudioSource audioSource)
    {
        audioSource.PlayOneShot(attacks[2].attackVoiceLine);
    }
}