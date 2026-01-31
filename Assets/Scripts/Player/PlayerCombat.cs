using System.Collections;
using UnityEngine;
using System.Linq;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private AudioSource audioSource;
    private SpriteRenderer _masqueSpriteRenderer;
    private SpriteRenderer _bodySpriteRenderer;

    private void Awake()
    {
        var renderers = GetComponentsInChildren<SpriteRenderer>();
        if(renderers.Length > 1)
        {
            _masqueSpriteRenderer = renderers.First(x => x.name == "Masque");
            _bodySpriteRenderer = renderers.First(x => x.name == "Body");
        }
    }
    
    public bool IsBlocking { get; private set; }

    public void UpgradeStat(MasqueType stat, int amount = 1)
    {
        playerData.PlayerStats[stat] += amount;
    }
    
    public void ChangeName(string newName)
    {
        playerData.ChangeName(newName);
    }
    
    public void SwapMasque(MasqueData newMasque)
    {
        playerData.SetMasque(newMasque);
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

    public void BossKilled(MasqueData reward)
    {
        playerData.BossKilled();
        SwapMasque(reward);
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
        playerData.PlayerStats = new()
        {
            { MasqueType.Strength, 1 },
            { MasqueType.Intelligence, 1 },
            { MasqueType.Charisma, 1 }
        };
    }
    
    public int GetPlayerDifficulty()
    {
        return playerData.PlayerStats[MasqueType.Strength] + playerData.PlayerStats[MasqueType.Intelligence] + playerData.PlayerStats[MasqueType.Charisma];
    }

    public bool TakeDamage(float damage)
    {
        playerData.GetActiveMasque().TakeDamage(damage);
        if (playerData.GetActiveMasque().durability <= 0)
        {
            var chosenMasques = playerData.masques.Where(x => x.durability > 0).ToArray();
            if (!chosenMasques.Any())
                return true;
            
            var newMasque = Random.Range(0, chosenMasques.Length);
            SetActiveMasque(chosenMasques[newMasque].type);
        }

        return false;
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
        StartCoroutine(PlayAnimation(GetActiveMasque().lightAttackAnim));
        var currentMasque = playerData.GetActiveMasque();
        EncounterManager.Instance.AttackUsed(EntityType.Enemy,currentMasque.type, currentMasque.attacks[0]);
        playerData.EndAnimation();
    }
    
    public void AttackMedium()
    {
        EncounterManager.Instance.PlayerAction();
        
        playerData.GetActiveMasque().MediumAttack(audioSource);
        StartCoroutine(PlayAnimation(GetActiveMasque().mediumAttackAnim));
        var currentMasque = playerData.GetActiveMasque();
        EncounterManager.Instance.AttackUsed(EntityType.Enemy,currentMasque.type, currentMasque.attacks[1]);
    }

    public void AttackStrong()
    {
        EncounterManager.Instance.PlayerAction();
        
        playerData.GetActiveMasque().HeavyAttack(audioSource);
        StartCoroutine(PlayAnimation(GetActiveMasque().hardAttackAnim));
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
    
    private IEnumerator PlayAnimation(Sprite[] frames)
    {
        var frameDelay = 0.05f;

        foreach (var frame in frames)
        {
            _bodySpriteRenderer.sprite = frame;
            yield return new WaitForSeconds(frameDelay);
        }

        _bodySpriteRenderer.sprite = playerData.defaultBackSprite;
    }
}