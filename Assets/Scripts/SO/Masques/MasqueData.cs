using UnityEngine;

[CreateAssetMenu(fileName = "MasqueData", menuName = "Scriptable Objects/MasqueData")]
public class MasqueData : ScriptableObject
{
    public MasqueType type;
    public float durability, maxDurability;
    public Sprite sprite;
    public Color color;
    public AttackOption[] attacks = new AttackOption[3];
    
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
