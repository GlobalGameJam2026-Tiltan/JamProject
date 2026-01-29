using UnityEngine;
using System.Collections.Generic;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Sprite playerSprite;

    public void UpgradeStat(string stat, int amount = 1)
    {
        playerData.PlayerStats[stat] += amount;
    }
    
    public void ChangeName(string newName)
    {
        playerData.ChangeName(newName);
    }
    
    public void SwapMask(MaskData newActiveMask)
    {
        if (newActiveMask.type == playerData.GetActiveMask().type) return;
        playerData.SetActiveMask(newActiveMask);
    }
    
    public void Heal()
    {
        foreach (var mask in playerData.masks)
        {
           mask.Heal();
        }
    }

    public void ResetPlayer()
    {
        playerData.playerName = "Player";
        playerData.PlayerStats = new Dictionary<string, int>()
        {
            { "Strength", 1 },
            { "Intelligence", 1 },
            { "Charisma", 1 }
        };
    }

    public void TakeDamage(float damage)
    {
        playerData.GetActiveMask().TakeDamage(damage);
    }

    public void UpgradeMask(MaskData upgradedMask)
    {
        playerData.SetMask(upgradedMask);
    }

    public void AttackBasic()
    {
        playerData.GetActiveMask().LightAttack();
    }
    
    public void AttackMedium()
    {
        playerData.GetActiveMask().MediumAttack();
    }

    public void AttackStrong()
    {
        playerData.GetActiveMask().HeavyAttack();
    }
}