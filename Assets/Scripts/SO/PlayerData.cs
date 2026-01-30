using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string playerName = "Player";
    public MasqueData[] masques = new MasqueData[3];
    public Dictionary<string, int> PlayerStats = new Dictionary<string, int>()
    {
        { "Strength", 1 },
        { "Intelligence", 1 },
        { "Charisma", 1 }
    };

    public Sprite backSprite;

    private MasqueType _activeMasqueType;
    private Sprite _masqueSprite;

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

    public int GetPlayerDifficulty()
    {
        return PlayerStats["Strength"] + PlayerStats["Intelligence"] + PlayerStats["Charisma"];
    }

    private MasqueData GetMasqueByType(MasqueType type)
    {
        return masques[(int)type];
    }
}