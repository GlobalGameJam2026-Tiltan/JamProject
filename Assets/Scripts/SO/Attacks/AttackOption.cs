using UnityEngine;

[CreateAssetMenu(fileName = "AttackOption", menuName = "Scriptable Objects/AttackOption")]
public class AttackOption : ScriptableObject
{
    public MasqueType type;
    public Sprite sprite;
    public string attackName;
    public int damage;
    public float hitChance;
    
    public Color GetColorType()
    {
        return type switch
        {
            MasqueType.Strength => new Color(),
            MasqueType.Intelligence => new Color(),
            MasqueType.Charisma => new Color(),
            _ => Color.white
        };
    }

    public void Attack()
    {
        Debug.Log($"{attackName} Attack!\nDid {damage} damage with a {hitChance}% hit chance");
    }
}
