using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private AudioSource audioSource;
    private SpriteRenderer _masqueSpriteRenderer;

    private void Awake()
    {
        var renderers = GetComponentsInChildren<SpriteRenderer>();
        _masqueSpriteRenderer = renderers.First(x => x.name == "Masque");
    }
    
    public bool IsBlocking { get; private set; }

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
        playerData.SetMasque(newActiveMasque);
    }
    
    public void SetActiveMasque(MasqueType type)
    {
        playerData.SetActiveMasque(type);
        _masqueSpriteRenderer.sprite = GetActiveMasque().sprite;
    }
    
    public MasqueData GetActiveMasque()
    {
        return playerData.GetActiveMasque();
    }
    
    public MasqueData GetMasqueByType(MasqueType type)
    {
        return playerData.GetMasqueByType(type);
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
    
    public int GetPlayerDifficulty()
    {
        return playerData.PlayerStats["Strength"] + playerData.PlayerStats["Intelligence"] + playerData.PlayerStats["Charisma"];
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
        //Lock Buttons
        EncounterManager.Instance.PlayerAction();
        playerData.GetActiveMasque().LightAttack(audioSource);
        var currentMasque = playerData.GetActiveMasque();
        EncounterManager.Instance.AttackUsed(EntityType.Enemy,currentMasque.type, currentMasque.attacks[0]);
    }
    
    public void AttackMedium()
    {
        EncounterManager.Instance.PlayerAction();
        playerData.GetActiveMasque().MediumAttack(audioSource);
        var currentMasque = playerData.GetActiveMasque();
        EncounterManager.Instance.AttackUsed(EntityType.Enemy,currentMasque.type, currentMasque.attacks[1]);
    }

    public void AttackStrong()
    {
        EncounterManager.Instance.PlayerAction();
        playerData.GetActiveMasque().HeavyAttack(audioSource);
        var currentMasque = playerData.GetActiveMasque();
        EncounterManager.Instance.AttackUsed(EntityType.Enemy,currentMasque.type, currentMasque.attacks[2]);
    }

    public void Block()
    {
        IsBlocking = true;
    }
    
    public void Unblock()
    {
        IsBlocking = false;
    }
    
    public int BossesKilled => playerData.BossesKilled;
}