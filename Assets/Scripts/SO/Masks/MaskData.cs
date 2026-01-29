using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MaskData", menuName = "Scriptable Objects/MaskData")]
public class MaskData : ScriptableObject
{
    public MaskType type;
    public float durability, maxDurability;
    public Sprite sprite;
    public Color color;
    public List<AttackOption> attacks = new List<AttackOption>();
    
    public void TakeDamage(float damage = 1f)
    {
        durability -= damage;
    }

    public void Heal()
    {
        durability = maxDurability;
    }

    public void LightAttack()
    {
        attacks[0].Attack();
    }

    public void MediumAttack()
    {
        attacks[1].Attack();
    }

    public void HeavyAttack()
    {
        attacks[2].Attack();
    }
}
