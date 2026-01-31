using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerName = "Player";
    public MasqueData[] masques = new MasqueData[3];
    public MasqueData[] baseMasques = new MasqueData[3];
    public Dictionary<MasqueType, int> PlayerStats = new()
    {
        { MasqueType.Strength, 1 },
        { MasqueType.Intelligence, 1 },
        { MasqueType.Charisma, 1 }
    };
    public Sprite backSprite;
    public Sprite defaultBackSprite;
    public Sprite[] defendAnimation;
    public Sprite[] idleAnimation;

    private MasqueType _activeMasqueType;
    private Sprite _masqueSprite;
    public int BossesKilled { get; private set; }

    public void ResetPlayer()
    {
        for (var i = 0; i < masques.Length; i++)
        {
            masques[i] = baseMasques[i];
            masques[i].ResetMasqueData();
        }
        
        PlayerStats = new ()
        {
            { MasqueType.Strength, 1 },
            { MasqueType.Intelligence, 1 },
            { MasqueType.Charisma, 1 }
        };
        
        playerName = "Player";
        _activeMasqueType = MasqueType.Strength;
        _masqueSprite = masques[(int)_activeMasqueType].sprite;
        BossesKilled = 0;
        backSprite = defaultBackSprite;
    }


    public void BossKilled()
    {
        BossesKilled++;
    }
    
    public void ChangeName(string newName)
    {
        playerName = newName;
    }

    public void SetMasque(MasqueData newMasque)
    {
        if (newMasque.durability <= 0) return;
        masques[(int)newMasque.type] = newMasque;
    }

    public void SetActiveMasque(MasqueType masqueType)
    {
        _activeMasqueType = masqueType;
        _masqueSprite = GetMasqueByType(masqueType).sprite;
    }

    public MasqueData GetActiveMasque()
    {
        return GetMasqueByType(_activeMasqueType);
    }

    public MasqueData GetMasqueByType(MasqueType type)
    {
        return masques[(int)type];
    }

    public void EndAnimation()
    {
        backSprite = defaultBackSprite;
    }
}