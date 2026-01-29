using UnityEngine;

[CreateAssetMenu(fileName = "AttackOption", menuName = "Scriptable Objects/AttackOption")]
public class AttackOption : ScriptableObject
{
    public MaskType type;
    public string attackName;
    public int damage;
    public float hitChance;
    
    public Color GetColorType()
    {
        return type switch
        {
            MaskType.Strength => new Color(),
            MaskType.Intelligence => new Color(),
            MaskType.Charisma => new Color(),
            _ => Color.white
        };
    }

    public void Attack()
    {
        // TODO: add attack logic
    }
}
