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
    
    public void SwapMasque(MasqueData newActiveMasque)
    {
        if (newActiveMasque.type == playerData.GetActiveMasque().type) return;
        playerData.SetActiveMasque(newActiveMasque);
    }
    
    public void Heal()
    {
        foreach (var masque in playerData.masques)
        {
           masque.Heal();
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
        playerData.GetActiveMasque().TakeDamage(damage);
    }

    public void UpgradeMasque(MasqueData upgradedMasque)
    {
        playerData.SetMasque(upgradedMasque);
    }

    public void AttackBasic()
    {
        playerData.GetActiveMasque().LightAttack();
    }
    
    public void AttackMedium()
    {
        playerData.GetActiveMasque().MediumAttack();
    }

    public void AttackStrong()
    {
        playerData.GetActiveMasque().HeavyAttack();
    }
}